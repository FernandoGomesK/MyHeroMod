using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.DarkShadow
{
    public partial class DarkShadowPlayer : ModPlayer
    {

        public bool isDarkShadowOn = false;
        
        public override void ResetEffects()
        {
            isDarkShadowOn =  false;
            
        }
    }
}