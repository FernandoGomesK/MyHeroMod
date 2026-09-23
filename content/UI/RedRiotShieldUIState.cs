using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria; 

namespace MyHeroMod.content.UI
{
    public class RedRiotShieldUIState : UIState
    {
        public DraggableRedRiotShieldBar redRiotBar;

        public override void OnInitialize()
        {
            redRiotBar = new DraggableRedRiotShieldBar();
            
            redRiotBar.Width.Set(56f, 0f);  
            redRiotBar.Height.Set(62f, 0f); 

            
            redRiotBar.Left.Set(Main.screenWidth - 355f, 0f);
            redRiotBar.Top.Set(10f, 0f);

            Append(redRiotBar);
        }
    }
}