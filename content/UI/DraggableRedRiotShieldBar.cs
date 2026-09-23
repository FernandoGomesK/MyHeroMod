using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyHeroMod.content.Quirks.Flight;
using MyHeroMod.content.Quirks.Hardening;
using MyHeroMod.content.System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace MyHeroMod.content.UI
{
    
    public class DraggableRedRiotShieldBar : UIElement
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
            var hardenPlayer = player.GetModPlayer<HardeningPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.Hardening))
                return;

            Texture2D barFrame = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/RedRiotFrame").Value;
            Texture2D barFill = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/RedRiotFill").Value;
            Texture2D unbreakableTex = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/RedRiotUnbreakable").Value;

            CalculatedStyle dimensions = GetDimensions();
            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

            float quotient = 1f;
            if (hardenPlayer.hardeningMaxHealth > 0)
            {
                quotient = (float)hardenPlayer.hardeningHealth / hardenPlayer.hardeningMaxHealth;
            }
            quotient = MathHelper.Clamp(quotient, 0f, 1f);

            
            spriteBatch.Draw(barFrame, drawPos, Color.White);

           
            int fillHeight = (int)(barFill.Height * quotient);
            int emptySpace = barFill.Height - fillHeight;

            Rectangle fillRect = new Rectangle(0, emptySpace, barFill.Width, fillHeight);
            Vector2 fillDrawPos = drawPos + new Vector2(0, emptySpace);

            spriteBatch.Draw(barFill, fillDrawPos, fillRect, Color.White);

            
            if (hardenPlayer.isUnbreakableOn)
            {
                spriteBatch.Draw(unbreakableTex, drawPos, Color.White);
            }

         
            string text = $"{(int)hardenPlayer.hardeningHealth} / {hardenPlayer.hardeningMaxHealth}";
            float scale = 0.8f;
            Vector2 textSize = Terraria.GameContent.FontAssets.MouseText.Value.MeasureString(text) * scale;
            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - textSize.X / 2f, barFrame.Height + 5f);

            Utils.DrawBorderString(spriteBatch, text, textPos, Color.Cyan, scale);
        }
    }
}