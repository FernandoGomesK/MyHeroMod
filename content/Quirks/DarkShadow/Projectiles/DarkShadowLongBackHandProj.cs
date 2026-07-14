using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; // Necessário para o PreDraw
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.DarkShadow;
using System.Collections.Generic;
using System.IO;

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class DarkShadowLongBackHandProj : ModProjectile
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

            // Aumenta a hitbox se o CBO Arms estiver ativo
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
                    bodyCenter = p.Center + new Vector2(0f, 10f); 
                    break;
                }
            }

            if (Projectile.ai[1] == 0)
            {
                if (Vector2.Distance(Projectile.Center, bodyCenter) > 400f)
                {
                    Projectile.ai[1] = 1; 
                }
            }
            else if (Projectile.ai[1] == 1)
            {
                Vector2 returnDirection = bodyCenter - Projectile.Center;
                float distanceToBody = returnDirection.Length();

                if (distanceToBody < 20f)
                {
                    Projectile.Kill();
                    return;
                }

                returnDirection.Normalize();
                Projectile.velocity = returnDirection * 20f; 
            }

            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;

            Color shadowColor = new Color(24, 0, 33);
            for (int i = 0; i < 2; i++) 
            {
                Vector2 cordPos = Vector2.Lerp(bodyCenter, Projectile.Center, Main.rand.NextFloat());
                cordPos += Main.rand.NextVector2Circular(5f, 5f); 
                
                Dust dust = Dust.NewDustPerfect(cordPos, DustID.WhiteTorch, Vector2.Zero, 0, shadowColor);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(1.0f, 1.8f);
                if (Projectile.ai[0] == 1) dust.customData = 0; 
            }
        }
    
        // MÁGICA PARA A MÃO GRANDE FICAR ATRÁS!
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            if (darkPlayer.isCBOArmsOn)
            {
                var Path = "MyHeroMod/content/Quirks/DarkShadow/Projectiles/BigDarkShadowLongBackHandProj";
                
                Texture2D bigTexture = ModContent.Request<Texture2D>(Path).Value;
                Vector2 drawOrigin = new Vector2(bigTexture.Width * 0.5f, bigTexture.Height * 0.5f);
                Vector2 drawPos = Projectile.Center - Main.screenPosition;
                SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                Main.EntitySpriteDraw(
                    bigTexture,
                    drawPos,
                    null, 
                    Projectile.GetAlpha(lightColor), // Aplica a transparência corretamente!
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