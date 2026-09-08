using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Dusts
{
    public class ElectricityDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true; 
            dust.noLight = false;   
            dust.alpha = 0;
            
            dust.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            
            int randomFrame = Main.rand.Next(3);
            dust.frame = new Rectangle(0, randomFrame * 10, 10, 10); 
        }

        public override bool Update(Dust dust)
        {
            Vector2 jitter = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
            dust.position += jitter;

            dust.position += dust.velocity;
            dust.velocity *= 0.85f; 

            if (Main.rand.NextBool(3)) 
            {
                dust.rotation += Main.rand.NextFloat(-0.5f, 0.5f);
            }

            dust.scale -= 0.04f; 
            dust.alpha += 8;

            if (dust.scale < 0.1f || dust.alpha >= 255)
            {
                dust.active = false;
            }

            if (dust.alpha < 150) 
            {
                float lightR = dust.color.R / 255f;
                float lightG = dust.color.G / 255f;
                float lightB = dust.color.B / 255f;

                if (lightR == 0f && lightG == 0f && lightB == 0f)
                {
                    lightR = 0.2f; lightG = 0.8f; lightB = 1.0f; 
                }
                
                Lighting.AddLight(dust.position, lightR * dust.scale, lightG * dust.scale, lightB * dust.scale); 
            }

            return false;
        }

        
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {

            float fade = (255 - dust.alpha) / 255f;
            
            
            Color baseColor = (dust.color == Color.Transparent || dust.color == Color.Black) ? Color.Cyan : dust.color;

       
            baseColor = Color.Lerp(baseColor, Color.White, 0.2f);

   
            return new Color(baseColor.R, baseColor.G, baseColor.B, 0) * fade;
        }
    }
}