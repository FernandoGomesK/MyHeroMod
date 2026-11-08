using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;


namespace MyHeroMod.content.Quirks.Gearshift
{

    public partial class GearshiftPlayer : ModPlayer
    {
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {

            if (isGearshiftBuffActive)
            {

                drawInfo.colorArmorBody = Color.RoyalBlue;
                drawInfo.colorArmorHead = Color.RoyalBlue;
                drawInfo.colorArmorLegs = Color.RoyalBlue;


                //Player.armorEffectDrawShadow = true; 
                Player.armorEffectDrawOutlines = true;

                Lighting.AddLight(Player.Center, Color.RoyalBlue.ToVector3());

            }
        }
    }
}
 