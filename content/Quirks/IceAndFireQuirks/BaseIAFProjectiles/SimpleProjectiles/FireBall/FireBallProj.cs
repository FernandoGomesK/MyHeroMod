using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles.SimpleProjectiles.FireBall
{
    
    public class FireBallProj : ModProjectile
    {
        
        public string dustcolor = "Torch";
        public override void SetDefaults()
        {
            Projectile.width = 60; 
            Projectile.height = 60;
            
           
            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 120; 
            
            
            Projectile.alpha = 255; 
            Projectile.ignoreWater = false; 
            Projectile.tileCollide = false; 
            
            
            
            
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 4.0f);
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
        
            for (int i = 0; i < 4; i++) 
            {
                int dustIndex = Dust.NewDust(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height, 
                    DustID.Torch, 
                    Projectile.velocity.X * 0.2f, 
                    Projectile.velocity.Y * 0.2f, 
                    100, 
                    default, 
                    5f 
                );
                
                Main.dust[dustIndex].noGravity = true; 
                Main.dust[dustIndex].velocity *= 1.5f;
                Main.dust[dustIndex].velocity += Projectile.velocity * 0.5f;
            }

            
            if (Projectile.width < 100) 
            {
                Projectile.width += 2;
                Projectile.height += 2;
            }
            

            
            Projectile.velocity *= 0.99f; 
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        
            target.AddBuff(BuffID.OnFire, 180); 
        }

        
    }

    
}