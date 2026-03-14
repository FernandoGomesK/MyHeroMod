using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;

namespace MyHeroMod.content.Quirks.SuperRegeneration
{
    public partial class RegenPlayer : ModPlayer
    {
        
        public override void UpdateLifeRegen()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            if (transPlayer.HasActiveQuirk(QuirkType.SuperRegeneration) || !Player.HasBuff(ModContent.BuffType<QuirkErased>()))
            {
                int regenBonus = 10;

                
                switch (transPlayer.CurrentStage)
                {
                    case QuirkStage.Initial: 
                        regenBonus = 10; 
                        break;      
                    case QuirkStage.Adequation: 
                        regenBonus = 20; 
                        break;   
                    case QuirkStage.Intermediate: 
                        regenBonus = 40; 
                        break; 
                    case QuirkStage.Advanced: 
                        regenBonus = 100;
                        break;    
                    case QuirkStage.Final: 
                        regenBonus = 200; 
                        break;       
                }

                Player.lifeRegen += regenBonus;
            }
        }
    }
}