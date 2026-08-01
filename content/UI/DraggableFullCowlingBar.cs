using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.ZeroGravity;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.UI
{
    public class DraggableFullCowlingBar : UIElement
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

            Player player = Main.LocalPlayer;
            var ofa9Player = player.GetModPlayer<OneForAll9thPlayer>();

            
            if (ofa9Player.percentage == 5)
            {
                Width.Set(18f, 0f);  
                Height.Set(24f, 0f);    
            }
            else if (ofa9Player.percentage == 10)
            {
                Width.Set(32f, 0f);  
                Height.Set(24f, 0f); 
            }
            else if (ofa9Player.percentage == 45)
            {
                Width.Set(34f, 0f);  
                Height.Set(24f, 0f); 
            }
            else
            {
                Width.Set(32f, 0f);  
                Height.Set(24f, 0f); 
            }

            
            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
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
            var ofa9Player = player.GetModPlayer<OneForAll9thPlayer>();
            var hasGear = player.HasBuff(ModContent.BuffType<GearshiftBuff>());

            if (!transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
                return;

            Texture2D barFill = ofa9Player.percentage switch
            {
                5 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill5").Value,
                10 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill10").Value,
                45 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill45").Value,
                _ => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill").Value,
            };

            Texture2D barFrame = ofa9Player.percentage switch
            {
                5 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame5").Value,
                10 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame10").Value,
                45 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame45").Value,
                _ => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame").Value,
            };

            CalculatedStyle dimensions = GetDimensions();
            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

            // --- LÓGICA DE PREENCHIMENTO INVERTIDA ---
            float quotient = 1f;
            if (transPlayer.maxStrain > 0)
            {
                // Fazemos 1f (100%) menos a porcentagem atual de strain.
                // Se o strain for 0, o quotient será 1f (Barra Cheia).
                // Se o strain for máximo, o quotient será 0f (Barra Vazia).
                quotient = 1f - ((float)transPlayer.currentStrain / transPlayer.maxStrain);
            }
            quotient = MathHelper.Clamp(quotient, 0f, 1f);

            spriteBatch.Draw(barFrame, drawPos, Color.White);

            int fillHeight = (int)(barFill.Height * quotient);
            int emptySpace = barFill.Height - fillHeight;

            Rectangle fillRect = new Rectangle(0, emptySpace, barFill.Width, fillHeight);
            Vector2 fillDrawPos = drawPos + new Vector2(0, emptySpace);

            Color fillColor = hasGear ? Color.Blue : Color.LimeGreen;

            spriteBatch.Draw(barFill, fillDrawPos, fillRect, fillColor);
            
            // Texto da interface
            string text = $"{(int)transPlayer.currentStrain} / {transPlayer.maxStrain}";
            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - 20f, barFrame.Height + 5f);
            Utils.DrawBorderString(spriteBatch, text, textPos, Color.Cyan, 0.8f);
        }
    }
}