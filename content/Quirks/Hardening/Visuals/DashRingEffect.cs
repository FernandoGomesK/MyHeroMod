using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Hardening.Projectiles
{
    public class DashRingProj : ModProjectile
    {
        // Point to the exact texture path from your original draw layer
        public override string Texture => "KhacesCore/Assets/Effects/RingEffect";

        public override void SetStaticDefaults()
        {
            // Tell Terraria this sprite sheet has 4 vertical animation frames
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 16; 
            Projectile.alpha = 50;    
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            
            Projectile.Center = player.Center;

         
            Projectile.rotation = player.velocity.ToRotation() + MathHelper.PiOver2;

          
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.Kill(); 
                }
            }
        }

      
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * ((255 - Projectile.alpha) / 255f);
        }
    }
}