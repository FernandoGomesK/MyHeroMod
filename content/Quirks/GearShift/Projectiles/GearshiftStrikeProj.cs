using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.Gearshift.Projectiles
{
    public class GearshiftStrikeProj : ModProjectile
    {
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
                var speed = 50f; 
                Vector2 targetPos = Main.MouseWorld;
                Vector2 dir = targetPos - player.Center;
                float distance = dir.Length();
                
                
                float maxDist = 300f; 
                if (distance > maxDist)
                {
                    dir.Normalize();
                    dir *= maxDist;
                    distance = maxDist;
                }

                Vector2 safePos = player.Center;
                float stepSize = 16f; 
                bool hitWall = false;

                
                for (float i = 0; i < distance; i += stepSize)
                {
                    Vector2 checkPos = player.Center + Vector2.Normalize(dir) * i;
                    if (Collision.SolidCollision(checkPos - new Vector2(player.width/2, player.height/2), player.width, player.height))
                    {
                        hitWall = true;
                        break; 
                    }
                    safePos = checkPos; 
                }

                
                Vector2 startPos = player.Center;
                int dustCount = (int)(Vector2.Distance(startPos, safePos) / 5); 
                for (int i = 0; i < dustCount; i++)
                {
                    Vector2 dustPos = Vector2.Lerp(startPos, safePos, (float)i / dustCount);
                    int d = Dust.NewDust(dustPos, 0, 0, DustID.FireworkFountain_Blue, 0, 0, 100, default, 1.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 0.5f;
                }

                
                player.Center = safePos;
                
                if (hitWall) 
                {
                    player.velocity = -Vector2.Normalize(dir) * 2f; 
                }
                else
                {
                    
                    player.velocity = Vector2.Normalize(dir) * speed;
                }

                
                player.SetImmuneTimeForAllTypes(20);
            }

            
            if (Projectile.ai[0] < 15)
            {
                player.gravity = 0f;
                player.noFallDmg = true;
                player.velocity *= 0.90f; 
                
               
                int trailDust = Dust.NewDust(player.position, player.width, player.height, DustID.Electric, 0, 0, 100, default, 1f);
                Main.dust[trailDust].noGravity = true;
            }

            Projectile.ai[0]++;
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