using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.Buffs;

namespace MyHeroMod.content.Quirks.Decay.Projectiles.DashTouch
{
    public class DashTouchProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10; 
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false; 
            Projectile.penetrate = -1; 
            Projectile.alpha = 255; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            var speed = 5f;

            

            
            


            Projectile.Center = player.Center;

        
        

            player.ChangeDir(Main.MouseWorld.X > player.MountedCenter.X ? 1 : -1);

            Vector2 dashDirection = Main.MouseWorld - player.Center;
            float distanceToMouse = dashDirection.Length();
            
            
            if (distanceToMouse > 20f)
            {
                dashDirection.Normalize();
                float force = speed; 
                player.velocity = dashDirection * force; 

                
                player.fullRotation = player.velocity.ToRotation() + MathHelper.PiOver2;
                player.fullRotationOrigin = player.Size / 2; 
            }
            else
            {
                
                player.velocity *= 0.8f; 
            }
        }
        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            
            
            player.ClearBuff(ModContent.BuffType<CruisingBuff>());
            
        
            player.fullRotation = 0f; 
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false; 
        }
    }
}