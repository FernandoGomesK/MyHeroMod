using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria; 

namespace MyHeroMod.content.UI
{
    public class FlightShieldUIState : UIState
    {
        public DraggableFlightShieldBar shieldBar;

        public override void OnInitialize()
        {
            shieldBar = new DraggableFlightShieldBar();
            
            shieldBar.Width.Set(56f, 0f);  
            shieldBar.Height.Set(62f, 0f); 

            
            shieldBar.Left.Set(Main.screenWidth - 355f, 0f);
            shieldBar.Top.Set(10f, 0f);

            Append(shieldBar);
        }
    }
}