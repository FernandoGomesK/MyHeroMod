using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.System.BaseProjectiles
{
    public abstract class BaseTrapProj : ModProjectile
    {
    
        protected virtual int TrapWidth => 30;
        protected virtual int TrapHeight => 60;
        protected virtual int TrapDuration => 180; 
        protected virtual int HitCooldown => 30; 

        public override void SetDefaults()
        {
            Projectile.width = TrapWidth;
            Projectile.height = TrapHeight; 
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = TrapDuration;  
            Projectile.tileCollide = false; 
            Projectile.ignoreWater = true;
            Projectile.hide = true; 
        
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = HitCooldown;
        }

        public override void AI()
        {
        
            if (Projectile.ai[0] < 10)
            {
                Projectile.position.Y -= 4f; 
                Projectile.ai[0]++;
            }

            SpawnTrapVisuals();
        }

        
        protected abstract void SpawnTrapVisuals();
    }
}