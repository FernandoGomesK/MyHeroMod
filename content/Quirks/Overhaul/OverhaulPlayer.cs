using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Overhaul
{
    public partial class OverhaulPlayer : ModPlayer
    {

        public bool isChimeraActive = false;
        
        public override void ResetEffects()
        {
            isChimeraActive = false;
        }
    }
}