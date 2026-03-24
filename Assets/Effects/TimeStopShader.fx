sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2;
sampler uImage3;
float3 uColor;
float uOpacity;
float3 uSecondaryColor;
float uTime;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uImageOffset;
float uIntensity;
float uProgress;
float2 uDirection;
float uSaturation;
float4 uSourceRect;
float2 uZoom;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;

// Função de hash para gerar ruído pseudo-aleatório
float hash(float2 p)
{
    return frac(sin(dot(p, float2(127.1, 311.7))) * 43758.5453);
}

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    // --- DISTORÇÃO HORIZONTAL (fitas VHS) ---
    float distortSpeed  = 3.0;   // velocidade das fitas
    float distortAmount = 0.003; // força da distorção
    float lineNoise = hash(float2(floor(coords.y * uScreenResolution.y), floor(uTime * distortSpeed)));
    // Só distorce em certas linhas aleatórias
    float distortLine = step(0.97, lineNoise);
    float shift = (hash(float2(coords.y, uTime)) - 0.5) * distortAmount * distortLine;
    float2 distortedCoords = coords + float2(shift, 0.0);

    // --- CHROMATIC ABERRATION ---
    float aberration = 0.008; // separação das cores
    float4 colorR = tex2D(uImage0, distortedCoords + float2( aberration, 0.0));
    float4 colorG = tex2D(uImage0, distortedCoords);
    float4 colorB = tex2D(uImage0, distortedCoords - float2( aberration, 0.0));
    float4 color  = float4(colorR.r, colorG.g, colorB.b, colorG.a);

    // --- DESSATURAÇÃO LEVE ---
    float grey = dot(color.rgb, float3(0.299, 0.587, 0.114));
    float desatAmount = 1.0;
    color.rgb = lerp(color.rgb, float3(grey, grey, grey), desatAmount);

    // --- SCANLINES ---
    float scanlineFreq      = 3.0;  // espessura das linhas (menor = mais grossas)
    float scanlineIntensity = 0.15; // escuridão das linhas (0.0 = invisível, 1.0 = preto)
    float scanline = sin(coords.y * uScreenResolution.y * scanlineFreq) * 0.5 + 0.5;
    color.rgb *= 1.0 - scanlineIntensity * (1.0 - scanline);

    // --- GRAIN (ruído estático) ---
    float grainAmount = 0.06; // quantidade de grain (0.0 = nenhum)
    float grain = hash(coords + frac(uTime * 0.1)) - 0.8;
    color.rgb += grain * grainAmount;

    // --- VINHETA ---
    float vignetteStrength = 0.4; // força da vinheta (0.0 = nenhuma)
    float2 vigCoords = coords * (1.0 - coords.yx);
    float vignette = vigCoords.x * vigCoords.y * 15.0;
    // Uso do abs() aqui para evitar o aviso do compilador!
    vignette = pow(abs(vignette), vignetteStrength);
    color.rgb *= vignette;

    // --- FADE com uOpacity (para transição suave) ---
    float4 original = tex2D(uImage0, coords);
    color = lerp(original, color, uOpacity);

    return color;
}

technique Technique1
{
    pass GreyscaleEffect
    {
        
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}