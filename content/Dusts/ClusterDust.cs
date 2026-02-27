using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Dusts
{
    public class ClusterDust: ModDust
    {

        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true; // Não cai (se quiser que caia, tire isso)
            dust.noLight = false;   // Não emite luz própria (opcional)
            dust.rotation = 0f;    // Começa reto
            dust.alpha = 0;
            
            
            
        }
        public override bool Update(Dust dust)
{
    // 1. FAZER ELA FICAR PARADA
    // Multiplicar por um número baixo (0.1f) cria uma "fricção" forte, parando ela quase na hora.
    // Se quiser que ela nasça e nem se mexa, coloque: dust.velocity = Vector2.Zero;
    

    dust.velocity *= 0.1f; 
    
    // Aplica o movimento (que será quase zero) só para não bugar colisão se houver
    dust.position += dust.velocity;

    // 2. TRAVAR A ROTAÇÃO (Não girar)
    dust.rotation = 0f; 

    // 3. NÃO ESFARELAR (Manter o tamanho fixo)
    // Nós NÃO vamos diminuir o dust.scale. O tamanho fica igual do início ao fim.

    dust.scale -= 0.05f; // Diminui 5% do tamanho por frame
            
            // Opcional: Fade de transparência (Alpha)
            // dust.alpha += 5; 

            // 4. Morrer
            if (dust.scale < 0.1f)
            {
                dust.active = false;
            }
    
    // 4. EFEITO DE FADE OUT (Sumir suavemente)
    // Aumentamos o 'alpha' (transparência). 0 = Visível, 255 = Invisível.
   dust.alpha += 5;
    if (dust.alpha >= 255) dust.active = false;

    // LUZ FORTE
    // Se quiser um brilho INTENSO, use valores acima de 1.0f.
    // Exemplo: 2.0f é uma luz muito forte.
    if (dust.alpha < 150) 
    {
        // Cor Laranja Bakugo (Muito Intensa)
        // Red = 2.0f (Super forte)
        // Green = 1.0f 
        // Blue = 0.2f
        Lighting.AddLight(dust.position, 2.0f, 1.0f, 0.2f); 
    }

    // Retorna false para dizer ao Terraria: "Não use a física padrão (que faz girar e encolher)"
    return false; 
}
public override Color? GetAlpha(Dust dust, Color lightColor)
{
    // Retorna a cor com transparência configurada
    // Usar Color.White faz ela brilhar ao máximo, mantendo a cor original do sprite se tiver.
    // O 'dust.alpha' no final permite que ela ainda desapareça suavemente (fade out).
    return new Color(255, 255, 255, 255 - dust.alpha);
}
}
}

