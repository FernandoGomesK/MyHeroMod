using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Projectiles.Base
{
    public abstract class BaseSimpleProj : ModProjectile
    {
        public virtual float Speed => 10f; 
        
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
        }

    
        public override void OnKill(int timeLeft) 
        { 
            
        }

        public override void AI() 
        { 
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}