using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria; 

namespace MyHeroMod.content.UI
{
    public class NauseaUIState : UIState
    {
        public DraggableNauseaBar nauseaBar;

        public override void OnInitialize()
        {
            nauseaBar = new DraggableNauseaBar();
            
            nauseaBar.Width.Set(64f, 0f);  
            nauseaBar.Height.Set(64f, 0f); 

            
            nauseaBar.Left.Set(Main.screenWidth - 355f, 0f);
            nauseaBar.Top.Set(10f, 0f);

            Append(nauseaBar);
        }
    }
}