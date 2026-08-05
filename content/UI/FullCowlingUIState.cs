using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria;
using MyHeroMod.content.Quirks.OFA9th;

namespace MyHeroMod.content.UI
{
    public class FullCowlingUIState : UIState
    {
        public DraggableFullCowlingBar fullCowlingBar;

        public override void OnInitialize()
        {
            fullCowlingBar = new DraggableFullCowlingBar();
            
            
            fullCowlingBar.Left.Set(Main.screenWidth - 380f, 0f);
            fullCowlingBar.Top.Set(10f, 0f);

            Append(fullCowlingBar);
        }
    }
}