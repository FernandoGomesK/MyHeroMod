using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;

namespace MyHeroMod.content.Quirks.Gearshift.Visuals
{
    public class TransmissionLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            var ModPlayer = drawInfo.drawPlayer.GetModPlayer<GearshiftPlayer>();

            return ModPlayer.ActivationTimer > 0 && ModPlayer.GearActivation;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (!ModContent.HasAsset("MyHeroMod/Assets/GearshiftTransmission")) return;

            Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/GearshiftTransmission").Value;
            var ModPlayer = drawInfo.drawPlayer.GetModPlayer<GearshiftPlayer>();

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

            Lighting.AddLight(drawInfo.Center, Color.Cyan.ToVector3() * 1.5f);

            DrawData drawData = new DrawData(
                texture,
                position,   
                sourceRectangle,
                Color.White ,
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