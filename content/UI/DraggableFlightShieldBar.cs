using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.Flight;

namespace MyHeroMod.content.UI
{
    // Criamos um elemento de UI customizado
    public class DraggableFlightShieldBar : UIElement
    {
        private Vector2 offset;
        public bool dragging;

        // Quando o jogador clica com o botão esquerdo
        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            
            if (Main.playerInventory)
            {
                offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
                dragging = true;
            }
        }

        // Quando o jogador solta o botão esquerdo
        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);
            dragging = false;
        }

        // Atualiza a posição a cada frame se estiver arrastando
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (dragging)
            {
                // Move o elemento para onde o mouse está, mantendo o offset de onde clicou
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();
            }

            // Opcional: Travar a barra para não sair da tela
            CalculatedStyle dimensions = GetDimensions();
            if (dimensions.X < 0) Left.Set(0, 0f);
            if (dimensions.X > Main.screenWidth - dimensions.Width) Left.Set(Main.screenWidth - dimensions.Width, 0f);
            if (dimensions.Y < 0) Top.Set(0, 0f);
            if (dimensions.Y > Main.screenHeight - dimensions.Height) Top.Set(Main.screenHeight - dimensions.Height, 0f);
            
            Recalculate();
        }

        // A lógica de desenho vem pra cá
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var flightPlayer = player.GetModPlayer<FlightPlayer>();

            // Se não tiver a Quirk, apenas não desenha nada
            if (transPlayer.SelectedQuirk != QuirkType.Flight)
                return;

            Texture2D barFrame = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FlightShieldFrame").Value;
            Texture2D barFill = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FlightShieldFill").Value;

            
            CalculatedStyle dimensions = GetDimensions();
            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

            float quotient = 1f;
            if (flightPlayer.flightShieldMaxHealth > 0)
            {
                quotient = (float)flightPlayer.flightShieldHealth / flightPlayer.flightShieldMaxHealth;
            }
            quotient = MathHelper.Clamp(quotient, 0f, 1f);

            spriteBatch.Draw(barFrame, drawPos, Color.White);

            int fillHeight = (int)(barFill.Height * quotient);
            int emptySpace = barFill.Height - fillHeight;

            Rectangle fillRect = new Rectangle(0, emptySpace, barFill.Width, fillHeight);
            Vector2 fillDrawPos = drawPos + new Vector2(0, emptySpace);

            spriteBatch.Draw(barFill, fillDrawPos, fillRect, Color.White);

            string text = $"{(int)flightPlayer.flightShieldHealth} / {flightPlayer.flightShieldMaxHealth}";
            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - 20f, barFrame.Height + 5f);
            Utils.DrawBorderString(spriteBatch, text, textPos, Color.Cyan, 0.8f);
        }
    }
}