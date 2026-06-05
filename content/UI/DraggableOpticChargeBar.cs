using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OpticBlast;

namespace MyHeroMod.content.UI
{
    public class DraggableOpticChargeBar : UIElement
    {
        private Vector2 offset;
        public bool dragging;

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            if (Main.playerInventory)
            {
                offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
                dragging = true;
            }
        }

        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);
            dragging = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();
            }

            CalculatedStyle dimensions = GetDimensions();
            if (dimensions.X < 0) Left.Set(0, 0f);
            if (dimensions.X > Main.screenWidth - dimensions.Width) Left.Set(Main.screenWidth - dimensions.Width, 0f);
            if (dimensions.Y < 0) Top.Set(0, 0f);
            if (dimensions.Y > Main.screenHeight - dimensions.Height) Top.Set(Main.screenHeight - dimensions.Height, 0f);
            
            Recalculate();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.OpticBlast))
                return;

            Texture2D barFrame = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastBarFrame").Value;
            Texture2D barFill = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastBarFill").Value;

            CalculatedStyle dimensions = GetDimensions();
            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

            float quotient = 1f;
            if (opticPlayer.MaxOpticBlast > 0)
            {
                quotient = (float)opticPlayer.CurrentOpticBlast / opticPlayer.MaxOpticBlast;
            }
            quotient = MathHelper.Clamp(quotient, 0f, 1f);

            
            spriteBatch.Draw(barFrame, drawPos, Color.White);

            
            Rectangle fillRect = new Rectangle(0, 0, (int)(barFill.Width * quotient), barFill.Height);
            spriteBatch.Draw(barFill, drawPos, fillRect, Color.White);

            
            string text = $"{opticPlayer.CurrentOpticBlast} / {opticPlayer.MaxOpticBlast}";
            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - 20f, barFrame.Height + 5f);
            Utils.DrawBorderString(spriteBatch, text, textPos, Color.Cyan, 0.8f);
        }
    }
}