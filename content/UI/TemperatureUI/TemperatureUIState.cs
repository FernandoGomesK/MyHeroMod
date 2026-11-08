using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.HellFlames;

namespace MyHeroMod.content.UI.TemperatureUI
{
    public class TemperatureUIState : UIState
    {
        public TemperatureBar temperatureBar;

        public override void OnInitialize()
        {
            temperatureBar = new TemperatureBar();

            temperatureBar.Width.Set(90f, 0f);
            temperatureBar.Height.Set(28f, 0f);

            Append(temperatureBar);
        }
    }

    public class TemperatureBar : BaseDraggableBar
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

            Left.Set(Main.screenWidth - 425f, 0f);
            Top.Set(10f + (uiSlot * 76f), 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot) &&
                !transPlayer.HasActiveQuirk(QuirkType.HellFlames) &&
                !transPlayer.HasActiveQuirk(QuirkType.Blueflame))
                return;

            int currentTemp = 0;
            int maxTemp = 100;
            int minTemp = 100;

            if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                var hchh = player.GetModPlayer<HalfColdHalfHotPlayer>();
                currentTemp = hchh.Temperature;
                maxTemp = hchh.MaxTemperature;
                minTemp = Math.Abs(hchh.MinTemperature);
            }
            else if (transPlayer.HasActiveQuirk(QuirkType.Blueflame))
            {
                var blue = player.GetModPlayer<BlueflamePlayer>();
                currentTemp = blue.Temperature;
                maxTemp = blue.MaxTemperature;
            }
            else if (transPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                var hell = player.GetModPlayer<HellFlamesPlayer>();
                currentTemp = hell.Temperature;
                maxTemp = hell.MaxTemperature;
            }

            Texture2D barFrame = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/TemperatureBarFrame").Value;
            Texture2D barFill = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/TemperatureBarFill").Value;

            CalculatedStyle dimensions = GetDimensions();
            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

            spriteBatch.Draw(barFrame, drawPos, Color.White);

            bool hasPhosphor = player.HasBuff(ModContent.BuffType<PhosphorBuff>());
            Color textColor = Color.White;

            if (hasPhosphor)
            {
                float pulse = (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 5f) * 0.5f + 0.5f);
                Color phosphorColor = Color.Lerp(Color.Cyan, Color.OrangeRed, pulse);
                textColor = phosphorColor;

                spriteBatch.Draw(barFill, drawPos, phosphorColor);
            }
            else
            {
                float centerOffsetX = barFill.Width / 2f;
                Vector2 centerDrawPos = drawPos + new Vector2(centerOffsetX, 0);

                if (currentTemp > 0)
                {
                    float quotient = MathHelper.Clamp((float)currentTemp / maxTemp, 0f, 1f);
                    int fillWidth = (int)(centerOffsetX * quotient);

                    Rectangle sourceRect = new Rectangle((int)centerOffsetX, 0, fillWidth, barFill.Height);

                    textColor = Color.OrangeRed;
                    spriteBatch.Draw(barFill, centerDrawPos, sourceRect, textColor);
                }
                else if (currentTemp < 0) 
                {
                    float quotient = MathHelper.Clamp((float)Math.Abs(currentTemp) / minTemp, 0f, 1f);
                    int fillWidth = (int)(centerOffsetX * quotient);

                    Vector2 leftDrawPos = centerDrawPos - new Vector2(fillWidth, 0);
                    Rectangle sourceRect = new Rectangle((int)(centerOffsetX - fillWidth), 0, fillWidth, barFill.Height);

                    textColor = Color.Cyan;
                    spriteBatch.Draw(barFill, leftDrawPos, sourceRect, textColor);
                }
            }

            string text = hasPhosphor ? "PHOSPHOR" : $"{currentTemp}°C";
            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - (hasPhosphor ? 40f : 15f), barFrame.Height + 2f);
            Utils.DrawBorderString(spriteBatch, text, textPos, textColor, 0.8f);
        }
    }
}