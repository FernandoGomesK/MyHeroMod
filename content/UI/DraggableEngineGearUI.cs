using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.Engine; 

namespace MyHeroMod.content.UI
{
    public class DraggableEngineGear : UIElement
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
            var enginePlayer = player.GetModPlayer<EnginePlayer>(); 

            
            if (!transPlayer.HasActiveQuirk(QuirkType.Engine))
                return;

            
            Texture2D textureToDraw = enginePlayer.currentGear switch
            {
                1 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear1").Value,
                2 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear2").Value,
                3 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear3").Value,
                4 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear4").Value,
                5 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear5").Value,
                _ => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGears").Value 
            };

            CalculatedStyle dimensions = GetDimensions();
            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

           
            spriteBatch.Draw(textureToDraw, drawPos, Color.White);
        }
    }
}