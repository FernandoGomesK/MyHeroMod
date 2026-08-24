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
                    if (modPlayer is IHeroTemperature tempUser && tempUser.HeatPerSecond != 0)
                    {
                        tempUser.AddHeat(tempUser.HeatPerSecond);
                    }

                    if (modPlayer is IStrainSource strainUser && strainUser.StrainPenaltyPerSecond != 0)
                    {
                        strainUser.AddStrain(strainUser.StrainPenaltyPerSecond);
                    }

                    if (modPlayer is IHeroBreath breathUser && breathUser.BreathChangePerSecond != 0)
                        breathUser.AddBreath(breathUser.BreathChangePerSecond);
                }
            }
        }
    }
}