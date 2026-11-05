using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Armor.Deku.BetaArmor
{
    public class BetaDekuManager : ModPlayer
    {
        public bool isCowlUp = false;

        public override void Kill(double damage, int hitDirection, bool pvp, Terraria.DataStructures.PlayerDeathReason damageSource)
        {
            isCowlUp = false;
        }
    }
}