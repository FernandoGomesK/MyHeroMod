using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using System;
using MyHeroMod.content.System.Interfaces;
using KhacesCore.Content.System.Interfaces;
using KhacesCore.Content.System;

namespace MyHeroMod.content.Quirks.SuperRegeneration
{
    public partial class RegenPlayer : ModPlayer, IStrainSource
    {
        public int StrainPenaltyPerSecond { get; set; }
        bool IStrainSource.IsLethalStrain => false;
        bool IStrainSource.CausesStrainDamage => false;
        bool IStrainSource.IsStrainActive => Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.SuperRegeneration);

        public void AddStrain(int amount)
        {
            Player.GetModPlayer<CorePlayer>().AddStrain(amount);
        }

        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var corePlayer = Player.GetModPlayer<CorePlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.SuperRegeneration))
            {
                StrainPenaltyPerSecond = 0;
                return;
            }

            bool isHealing = Player.statLife < Player.statLifeMax2;

            if (isHealing)
            {
                StrainPenaltyPerSecond = Math.Max(5, (int)(corePlayer.maxStrain * 0.05f));
            }
            else
            {
                StrainPenaltyPerSecond = corePlayer.currentStrain > 0 ? -5 : 0;
            }
        }

        public override void UpdateLifeRegen()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var corePlayer = Player.GetModPlayer<CorePlayer>();

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

                if (corePlayer.maxStrain > 0)
                {
                    float strainRatio = (float)corePlayer.currentStrain / corePlayer.maxStrain;

                    if (strainRatio >= 0.90f)
                    {
                        regenBonus = 0;
                        if (Player.lifeRegen > 0)
                        {
                            Player.lifeRegen = 0;
                        }
                    }
                    else
                    {
                        float regenMultiplier = 1f - (strainRatio / 0.90f);
                        regenBonus = (int)(regenBonus * Math.Max(0f, regenMultiplier));
                    }
                }

                Player.lifeRegen += regenBonus;
            }
        }
    }
}