using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.System.BasePlayer;


namespace MyHeroMod.content.Quirks.FaJin
{
    public partial class FajinPlayer : BasePlayer
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