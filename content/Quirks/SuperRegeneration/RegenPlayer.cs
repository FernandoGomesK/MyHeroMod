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

            
            bool hasRegenQuirk = transPlayer.HasActiveQuirk(QuirkType.SuperRegeneration);

            
            bool isNotErased = !Player.HasBuff(ModContent.BuffType<QuirkErased>());

            
            if (hasRegenQuirk && isNotErased)
            {
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
}}