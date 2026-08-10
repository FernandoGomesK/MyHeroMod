using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles
{
    public abstract class BaseDashProj : ModProjectile
    {
       
        protected virtual float DashSpeed => 30f; 
        protected virtual int DashDuration => 15; 
        protected virtual float HitboxThickness => 80f; 
        protected virtual int ImmuneTime => 20; 

        protected Vector2 startPos;

        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj"; // Invisible

        public override void SetDefaults()
        {
            Projectile.width = (int)HitboxThickness; 
            Projectile.height = (int)HitboxThickness;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false; 
            Projectile.penetrate = -1; 
            Projectile.hide = true;
            Projectile.timeLeft = DashDuration; 
            
            
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = DashDuration + 5; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            
            if (Projectile.ai[0] == 0)
            {
                startPos = player.Center; 

                Vector2 dir = Main.MouseWorld - player.Center;
                if (dir != Vector2.Zero)
                {
                    dir.Normalize(); 
                }

                
                player.velocity = dir * DashSpeed;
            }

            
            Projectile.Center = player.Center;

            
            player.gravity = 0f;
            player.noFallDmg = true;
            player.armorEffectDrawShadow = true;
            player.SetImmuneTimeForAllTypes(ImmuneTime);

            SpawnDashVisuals(player);

            Projectile.ai[0]++;
        }

      
        protected abstract void SpawnDashVisuals(Player player);

        
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint = 0f;
                
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), startPos, Main.player[Projectile.owner].Center, HitboxThickness, ref collisionPoint))
            {
                return true;
            }
            
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.fullRotation = 0f; 
            player.velocity *= 0.5f; 
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false; 
        }
    }
}