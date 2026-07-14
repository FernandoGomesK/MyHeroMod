using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; // Required for PreDraw and Texture2D
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.DarkShadow; // Ensure this is here to access DarkShadowPlayer

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class DarkShadowBodyProj : ModProjectile
    {
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

            if (player.dead || !player.active || !darkPlayer.isDarkShadowOn || darkPlayer.isBlackAbyssOn)
            {
                Projectile.Kill();
                return;
            }

            Projectile.timeLeft = 2; 

            // 1. DYNAMIC POSITION AND HITBOX
            float offsetX = -50f;
            float offsetY = -30f;

            if (darkPlayer.isMediumDarkShadowOn)
            {
                Projectile.width = 40; 
                Projectile.height = 40;
                offsetX = -80f; // Pushes it further back
            }
            else
            {
                // Resets defaults if it turns back to daytime
                Projectile.width = 28; 
                Projectile.height = 28;
            }

            Vector2 hoverPosition = player.Center + new Vector2(offsetX * player.direction, offsetY);

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

            // 2. DYNAMIC DUST DENSITY
            int dustAmount = darkPlayer.isMediumDarkShadowOn ? 8 : 5; // Thicker cord for the bigger body

            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 cordPos = Vector2.Lerp(playerPoint, darkShadowTail, Main.rand.NextFloat());
                cordPos += Main.rand.NextVector2Circular(8f, 8f); 
                
                Dust dust = Dust.NewDustPerfect(cordPos, DustID.Shadowflame, Vector2.Zero, 0, shadowColor);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(1.2f, 2.0f);
                dust.velocity = Main.rand.NextVector2Circular(0.5f, 0.5f);
            }
        }

        // 3. PREDRAW FOR TEXTURE SWAPPING
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            if (darkPlayer.isMediumDarkShadowOn)
            {
                var Path = "MyHeroMod/content/Quirks/DarkShadow/Projectiles/MediumDarkShadowBodyProj"; 
                Texture2D mediumTexture = ModContent.Request<Texture2D>(Path).Value;

                Vector2 drawOrigin = new Vector2(mediumTexture.Width * 0.5f, mediumTexture.Height * 0.5f);
                Vector2 drawPos = Projectile.Center - Main.screenPosition;
                SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                Main.EntitySpriteDraw(
                    mediumTexture,
                    drawPos,
                    null, 
                    Projectile.GetAlpha(lightColor), 
                    Projectile.rotation,
                    drawOrigin,
                    Projectile.scale,
                    effects,
                    0
                );

                return false; // Hides the small base body
            }

            return true; // Draws the small base body normally during the day
        }
    }
}