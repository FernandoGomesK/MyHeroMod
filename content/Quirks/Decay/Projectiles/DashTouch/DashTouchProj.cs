using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Debuffs;

namespace MyHeroMod.content.Quirks.Decay.Projectiles.DashTouch
{
    public class DashTouchProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20; 
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false; 
            Projectile.penetrate = -1; 
            Projectile.alpha = 255; 
            
            Projectile.timeLeft = 30; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            
            
            Projectile.Center = player.Center;

            
            if (Projectile.ai[0] == 0)
            {
                
                var speed = 25f; 

                player.ChangeDir(Main.MouseWorld.X > player.MountedCenter.X ? 1 : -1);
                Vector2 dashDirection = Main.MouseWorld - player.Center;
                
                if (dashDirection.Length() > 20f)
                {
                    dashDirection.Normalize();
                    player.velocity = dashDirection * speed; 
                }
            }

            
            if (Projectile.ai[0] < 15)
            {
                
                player.gravity = 0f;
                player.noFallDmg = true;
                
                
            }

            
            Projectile.ai[0]++;
        }

         public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DecayBuff>(), 300);
            Projectile.Kill();
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<DecayBuff>(), 300);
            Projectile.Kill();
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