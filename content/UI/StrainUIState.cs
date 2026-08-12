using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria; 

namespace MyHeroMod.content.UI
{
    public class strainUIState : UIState
    {
        public DraggableNauseaBar strainBar;

        public override void OnInitialize()
        {
            strainBar = new DraggableNauseaBar();
            
            strainBar.Width.Set(64f, 0f);  
            strainBar.Height.Set(64f, 0f); 

            
            strainBar.Left.Set(Main.screenWidth - 500f, 0f);
            strainBar.Top.Set(10f, 0f);

            Append(strainBar);
        }
    }
}