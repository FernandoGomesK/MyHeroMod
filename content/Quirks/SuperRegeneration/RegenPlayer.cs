using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using System;
using MyHeroMod.content.System.Interfaces;


namespace MyHeroMod.content.Quirks.SuperRegeneration
{
    public partial class RegenPlayer : ModPlayer,  IStrainSource
    {
        public int StrainPenaltyPerSecond { get; set; }

        public void AddStrain(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            transPlayer.currentStrain += amount;

            if (transPlayer.currentStrain <= 0)
            {
                transPlayer.currentStrain = 0;
            }
            else if (transPlayer.currentStrain >= transPlayer.maxStrain)
            {
                transPlayer.currentStrain = transPlayer.maxStrain;
            }
        }


        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.SuperRegeneration))
            {
                StrainPenaltyPerSecond = 0; 
                return;
            }

            bool isHealing = Player.statLife < Player.statLifeMax2;

            if (isHealing)
            {
                
                StrainPenaltyPerSecond = Math.Max(5, (int)(transPlayer.maxStrain * 0.05f));
            }
            else
            {
                StrainPenaltyPerSecond = transPlayer.currentStrain > 0 ? -5 : 0;
            }
        }

        public override void UpdateLifeRegen()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            bool hasRegenQuirk = transPlayer.HasActiveQuirk(QuirkType.SuperRegeneration);
            bool isNotErased = !Player.HasBuff(ModContent.BuffType<QuirkErased>());

            if (hasRegenQuirk && isNotErased)
            {
                int regenBonus = 10;

                switch (transPlayer.CurrentStage)
                {
                    case QuirkStage.Initial: regenBonus = 10; break;
                    case QuirkStage.Adequation: regenBonus = 20; break;
                    case QuirkStage.Intermediate: regenBonus = 40; break;
                    case QuirkStage.Advanced: regenBonus = 100; break;
                    case QuirkStage.Final: regenBonus = 200; break;
                }

              
                if (transPlayer.maxStrain > 0)
                {
                    float strainRatio = (float)transPlayer.currentStrain / transPlayer.maxStrain;
                    float regenMultiplier = Math.Max(0.1f, 1f - strainRatio);
                    regenBonus = (int)(regenBonus * regenMultiplier);
                }

                Player.lifeRegen += regenBonus;
            }
        }
    }
}