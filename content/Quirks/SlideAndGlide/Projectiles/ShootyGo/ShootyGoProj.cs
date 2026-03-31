using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot
{
    public class ShootyGoProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32; 
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true; 
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 120; 
            Projectile.alpha = 255; // Invisível
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position); 
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