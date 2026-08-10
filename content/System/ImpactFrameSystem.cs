using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace MyHeroMod.content.System
{
    public class ImpactFrameSystem : ModSystem
    {
        public static int ImpactTimer = 0; 

        public override void PostUpdateEverything()
        {
            if (ImpactTimer > 0)
            {
                ImpactTimer--;
            }
        }

        public override void PostDrawTiles()
        {
            if (ImpactTimer > 0)
            {
               
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

                Main.spriteBatch.Draw(
                    TextureAssets.MagicPixel.Value, 
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), 
                    Color.White
                );

                Main.spriteBatch.End();
            }
        }
    }
}