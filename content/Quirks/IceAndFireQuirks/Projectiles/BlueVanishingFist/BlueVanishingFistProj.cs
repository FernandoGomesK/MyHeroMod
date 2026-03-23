using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.BlueVanishingFist
{
    public class BlueVanishingFistProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.alpha = 255;
            Projectile.light = 1.0f;
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 4.0f);
                Main.dust[idx].noGravity = true;
                Main.dust[idx].velocity *= 3f;

                Main.dust[idx].velocity += Projectile.velocity * 0.5f;
            
            } 
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 4.5f);
            }
            for (int i = 0; i < 5; i++)
            {

                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ash, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f );
            }
        }   
        public override void AI()
        {

            for (int i = 0; i < 4; i++) // Pode aumentar para 3 se quiser mais denso
            {
                int dustIndex = Dust.NewDust(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height, 
                    DustID.BlueTorch, // ID do fogo padrão (6)
                    Projectile.velocity.X * 0.2f, 
                    Projectile.velocity.Y * 0.2f, 
                    100, 
                    default, 
                    5f // Tamanho grande
                );
                
                Main.dust[dustIndex].noGravity = true; // Fogo flutua
                Main.dust[dustIndex].velocity *= 1.5f; // Fogo se expande um pouco
                Main.dust[dustIndex].velocity += Projectile.velocity * 0.5f; // Segue o tiro
            }

            Projectile.rotation = Projectile.velocity.ToRotation();
            
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.5f);
            }
            if (Main.rand.NextBool(7))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f);
            }

        }

        
    }

    
}