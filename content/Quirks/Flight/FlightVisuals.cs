using Microsoft.Xna.Framework;
using Terraria.DataStructures;

using Terraria.ModLoader;
using Terraria;


namespace MyHeroMod.content.Quirks.Flight
{
    public partial class FlightPlayer : ModPlayer
    {

        
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            
       if (isFlightShieldOn)
       {
            if (flightShieldHealth > 0)
                {
                    drawInfo.colorArmorLegs = Color.Yellow * 1.2f;
                    drawInfo.colorArmorBody = Color.Yellow * 1.2f;
                    drawInfo.colorBodySkin = Color.Yellow * 1.2f;
                    drawInfo.colorArmorBody = Color.Yellow * 1.2f;
                    Player.armorEffectDrawOutlines = true;
                    Lighting.AddLight(Player.Center, Color.Yellow.ToVector3() );   
                }
            
            
        
            
        }
    }
    }
    }