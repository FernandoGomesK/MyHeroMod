using KhacesCore.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{
    public class ManchesterFlightEffect : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Wings);

        private static Asset<Texture2D> sonicBoomTexture;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            if (!player.active || player.dead) return false;

            int type = ModContent.ProjectileType<ManchesterSmashController>();

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];

                // localAI[0] > 15 = already launched (HoverFrames = 15 on the base)
                if (proj.active && proj.type == type && proj.owner == player.whoAmI && proj.localAI[0] > 15f)
                    return true;
            }

            return false;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            sonicBoomTexture ??= ModContent.Request<Texture2D>("KhacesCore/Assets/Effects/SoundBarrier");

            Player player = drawInfo.drawPlayer;
            Texture2D texture = sonicBoomTexture.Value;

            int frameCount = 4;
            int frameSpeed = 4;
            int currentFrame = (int)(Main.GameUpdateCount / frameSpeed) % frameCount;

            int frameHeight = texture.Height / frameCount;
            Rectangle sourceRect = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

            Vector2 position = player.Center - Main.screenPosition;

            
            Vector2 origin = new Vector2(texture.Width / 2f, 16f);

            DrawData drawData = new DrawData(
                texture,
                new Vector2((int)position.X, (int)position.Y),
                sourceRect,
                Color.Red * 0.8f,
                MathHelper.Pi,
                origin,
                1f,
                drawInfo.playerEffect,
                0
            );

            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}