using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th;

namespace MyHeroMod.content.Quirks.OFA9th.Visuals
{
    public class RedVeinLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            var ModPlayer = drawInfo.drawPlayer.GetModPlayer<OneForAll9thPlayer>();

            return ModPlayer.ActivationTimer > 0;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (!ModContent.HasAsset("MyHeroMod/Assets/FullCowlingVeins")) return;

            Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/FullCowlingVeins").Value;
            var ModPlayer = drawInfo.drawPlayer.GetModPlayer<OneForAll9thPlayer>();

            int totalframes = 4;
            int timer = ModPlayer.ActivationTimer;
            int MaxTime = ModPlayer.ActivationMaxTime;

            int frameDuration = MaxTime / totalframes;
            if (frameDuration < 1) frameDuration = 1;

            int currentFrame = (timer / frameDuration);
            if (currentFrame >= totalframes) currentFrame = totalframes - 1;

            int frameHeight = texture.Height / totalframes;
            Rectangle sourceRectangle = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);
            Vector2 position = drawInfo.Center - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y + drawInfo.drawPlayer.gfxOffY);

            DrawData drawData = new DrawData(
                texture,
                position,
                sourceRectangle,
                Color.White * 0.8f,
                drawInfo.drawPlayer.fullRotation,
                new Vector2(texture.Width / 2f, frameHeight / 2f),
                1f,
                drawInfo.playerEffect,
                0
            );

            drawInfo.DrawDataCache.Add(drawData);


           
        }

    }
}