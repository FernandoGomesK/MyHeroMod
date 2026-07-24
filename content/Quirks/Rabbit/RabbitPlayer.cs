using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Rabbit
{
    public class RabbitPlayer : ModPlayer
    {
        public bool isIronSolesOn = false;

        public override void ResetEffects()
        {
            isIronSolesOn = false;
        }


        
    }
}