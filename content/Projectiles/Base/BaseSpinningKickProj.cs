using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MyHeroMod.content.Projectiles.Base
{ 
    public abstract class BaseSpinningKickProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj"; 

        
        protected virtual float DashSpeed => 25f;       
        protected virtual int HoverFrames => 15;        
        
        
        protected virtual bool CanSteer => true;  
        protected virtual float TurnSpeed => 1f;  

        public override void SetDefaults()
        {
            Projectile.width = 80; 
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true; 
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 120; 
            Projectile.alpha = 255; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = player.Center;
            player.heldProj = Projectile.whoAmI;

            
            if (Projectile.ai[0] < HoverFrames)
            {
                Projectile.ai[0]++;
                Projectile.width = 5;
                Projectile.height = 5;

                player.velocity.Y = -5f; 
                player.velocity.X *= 0.9f; 
                
                // Começa a girar
                player.fullRotation += 0.4f * player.direction;
                player.fullRotationOrigin = player.Size / 2;
                
                SpawnHoverDust(player); 
            }
            
            else
            {
                
                if (Projectile.ai[0] == HoverFrames || CanSteer)
                {
                    Vector2 targetDirection = Main.MouseWorld - player.Center;
                    targetDirection.Normalize();
                    Vector2 targetVelocity = targetDirection * DashSpeed;

                    
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, targetVelocity, TurnSpeed);
                    
                    
                    if (Projectile.ai[0] == HoverFrames)
                    {
                        player.direction = Projectile.velocity.X > 0 ? 1 : -1;
                        SoundEngine.PlaySound(SoundID.Item14, player.position); 
                        Projectile.ai[0]++;
                    }
                }

                
                player.velocity = Projectile.velocity;

                
                player.fullRotation += 0.8f * player.direction;
                player.fullRotationOrigin = player.Size / 2f;

                SpawnDashDust(player); 
            }
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.velocity = Vector2.Zero;
            player.fullRotation = 0f; 
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position); 

            SpawnExplosionDust(Projectile.Center); 
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => Projectile.Kill();
        public override bool OnTileCollide(Vector2 oldVelocity) => true;

        public virtual void SpawnHoverDust(Player player) { }
        public virtual void SpawnDashDust(Player player) { }
        public virtual void SpawnExplosionDust(Vector2 position) { }
    }
}