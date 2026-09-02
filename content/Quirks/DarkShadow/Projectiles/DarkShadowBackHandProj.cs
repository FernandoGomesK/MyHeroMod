using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class DarkShadowBackHandProj : ModProjectile
    {
        Color shadowColor = new Color(24, 0, 33);
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }

        public override void SetDefaults()
        {
            Projectile.width = 26; 
            Projectile.height = 16;
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

            // 1. CONDIÇÃO DE MORTE ATUALIZADA
            if (player.dead || !player.active || (!darkPlayer.isDarkShadowOn && !darkPlayer.isCBOArmsOn))
            {
                Projectile.Kill();
                return;
            }

            if (darkPlayer.isBackHandAttacking)
            {
                Projectile.alpha = 255; 
                return; 
            }
            else
            {
                Projectile.alpha = 0; 
            }

            Projectile.timeLeft = 2;

            // --- NOVO: ENCONTRAR O CORPO PRIMEIRO ---
            Vector2 anchorPosition = player.Center;
            int anchorDirection = player.direction;
            
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<DarkShadowBodyProj>())
                {
                    anchorPosition = p.Center + new Vector2(0f, 10f); // Usa a posição do corpo
                    anchorDirection = p.spriteDirection; // Vira para onde o corpo está olhando
                    break; 
                }
            }

            // 2. LÓGICA DE POSIÇÃO E HITBOX
            float hoverX = -8f;
            float hoverY = -20f;

            if (darkPlayer.isCBOArmsOn)
            {
                Projectile.width = 50;
                Projectile.height = 50;
                hoverX = +25f; 
                hoverY = -25f;
            }
            else if (darkPlayer.isMediumDarkShadowOn)
            {
                Projectile.width = 50;
                Projectile.height = 50;
                hoverX = -8f; 
                hoverY = -20f;
            }
            else
            {
                Projectile.width = 26;
                Projectile.height = 16;
                hoverX = -8f; 
                hoverY = -20f;
            }

            // A mão usa o anchorPosition (Corpo) em vez do player.Center
            Vector2 hoverPosition = anchorPosition + new Vector2(hoverX * anchorDirection, hoverY);
            Vector2 direction = hoverPosition - Projectile.Center;
            float distance = direction.Length();

            float maxAllowedRange = (darkPlayer.darkShadowBodyRange > 0 ? darkPlayer.darkShadowBodyRange : 120f) + 30f;

            // Teleport Check: Ensures the hand instantly catches up to the anchor
            if (distance > 2000f) // Safeguard for Magic Mirrors/Recalls across the map
            {
                Projectile.Center = hoverPosition;
                Projectile.velocity = Vector2.Zero;
            }
            else if (distance > maxAllowedRange)
            {
                for (int i = 0; i < 3; i++) 
                {
                    int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 100, shadowColor, 1.5f);
                    if (dustIndex >= 0)
                    {
                        Dust dust = Main.dust[dustIndex];
                        dust.noGravity = true;
                        dust.velocity *= 0.3f; 
                    }
                }
                
                direction.Normalize();
                
                
                float excessDistance = distance - maxAllowedRange;
                
               
                float dynamicSpeed = 25f + (excessDistance * 0.15f);
                
                
                Projectile.velocity = (Projectile.velocity * 2f + direction * dynamicSpeed) / 3f;
            }
            else if (distance > 10f)
            {
                direction.Normalize();
                Projectile.velocity = (Projectile.velocity * 10f + direction * 8f) / 11f; 
            }
            else
            {
                Projectile.velocity *= 0.8f; 
            }

            Projectile.spriteDirection = anchorDirection;

            // 3. CORDÃO UMBRAL
            
            if (Projectile.alpha == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 cordPos = Vector2.Lerp(anchorPosition, Projectile.Center, Main.rand.NextFloat());
                    cordPos += Main.rand.NextVector2Circular(4f, 4f); 
                    
                    Dust dust = Dust.NewDustPerfect(cordPos, DustID.WhiteTorch, Vector2.Zero, 0, shadowColor);
                    dust.noGravity = true;
                    dust.scale = Main.rand.NextFloat(0.8f, 1.5f);
                    dust.velocity = Main.rand.NextVector2Circular(0.2f, 0.2f);
                    dust.customData = 0; 
                }
            }
        }

        // 4. TROCA DE TEXTURA NO PREDRAW
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.alpha == 255)
                return false;

            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            if (darkPlayer.isCBOArmsOn || darkPlayer.isMediumDarkShadowOn)
            {
                var Path = "MyHeroMod/content/Quirks/DarkShadow/Projectiles/BigDarkShadowBackHandProj"; 
                Texture2D bigTexture = ModContent.Request<Texture2D>(Path).Value;

                Vector2 drawOrigin = new Vector2(bigTexture.Width * 0.5f, bigTexture.Height * 0.5f);
                Vector2 drawPos = Projectile.Center - Main.screenPosition;
                SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                Main.EntitySpriteDraw(
                    bigTexture,
                    drawPos,
                    null, 
                    Projectile.GetAlpha(lightColor), 
                    Projectile.rotation,
                    drawOrigin,
                    Projectile.scale,
                    effects,
                    0
                );

                return false; 
            }

            return true; 
        }
    }
}