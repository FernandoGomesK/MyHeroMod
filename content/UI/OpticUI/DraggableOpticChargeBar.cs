//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria;
//using Terraria.UI;
//using Terraria.ModLoader;
//using MyHeroMod.content.System;
//using MyHeroMod.content.Quirks.OpticBlast;

//namespace MyHeroMod.content.UI.OpticUI
//{
//    // 1. THE STATE
//    public class OpticChargeUIState : UIState
//    {
//        public OpticChargeBar OpticBar;

//        public override void OnInitialize()
//        {
//            OpticBar = new OpticChargeBar();
//            Append(OpticBar);
//        }
//    }

//    // 2. THE ELEMENT
//    public class OpticChargeBar : BaseDraggableBar
//    {
//        protected override void AutoPosition()
//        {
//            Player player = Main.LocalPlayer;
//            var transPlayer = player.GetModPlayer<TransformationPlayer>();

//            int uiSlot = 0;

//            // Push the UI down if other Quirks are active
//            if (transPlayer.HasActiveQuirk(QuirkType.Engine)) uiSlot++;
//            if (transPlayer.HasActiveQuirk(QuirkType.Flight)) uiSlot++;
//            if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th) || transPlayer.HasActiveQuirk(QuirkType.OneForAll8th)) uiSlot++;

//            Left.Set(Main.screenWidth - 355f, 0f);

//            Top.Set(10f + (uiSlot * 76f), 0f);
//        }

//        public override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);

//            Player player = Main.LocalPlayer;
//            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();

//            if (opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.Full)
//            {
//                Width.Set(110f, 0f);
//                Height.Set(28f, 0f);
//            }
//            else
//            {
//                Width.Set(100f, 0f);
//                Height.Set(28f, 0f);
//            }
//        }

//        protected override void DrawSelf(SpriteBatch spriteBatch)
//        {
//            Player player = Main.LocalPlayer;
//            var transPlayer = player.GetModPlayer<TransformationPlayer>();
//            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();

//            if (!transPlayer.HasActiveQuirk(QuirkType.OpticBlast))
//                return;

//            Texture2D barFrame = opticPlayer.CurrentPercentage switch
//            {
//                OpticBlastPlayer.Percentage.Zero => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/00OpticBlastBarFrame").Value,
//                OpticBlastPlayer.Percentage.TwentyFive => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/25OpticBlastBarFrame").Value,
//                OpticBlastPlayer.Percentage.Fifty => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/50OpticBlastBarFrame").Value,
//                OpticBlastPlayer.Percentage.SeventyFive => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/75OpticBlastBarFrame").Value,
//                OpticBlastPlayer.Percentage.Full => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/100OpticBlastBarFrame").Value,
//                _ => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/00OpticBlastBarFrame").Value
//            };

//            Texture2D barFill = opticPlayer.CurrentPercentage switch
//            {
//                OpticBlastPlayer.Percentage.Zero => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/00OpticBlastBarFill").Value,
//                OpticBlastPlayer.Percentage.TwentyFive => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/25OpticBlastBarFill").Value,
//                OpticBlastPlayer.Percentage.Fifty => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/50OpticBlastBarFill").Value,
//                OpticBlastPlayer.Percentage.SeventyFive => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/75OpticBlastBarFill").Value,
//                OpticBlastPlayer.Percentage.Full => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/100OpticBlastBarFill").Value,
//                _ => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/OpticBlastUI/00OpticBlastBarFill").Value
//            };

//            CalculatedStyle dimensions = GetDimensions();
//            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

//            float quotient = 1f;
//            if (opticPlayer.MaxOpticBlast > 0)
//            {
//                quotient = (float)opticPlayer.CurrentOpticBlast / opticPlayer.MaxOpticBlast;
//            }
//            quotient = MathHelper.Clamp(quotient, 0f, 1f);

//            spriteBatch.Draw(barFrame, drawPos, Color.White);
//            Rectangle fillRect = new Rectangle(0, 0, (int)(barFill.Width * quotient), barFill.Height);
//            spriteBatch.Draw(barFill, drawPos, fillRect, Color.White);

//            string text = $"{opticPlayer.CurrentOpticBlast} / {opticPlayer.MaxOpticBlast}";
//            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - 20f, barFrame.Height + 5f);
//            Utils.DrawBorderString(spriteBatch, text, textPos, Color.Cyan, 0.8f);
//        }
//    }
//}