using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; // Necessário para o PreDraw
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class DarkShadowBackHandProj : ModProjectile
    {
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }

        public override void SetDefaults()
        {
            // Começa com os valores padrões (pequenos)
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
            // O projétil morre apenas se NEM o Dark Shadow normal NEM os braços do CBO estiverem ativos
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

            // 2. LÓGICA DE POSIÇÃO E HITBOX
            float hoverX = -8f;
            float hoverY = -20f;

            if (darkPlayer.isCBOArmsOn)
            {
                // Modo Black Abyss: Braços gigantes na FRENTE do jogador
                Projectile.width = 50;
                Projectile.height = 50;
                hoverX = +25f; 
                hoverY = -25f;
            }
            else if (darkPlayer.isMediumDarkShadowOn)
            {
                // Modo Noite (Médio): Braços gigantes ATRÁS do jogador (posição padrão)
                Projectile.width = 50;
                Projectile.height = 50;
                hoverX = -8f; 
                hoverY = -20f;
            }
            else
            {
                // Modo Dia (Normal): Braços pequenos ATRÁS do jogador
                Projectile.width = 26;
                Projectile.height = 16;
                hoverX = -8f; 
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

            // 3. CORDÃO UMBRAL
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