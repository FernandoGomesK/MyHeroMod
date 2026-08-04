using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria; 

namespace MyHeroMod.content.UI
{
    public class SweatUIState : UIState
    {
        public DraggableSweatBar sweatBar;

        public override void OnInitialize()
        {
            sweatBar = new DraggableSweatBar();
            
            sweatBar.Width.Set(72f, 0f);  
            sweatBar.Height.Set(74f, 0f); 

            
            sweatBar.Left.Set(Main.screenWidth - 380f, 0f);
            sweatBar.Top.Set(10f, 0f);

            Append(sweatBar);
        }
    }
}