using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Gearshift;

namespace MyHeroMod.content.Systems
{
    /// <summary>
    /// Garante que os projéteis guardem posições antigas (oldPos/oldRot).
    /// Isso precisa ser feito no carregamento, antes de qualquer projétil ser criado.
    /// </summary>
    public class GearshiftTrailSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            for (int i = 0; i < ProjectileLoader.ProjectileCount; i++)
            {
                // Só aumenta onde o rastro é menor; não mexe no TrailingMode de quem já usa
                if (ProjectileID.Sets.TrailCacheLength[i] < 10)
                    ProjectileID.Sets.TrailCacheLength[i] = 10;
            }
        }
    }

    public class GearshiftGlobalProjectile : GlobalProjectile
    {
        private static bool GearshiftActive(Projectile projectile)
        {
            if (!projectile.friendly || projectile.owner < 0 || projectile.owner >= Main.maxPlayers)
                return false;

            Player player = Main.player[projectile.owner];
            return player.active
                && !player.dead
                && player.GetModPlayer<GearshiftPlayer>().isGearshiftBuffActive;
        }

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (!GearshiftActive(projectile))
                return true;

            Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
            Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            Vector2 origin = frame.Size() / 2f;
            SpriteEffects effects = projectile.spriteDirection == -1
                ? SpriteEffects.FlipHorizontally
                : SpriteEffects.None;

            int length = projectile.oldPos.Length;

            for (int i = 0; i < length; i++)
            {
                if (projectile.oldPos[i] == Vector2.Zero)
                    continue;


                Vector2 drawPos = projectile.oldPos[i] + projectile.Size / 2f
                                  - Main.screenPosition
                                  + new Vector2(0f, projectile.gfxOffY);


                float fade = (length - i) / (float)length;
                Color color = Color.Cyan * fade * 0.6f;

                Main.EntitySpriteDraw(
                    texture,
                    drawPos,
                    frame,
                    color,
                    projectile.oldRot[i],
                    origin,
                    projectile.scale,
                    effects,
                    0
                );
            }
            return true;
        }

        public override void PostAI(Projectile projectile)
        {
            if (!GearshiftActive(projectile))
                return;

            if (Main.rand.NextBool(5)) 
            {
                Dust d = Dust.NewDustDirect(
                    projectile.position,
                    projectile.width,
                    projectile.height,
                    DustID.Electric,
                    0f, 0f, 100,
                    Color.Cyan,
                    0.5f
                );
                d.velocity *= 0.2f;
                d.noGravity = true;
            }
        }
    }
}