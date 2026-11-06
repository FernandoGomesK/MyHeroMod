sampler uImage0 : register(s0);
float uOpacity;
float uTime;
float2 uScreenResolution;

float4 SandevistanShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 original = tex2D(uImage0, coords);

    // 1. Aberração cromática (verde reaproveita o original)
    float r = tex2D(uImage0, coords + float2(0.006, 0.0)).r;
    float b = tex2D(uImage0, coords - float2(0.006, 0.0)).b;
    float3 rgb = float3(r, original.g, b);

    // 2. Cinza -> verde neon
    float grey = dot(rgb, float3(0.299, 0.587, 0.114));
    float3 cyber = lerp(float3(0.0, 0.05, 0.02), float3(0.15, 1.0, 0.45), saturate(grey * 1.6));

    // 3. Scanlines com rolagem (onda triangular)
    float scan = abs(frac(coords.y * uScreenResolution.y * 0.5 + uTime * 0.6) * 2.0 - 1.0);
    cyber *= 0.7 + 0.3 * scan;

    // 4. Vinheta
    float2 v = coords * (1.0 - coords.yx);
    cyber *= saturate(v.x * v.y * 15.0);

    return lerp(original, float4(cyber, original.a), uOpacity);
}

technique Technique1
{
    pass SandevistanEffect
    {
        PixelShader = compile ps_2_0 SandevistanShaderFunction();
    }
}