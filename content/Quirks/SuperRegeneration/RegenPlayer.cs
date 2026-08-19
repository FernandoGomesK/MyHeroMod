using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using System;

namespace MyHeroMod.content.Quirks.SuperRegeneration
{
    public partial class RegenPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.SuperRegeneration))
            {
                return;
            }

            if (Player.statLife < Player.statLifeMax2)
            {
                if (Main.GameUpdateCount % 12 == 0)
                {
                    int strainIncrease = (int)(transPlayer.maxStrain * 0.01f);
                    transPlayer.currentStrain += Math.Max(1, strainIncrease);
                }
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