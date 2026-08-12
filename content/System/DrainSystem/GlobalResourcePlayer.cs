using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System.Interfaces;

namespace MyHeroMod.content.System
{
    public class GlobalResourcePlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            if (Main.GameUpdateCount % 60 == 0)
            {
            
                foreach (var modPlayer in Player.ModPlayers)
                {
            
                    if (modPlayer is IHeroTemperature tempUser)
                    {
                    
                        if (tempUser.HeatPerSecond != 0)
                        {
                            tempUser.AddHeat(tempUser.HeatPerSecond);
                        }


                        if (tempUser.StrainPenaltyPerSecond > 0)
                        {
                            tempUser.AddStrain(tempUser.StrainPenaltyPerSecond);
                        }
                    }
                }
            }
        }
    }
}