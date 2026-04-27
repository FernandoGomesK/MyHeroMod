using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.Erasure;
using System;

namespace MyHeroMod.content.UI
{
    public class BlinkUIState : UIState
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var erasurePlayer = player.GetModPlayer<ErasurePlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.Erasure))
                return;

            Texture2D barFrame = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EyeFrame").Value;
            Texture2D barFill = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EyeFullFill").Value;

            Vector2 screenCenter = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 2f);
            Vector2 drawPos = screenCenter + new Vector2(-barFrame.Width / 2f, 60f);

        
            float t = erasurePlayer.blinkAnimTimer;
            float quotient = 1f - MathF.Pow(1f - t, 4f);
            quotient = MathHelper.Clamp(quotient, 0f, 1f);

            spriteBatch.Draw(barFrame, drawPos, Color.White);

            int halfHeight = barFill.Height / 2;
            int eyelidHeight = (int)(halfHeight * quotient);

            if (eyelidHeight > 0)
            {
                
                Rectangle topRect = new Rectangle(0, 0, barFill.Width, eyelidHeight);
                spriteBatch.Draw(barFill, drawPos, topRect, Color.White);

                // Pálpebra de baixo
                Rectangle bottomRect = new Rectangle(0, barFill.Height - eyelidHeight, barFill.Width, eyelidHeight);
                Vector2 bottomPos = drawPos + new Vector2(0, barFill.Height - eyelidHeight);
                spriteBatch.Draw(barFill, bottomPos, bottomRect, Color.White);
            }

            base.Draw(spriteBatch);
        }
    }
}