using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;


namespace MyHeroMod.content.Quirks.FaJin
{
    public partial class FaJinPlayer : ModPlayer
    {
       
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
        if (FaJinStored)
        {
            drawInfo.colorArmorLegs = Color.Red;
            drawInfo.colorArmorBody = Color.Red;
            
        }
        
        if (FaJinStored)
            {
                
            }
        }
    }
}