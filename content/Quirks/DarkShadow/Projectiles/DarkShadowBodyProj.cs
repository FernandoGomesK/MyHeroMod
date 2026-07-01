using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class DarkShadowBodyProj : ModProjectile
    {
       
        // public override string Texture => "MyHeroMod/Assets/Projectiles/DarkShadowBody"; 

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            
            if (player.dead || !player.active || !darkPlayer.isDarkShadowOn)
            {
                Projectile.Kill();
                return;
            }

            
            Projectile.timeLeft = 2; 

            
            Vector2 hoverPosition = player.Center + new Vector2(-50f * player.direction, -30f);

            
            Vector2 direction = hoverPosition - Projectile.Center;
            float distance = direction.Length();

            
            if (distance > 10f)
            {
                direction.Normalize();
                
                Projectile.velocity = (Projectile.velocity * 10f + direction * 6f) / 11f; 
            }
            else
            {
                
                Projectile.velocity *= 0.8f; 
            }

            
            Projectile.spriteDirection = player.direction; 

            
            Vector2 playerPoint = player.Center;

            
            float tailX = Projectile.spriteDirection == 1 ? 0f : Projectile.width;
            
           
            Vector2 darkShadowTail = Projectile.position + new Vector2(tailX, Projectile.height);

            Color shadowColor = new Color(24, 0, 33);

            
            for (int i = 0; i < 5; i++)
            {
                
                Vector2 cordPos = Vector2.Lerp(playerPoint, darkShadowTail, Main.rand.NextFloat());
                
               
                cordPos += Main.rand.NextVector2Circular(8f, 8f); 
                
                Dust dust = Dust.NewDustPerfect(cordPos, DustID.Shadowflame, Vector2.Zero, 0, shadowColor);
                dust.noGravity = true;
                
                
                dust.scale = Main.rand.NextFloat(1.2f, 2.0f);
                
                
                dust.velocity = Main.rand.NextVector2Circular(0.5f, 0.5f);
            }
        }
    }
}