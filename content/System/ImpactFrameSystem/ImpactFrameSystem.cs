using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace MyHeroMod.content.System
{
    public class ImpactFrameSystem : ModSystem
    {
        private const int WhiteFrames = 5;
        private const int CustomFrames = 1;
        private const int TotalFrames = WhiteFrames + CustomFrames;

        public static int ImpactTimer = 0;
        public static string CurrentImpactTexture = "";

        public static void Trigger(string texture)
        {
            CurrentImpactTexture = texture;
            ImpactTimer = TotalFrames;
        }

        public override void PostUpdateEverything()
        {
            if (ImpactTimer > 0)
                ImpactTimer--;
            else
                CurrentImpactTexture = "";
        }

        public override void PostDrawTiles()
        {
            if (ImpactTimer <= 0 || !ModContent.GetInstance<MyHeroConfig>().EnableImpactFrames)
                return;

            bool isCustomFrame = ImpactTimer <= CustomFrames && !string.IsNullOrEmpty(CurrentImpactTexture);

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            if (isCustomFrame)
            {
                Texture2D customImpact = ModContent.Request<Texture2D>(CurrentImpactTexture).Value;
                Main.spriteBatch.Draw(
                    customImpact,
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),
                    Color.DarkBlue
                );
            }
            else
            {
                Main.spriteBatch.Draw(
                    TextureAssets.MagicPixel.Value,
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),
                    Color.White
                );
            }

            Main.spriteBatch.End();
        }
    }
}