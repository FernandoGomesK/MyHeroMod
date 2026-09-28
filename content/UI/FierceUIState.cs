using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria; 

namespace MyHeroMod.content.UI
{
    public class FierceUIState : UIState
    {
        public DraggableFierceBar fierceBar;

        public override void OnInitialize()
        {
            fierceBar = new DraggableFierceBar();

            fierceBar.Width.Set(68f, 0f);
            fierceBar.Height.Set(28f, 0f);


            fierceBar.Left.Set(Main.screenWidth - 355f, 0f);
            fierceBar.Top.Set(10f, 0f);

            Append(fierceBar);
        }
    }
}