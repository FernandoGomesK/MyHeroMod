using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria; 

namespace MyHeroMod.content.UI
{
    public class EngineGearUIState : UIState
    {
        public DraggableEngineGear EngineGear;

        public override void OnInitialize()
        {
            EngineGear = new DraggableEngineGear();
            
            EngineGear.Width.Set(46f, 0f);  
            EngineGear.Height.Set(66f, 0f); 

            
            EngineGear.Left.Set(Main.screenWidth - 355f, 0f);
            EngineGear.Top.Set(10f, 0f);

            Append(EngineGear);
        }
    }
}