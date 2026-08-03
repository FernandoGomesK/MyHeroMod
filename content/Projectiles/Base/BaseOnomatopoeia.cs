using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Projectiles.Base
{
    public abstract class BaseOnomatopoeia : ModProjectile
    {   
    
        public virtual int Duration => 60; 
        public virtual Color TextColor => Color.White;
        
        public override void SetDefaults()
        {
            
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1; 
            Projectile.tileCollide = false; 
            Projectile.ignoreWater = true;
            Projectile.timeLeft = Duration;
            Projectile.alpha = 0; 
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, TextColor.ToVector3());
            if (Projectile.localAI[0] == 0f)
            {
                
                Projectile.rotation = Main.rand.NextFloat(-0.4f, 0.4f);
                Projectile.localAI[0] = 1f;
            }

            
            Projectile.velocity.Y = -0.5f;
            Projectile.velocity.X *= 0.95f; 

            
            if (Projectile.timeLeft < 20)
            {
                Projectile.alpha += 12; 
            }
        }
    }
}