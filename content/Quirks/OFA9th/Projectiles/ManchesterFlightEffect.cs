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
        private static Projectile GetLaunchedController(Player player)
        {
            int type = ModContent.ProjectileType<ManchesterSmashController>();

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];

                if (proj.active && proj.type == type && proj.owner == player.whoAmI && proj.localAI[0] > 15f)
                    return proj;
            }

            return null;
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            if (!player.active || player.dead) return false;

            return GetLaunchedController(player) != null;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            sonicBoomTexture ??= ModContent.Request<Texture2D>("KhacesCore/Assets/Effects/SoundBarrier");

            Player player = drawInfo.drawPlayer;
            Projectile controller = GetLaunchedController(player);
            if (controller == null) return;

            bool usedFaJin = controller.ai[1] == 1f;
            Color tint = (usedFaJin ? Color.Red : Color.White) * 0.8f;

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
                tint,
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