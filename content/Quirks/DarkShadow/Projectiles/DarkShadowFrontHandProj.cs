using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; // Necessário para o PreDraw
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class DarkShadowFrontHandProj : ModProjectile
    {
        public override void SetDefaults()
        {
            // Tamanho inicial padrão (mão pequena)
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

            // 1. CONDIÇÃO DE MORTE UNIFICADA
            if (player.dead || !player.active || (!darkPlayer.isDarkShadowOn && !darkPlayer.isCBOArmsOn))
            {
                Projectile.Kill();
                return;
            }

            if (darkPlayer.isFrontHandAttacking)
            {
                Projectile.alpha = 255;
                return;
            }
            else
            {
                Projectile.alpha = 0; 
            }

            Projectile.timeLeft = 2;

            // 2. LÓGICA DE POSIÇÃO E HITBOX DINÂMICAS
            float hoverX = -18f;
            float hoverY = -20f;

            if (darkPlayer.isCBOArmsOn)
            {
                // Modo Black Abyss: Braços gigantes avançados na FRENTE
                Projectile.width = 50; 
                Projectile.height = 50;
                hoverX = +30f; 
                hoverY = -20f;
            }
            else if (darkPlayer.isMediumDarkShadowOn)
            {
                // Modo Noite (Médio): Braços gigantes na posição normal da frente
                Projectile.width = 50; 
                Projectile.height = 50;
                hoverX = -18f; 
                hoverY = -20f;
            }
            else
            {
                // Modo Dia (Normal)
                Projectile.width = 26; 
                Projectile.height = 16;
                hoverX = -18f; 
                hoverY = -20f;
            }

            Vector2 hoverPosition = player.Center + new Vector2(hoverX * player.direction, hoverY);
            Vector2 direction = hoverPosition - Projectile.Center;
            float distance = direction.Length();

            if (distance > 10f)
            {
                direction.Normalize();
                Projectile.velocity = (Projectile.velocity * 10f + direction * 8f) / 11f; 
            }
            else
            {
                Projectile.velocity *= 0.8f; 
            }

            Projectile.spriteDirection = player.direction;

            // 3. CORDÃO UMBRAL CUSTOMIZADO (#180021)
            Vector2 cordStartPos = player.Center; 
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<DarkShadowBodyProj>())
                {
                    cordStartPos = p.Center + new Vector2(0f, 10f); 
                    break; 
                }
            }

            Color shadowColor = new Color(24, 0, 33);
            
            if (Projectile.alpha == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 cordPos = Vector2.Lerp(cordStartPos, Projectile.Center, Main.rand.NextFloat());
                    cordPos += Main.rand.NextVector2Circular(4f, 4f); 
                    
                    Dust dust = Dust.NewDustPerfect(cordPos, DustID.WhiteTorch, Vector2.Zero, 0, shadowColor);
                    dust.noGravity = true;
                    dust.scale = Main.rand.NextFloat(0.8f, 1.5f);
                    dust.velocity = Main.rand.NextVector2Circular(0.2f, 0.2f);
                    dust.customData = 0;
                }
            }
        }

        // 4. TROCA DE TEXTURA COM PREDRAW
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.alpha == 255)
                return false;

            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            // Se o CBO Arms ou o Medium Mode estiverem ativos, carrega a textura grande
            if (darkPlayer.isCBOArmsOn || darkPlayer.isMediumDarkShadowOn)
            {
                var Path = "MyHeroMod/content/Quirks/DarkShadow/Projectiles/BigDarkShadowFrontHandProj";
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

                return false; // Esconde a mão base pequena
            }

            return true; // Desenha a textura original se estiver de dia/forma base
        }
    }
}