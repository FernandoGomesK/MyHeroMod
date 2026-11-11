using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.ZeroGravity;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.OFA8th;
using KhacesCore.Content.System;
using MyHeroMod.content.Buffs.Gearshift;

namespace MyHeroMod.content.UI.FullCowlUI
{
 
    public class FullCowlingUIState : UIState
    {
        public FullCowlingBar fullCowlingBar;

        public override void OnInitialize()
        {
            fullCowlingBar = new FullCowlingBar();
            
            Append(fullCowlingBar);
        }
    }

    
    public class FullCowlingBar : BaseDraggableBar
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

            if (transPlayer.HasActiveQuirk(QuirkType.Flight))
            {
                uiSlot++;
            }



            Left.Set(Main.screenWidth - 380f, 0f);

            Top.Set(10f + (uiSlot * 76f), 0f);
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);

            Player player = Main.LocalPlayer;
            var ofa9Player = player.GetModPlayer<OneForAll9thPlayer>();

          
            if (ofa9Player.isQuirkless || ofa9Player.percentage == 5)
            {
                Width.Set(24f, 0f);
                Height.Set(32f, 0f);
            }
            else if (ofa9Player.percentage == 10)
            {
                Width.Set(44f, 0f);
                Height.Set(32f, 0f);
            }
            else if (ofa9Player.percentage == 20)
            {
                Width.Set(52f, 0f);
                Height.Set(32f, 0f);
            }
            else if (ofa9Player.percentage == 45)
            {
                Width.Set(48f, 0f);
                Height.Set(32f, 0f);
            }
            else if (ofa9Player.percentage == 100)
            {
                Width.Set(72f, 0f);
                Height.Set(32f, 0f);
            }
            else
            {
                Width.Set(24f, 0f);
                Height.Set(32f, 0f);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var ofa9Player = player.GetModPlayer<OneForAll9thPlayer>();
            var ofa8Player = player.GetModPlayer<OneForAll8thPlayer>();
            var corePlayer = player.GetModPlayer<CorePlayer>();
            var hasGear = player.HasBuff(ModContent.BuffType<GearshiftBuff>());

            int emberBar = ofa9Player.isQuirkless ? ofa9Player.EmberBar : (ofa8Player.isQuirkless ? ofa8Player.EmberBar : 0);

            bool hasOFA9 = transPlayer.HasActiveQuirk(QuirkType.OneForAll9th);
            bool hasOFA8Quirkless = transPlayer.HasActiveQuirk(QuirkType.OneForAll8th) && ofa8Player.isQuirkless;

            if (!hasOFA9 && !hasOFA8Quirkless)
                return;

            Texture2D barFill;
            Texture2D barFrame;
            float quotient = 1f;

           
            if (ofa9Player.isQuirkless || ofa8Player.isQuirkless)
            {
                barFill = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EmbersBarFill").Value;
                barFrame = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EmbersBarFrame").Value;

                float maxTimer = 1200f;
                if (maxTimer <= 0) maxTimer = 1f;

                quotient = MathHelper.Clamp((float)emberBar / maxTimer, 0f, 1f);
            }
            else
            {
                barFill = ofa9Player.percentage switch
                {
                    5 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill5").Value,
                    10 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill10").Value,
                    20 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill20").Value,
                    45 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill45").Value,
                    100 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill100").Value,
                    _ => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFill").Value,
                };

                barFrame = ofa9Player.percentage switch
                {
                    5 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame5").Value,
                    10 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame10").Value,
                    20 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame20").Value,
                    45 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame45").Value,
                    100 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame100").Value,
                    _ => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/FullCowlingBarFrame").Value,
                };

                if (corePlayer.maxStrain >= 0)
                {
                    quotient = 1f - ((float)corePlayer.currentStrain / corePlayer.maxStrain);
                }
                quotient = MathHelper.Clamp(quotient, 0f, 1f);
            }

            CalculatedStyle dimensions = GetDimensions();
            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

            spriteBatch.Draw(barFrame, drawPos, Color.White);

            int fillHeight = (int)(barFill.Height * quotient);
            int emptySpace = barFill.Height - fillHeight;

            Rectangle fillRect = new Rectangle(0, emptySpace, barFill.Width, fillHeight);
            Vector2 fillDrawPos = drawPos + new Vector2(0, emptySpace);

            Color fillColor = (ofa9Player.isQuirkless || ofa8Player.isQuirkless) ? Color.White : (hasGear ? Color.Blue : Color.LimeGreen);

            spriteBatch.Draw(barFill, fillDrawPos, fillRect, fillColor);

            string text;
            if (ofa9Player.isQuirkless)
            {
                float maxTimer = ofa9Player.initialEmbers;
                if (maxTimer <= 0) maxTimer = 1f;

               
                float percentual = MathHelper.Clamp(((float)emberBar / maxTimer) * 100f, 0f, 100f);
                text = $"{percentual:F1}%";
            }
            else if (ofa8Player.isQuirkless)
            {
                float maxTimer = ofa8Player.initialEmbers;
                if (maxTimer <= 0) maxTimer = 1f;

             
                float percentual = MathHelper.Clamp(((float)emberBar / maxTimer) * 100f, 0f, 100f);
                text = $"{percentual:F1}%";
            }
            else
            {
                int remainingStrain = corePlayer.maxStrain - (int)corePlayer.currentStrain;
                text = $"{remainingStrain} / {corePlayer.maxStrain}";
            }

            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - 20f, barFrame.Height + 5f);
            Utils.DrawBorderString(spriteBatch, text, textPos, Color.Cyan, 0.8f);
        }
    }
}