using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System.Interfaces;
using Terraria.DataStructures;

namespace MyHeroMod.content.System
{
    public class GlobalResourcePlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            if (Main.GameUpdateCount % 60 == 0)
            {   
                var transPlayer = Player.GetModPlayer<TransformationPlayer>();
                int strainDamage = 0;

                
                if (transPlayer.currentStrain >= transPlayer.maxStrain) 
                {
                    strainDamage = 20;  
                }
                else if (transPlayer.currentStrain >= (transPlayer.maxStrain * 0.75f)) 
                {
                    strainDamage = 5;
                }
                else if (transPlayer.currentStrain >= (transPlayer.maxStrain * 0.50f)) 
                {
                    strainDamage = 2; 
                }

                
                if (strainDamage > 0)
                {
                    Player.statLife -= strainDamage;
                    Player.lifeRegenTime = 0; 

                    
                    CombatText.NewText(Player.getRect(), CombatText.LifeRegenNegative, strainDamage.ToString(), false, true);

                    
                    
                    if (Player.statLife <= 0)
                    {
                        var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name)
                    );
                    }
                }
            
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