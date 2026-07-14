using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; // Required for Texture2D and SpriteEffects
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.DarkShadow;

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class DarkShadowLongFrontHandProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            // 1. Update ONLY the hitbox here. No texture logic.
            // (Make sure to use capital 'P' for Projectile in tModLoader 1.4+)
            if (darkPlayer.isCBOArmsOn)
            {
                Projectile.width = 50;
                Projectile.height = 50;
            }
            
            Vector2 bodyCenter = player.Center; 
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<DarkShadowBodyProj>())
                {
                    bodyCenter = p.Center + new Vector2(0f, 10f); // A mesma âncora que criámos antes!
                    break;
                }
            }

            // 3. ESTADO 0: INDO PARA A FRENTE
            if (Projectile.ai[1] == 0)
            {
                // Se estiver muito longe do corpo (Ex: 400 pixels), muda para o estado de voltar
                if (Vector2.Distance(Projectile.Center, bodyCenter) > 400f)
                {
                    Projectile.ai[1] = 1; // Muda o estado para Voltar
                }
            }
            // 4. ESTADO 1: VOLTANDO (RETRACTING)
            else if (Projectile.ai[1] == 1)
            {
                Vector2 returnDirection = bodyCenter - Projectile.Center;
                float distanceToBody = returnDirection.Length();

                // Se chegou perto o suficiente do corpo, o ataque terminou!
                if (distanceToBody < 20f)
                {
                    Projectile.Kill();
                    return;
                }

                // Puxa a mão de volta com muita velocidade
                returnDirection.Normalize();
                Projectile.velocity = returnDirection * 20f; 
            }

            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;

            // 5. CORDÃO UMBRAL DO ATAQUE (Com a sua cor #180021)
            Color shadowColor = new Color(24, 0, 33);
            for (int i = 0; i < 2; i++) // Um pouco mais denso durante o ataque
            {
                Vector2 cordPos = Vector2.Lerp(bodyCenter, Projectile.Center, Main.rand.NextFloat());
                cordPos += Main.rand.NextVector2Circular(5f, 5f); 
                
                Dust dust = Dust.NewDustPerfect(cordPos, DustID.WhiteTorch, Vector2.Zero, 0, shadowColor);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(1.0f, 1.8f);
                if (Projectile.ai[0] == 1) dust.customData = 0; 
            }
        }

        // This method determines exactly how the projectile is drawn on screen
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            if (darkPlayer.isCBOArmsOn)
            {
                
                var Path = "MyHeroMod/content/Quirks/DarkShadow/Projectiles/BigDarkShadowLongFrontHandProj";
                Texture2D bigTexture = ModContent.Request<Texture2D>(Path).Value;
                
                
                // Calculate where the center of the texture is so it rotates properly
                Vector2 drawOrigin = new Vector2(bigTexture.Width * 0.5f, bigTexture.Height * 0.5f);
                
                // Convert world position to screen position
                Vector2 drawPos = Projectile.Center - Main.screenPosition;

                // Ensure the sprite flips correctly based on direction
                SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                // Draw the custom texture manually
                Main.EntitySpriteDraw(
                    bigTexture,
                    drawPos,
                    null, // null means draw the whole texture (no frames)
                    lightColor,
                    Projectile.rotation,
                    drawOrigin,
                    Projectile.scale,
                    effects,
                    0
                );

                // Return FALSE. This tells tModLoader: "I drew this manually, do NOT draw the default texture."
                return false; 
            }

            
            return true;
        }
    }
}