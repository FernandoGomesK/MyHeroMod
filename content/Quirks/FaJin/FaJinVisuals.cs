using Microsoft.Xna.Framework;
using Terraria.DataStructures;

using Terraria.ModLoader;


namespace MyHeroMod.content.Quirks.FaJin
{
    public partial class FajinPlayer : ModPlayer
    {

        
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            
       if (FaJinStored)
       {
            drawInfo.colorArmorLegs = Color.Red * 1.2f;
            drawInfo.colorArmorBody = Color.Red * 1.2f;
            
        
            
        }
    }
    }
    }