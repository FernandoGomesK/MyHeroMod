using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.GearShift.Projectiles
{
    public class GearshiftStrikeProj : ModProjectile
    {
        
        public Vector2 startPos;
        public Vector2 endPos;

        public override void SetDefaults()
        {
            
            Projectile.width = 80; 
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false; 
            Projectile.penetrate = -1; 
            Projectile.alpha = 255; 
            Projectile.timeLeft = 30; 
            
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;

            
            if (Projectile.ai[0] == 0)
            {
                startPos = player.Center; 

                var speed = 50f; 
                Vector2 targetPos = Main.MouseWorld;
                Vector2 dir = targetPos - player.Center;
                float distance = dir.Length();
                
                
                if (distance != 0)
                {
                    dir.Normalize(); 
                }

            
                float maxDist = 300f; 
                if (distance > maxDist)
                {
                    distance = maxDist;
                }

                Vector2 safePos = player.Center;
                float stepSize = 16f; 
                bool hitWall = false;

                for (float i = 0; i < distance; i += stepSize)
                {
                    
                    Vector2 checkPos = player.Center + (dir * i);
                    
                    checkPos.Y -= 4f; 
                    
                    if (Collision.SolidCollision(checkPos - new Vector2(player.width/2, player.height/2), player.width, player.height))
                    {
                        hitWall = true;
                        break; 
                    }
                    
                    safePos = player.Center + (dir * i); 
                }

                endPos = safePos; 

                
                int dustCount = (int)(Vector2.Distance(startPos, safePos) / 5); 
                for (int i = 0; i < dustCount; i++)
                {
                    Vector2 dustPos = Vector2.Lerp(startPos, safePos, (float)i / dustCount);
                    int d = Dust.NewDust(dustPos - new Vector2(10, 10), 20, 20, DustID.FireworkFountain_Blue, 0, 0, 100, default, 1.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 0.5f;
                }

                player.Center = safePos;
                
                if (hitWall) 
                {
                    
                    player.velocity = -dir * 2f; 
                }
                else
                {
                  
                    player.velocity = dir * speed;
                }

                player.SetImmuneTimeForAllTypes(20);
            }

            
            if (Projectile.ai[0] < 15)
            {
                player.gravity = 0f;
                player.noFallDmg = true;
                player.velocity *= 0.90f; 

                player.armorEffectDrawShadow = true;
                
               
                int trailDust = Dust.NewDust(player.position, player.width, player.height, DustID.FireworkFountain_Blue, 0f, 0f, 100, default, 1.2f);
                Main.dust[trailDust].noGravity = true;
                
                
                Main.dust[trailDust].velocity *= 0.1f;


            }

            Projectile.ai[0]++;
        }

        // CUSTOM HITBOX LOGIC
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            // Only apply the massive line hitbox while the player is actively dashing/teleporting
            if (Projectile.ai[0] < 15)
            {
                float collisionPoint = 0f;
                
                // Draws an 80-pixel thick invisible line between startPos and endPos. 
                // If the enemy touches any part of this line, it counts as a hit!
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), startPos, endPos, 80f, ref collisionPoint))
                {
                    return true;
                }
            }
            
            // Fallback to normal collision if the dash is over
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.fullRotation = 0f; 
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false; 
        }
    }
}