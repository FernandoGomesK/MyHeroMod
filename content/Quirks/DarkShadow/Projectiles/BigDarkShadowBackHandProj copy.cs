using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; // Necessário para o PreDraw
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.DarkShadow;
using System.Collections.Generic;

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class BigDarkShadowBackHandProj : ModProjectile
    {
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }
        
        public override void SetDefaults()
        {
            Projectile.width = 50; 
            Projectile.height = 50;
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

            if (player.dead || !player.active || !darkPlayer.isCBOArmsOn)
            {
                Projectile.Kill();
                return;
            }

            // Define a transparência: 255 = invisível, 0 = visível
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

            Vector2 hoverPosition = player.Center + new Vector2(+25f * player.direction, -25);
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
            
            // Só desenha a poeira se o projétil estiver visível!
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

        public override bool PreDraw(ref Color lightColor)
        {
            // Se o projétil estiver totalmente transparente (atacando), não gaste performance desenhando!
            if (Projectile.alpha == 255)
                return false;

            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            // Exemplo de como você faria o PreDraw aqui (caso essa seja uma mão base que vira Big)
            // Se essa classe JÁ É a Big, você não precisa do PreDraw, a menos que queira adicionar efeitos!
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value; // Pega a textura padrão deste projétil
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Main.EntitySpriteDraw(
                texture,
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
    }
}