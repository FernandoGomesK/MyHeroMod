using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria; 

namespace MyHeroMod.content.UI
{
    public class OpticChargeUIState : UIState
    {
        public DraggableOpticChargeBar OpticBar;

        public override void OnInitialize()
        {
            OpticBar = new DraggableOpticChargeBar();
            
            OpticBar.Width.Set(68f, 0f);  
            OpticBar.Height.Set(28f, 0f); 

            
            OpticBar.Left.Set(Main.screenWidth - 355f, 0f);
            OpticBar.Top.Set(10f, 0f);

            Append(OpticBar);
        }
    }
}