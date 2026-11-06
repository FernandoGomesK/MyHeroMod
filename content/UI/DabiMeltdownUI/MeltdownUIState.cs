using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.Flight;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;

namespace MyHeroMod.content.UI.DabiMeltdownUI
{
    public class MeltdownUIState : UIState
    {
        public MeltdownBar meltdownBar;

        public override void OnInitialize()
        {
            meltdownBar = new MeltdownBar();

            meltdownBar.Width.Set(60f, 0f);
            meltdownBar.Height.Set(60f, 0f);

            Append(meltdownBar);
        }
    }

    public class MeltdownBar : BaseDraggableBar
    {
        protected override void AutoPosition()
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            int uiSlot = 0;

            if (transPlayer.HasActiveQuirk(QuirkType.Engine))
            {
                uiSlot++;
            }

            Left.Set(Main.screenWidth - 355f, 0f);
            Top.Set(10f + (uiSlot * 76f), 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var blueflamePlayer = player.GetModPlayer<BlueflamePlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.Blueflame))
                return;

            Texture2D barFrame = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/MeltdownFrame").Value;
            Texture2D barFill = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/MeltdownFill").Value;

            CalculatedStyle dimensions = GetDimensions();
            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

            float quotient = 0f;
            if (blueflamePlayer.currentExplosionCount > 0)
            {
                quotient = (float)blueflamePlayer.currentExplosionCount / blueflamePlayer.ExplosionLimit;
            }
            quotient = MathHelper.Clamp(quotient, 0f, 1f);

            spriteBatch.Draw(barFrame, drawPos, Color.White);

            int fillHeight = (int)(barFill.Height * quotient);
            int emptySpace = barFill.Height - fillHeight;

            Rectangle fillRect = new Rectangle(0, emptySpace, barFill.Width, fillHeight);
            Vector2 fillDrawPos = drawPos + new Vector2(0, emptySpace);

            spriteBatch.Draw(barFill, fillDrawPos, fillRect, Color.White);

            string text = $"{(int)blueflamePlayer.currentExplosionCount} / {blueflamePlayer.ExplosionLimit}";
            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - 20f, barFrame.Height + 5f);
            Utils.DrawBorderString(spriteBatch, text, textPos, Color.Cyan, 0.8f);
        }
    }
}