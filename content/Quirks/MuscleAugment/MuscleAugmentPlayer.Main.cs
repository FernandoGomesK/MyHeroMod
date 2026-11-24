using KhacesCore.Content.System;
using KhacesCore.Content.System.BaseProjectiles;
using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs.MuscleAugment;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.MuscleAugment
{
    public class MuscleAugmentPlayer : ModPlayer, IDashModifier, IPunchModifier, IStrainSource, IQuirkResetter
    {
        public CorePlayer Core => Player.GetModPlayer<CorePlayer>();
        public bool isMuscleAugmentActive = false;
        public bool isPartialMuscleAugmentActive = false;

        public int StrainPenaltyPerSecond { get; set; }

        public void AddStrain(int amount)
        {
            Core.currentStrain += amount;

            if (Core.currentStrain <= 0)
            {
                Core.currentStrain = 0;
            }
            else if (Core.currentStrain >= Core.maxStrain)
            {
                Core.currentStrain = Core.maxStrain;
                Player.ClearBuff(ModContent.BuffType<MuscleAugmentBuff>());
                Player.ClearBuff(ModContent.BuffType<PartialAugmentBuff>());
                isMuscleAugmentActive = false;
                isPartialMuscleAugmentActive = false;
            }
        }

        public override void ResetEffects()
        {
            isMuscleAugmentActive = false;
            isPartialMuscleAugmentActive = false;
        }

        public void FullReset()
        {
            isMuscleAugmentActive = false;
            isPartialMuscleAugmentActive = false;
            Player.ClearBuff(ModContent.BuffType<MuscleAugmentBuff>());
            Player.ClearBuff(ModContent.BuffType<PartialAugmentBuff>());
        }

        public int CalculateStageDamage(int initial, int adequation, int intermediate, int advanced, int finalDmg)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            return transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => initial,
                QuirkStage.Adequation => adequation,
                QuirkStage.Intermediate => intermediate,
                QuirkStage.Advanced => advanced,
                QuirkStage.Final => finalDmg,
                _ => initial
            };
        }

        public override void FrameEffects()
        {
            if (Player.HasBuff(ModContent.BuffType<MuscleAugmentBuff>()))
            {
                Player.body = EquipLoader.GetEquipSlot(Mod, "MuscularBody", EquipType.Body);
            }
            else if (Player.HasBuff(ModContent.BuffType<PartialAugmentBuff>()))
            {
                Player.body = EquipLoader.GetEquipSlot(Mod, "MuscularBodyArm", EquipType.Body);
            }
        }

        public void ModifyPunch(ref float projSpeed, ref int baseDamage, ref int numberOfPunches, ref int mainProjType, ref int extraProjType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.MuscleAugmentation))
                return;

            if (isMuscleAugmentActive)
            {
                projSpeed = 30;
                baseDamage = 15;
                mainProjType = ModContent.ProjectileType<PunchAttackProj>();
                extraProjType = ModContent.ProjectileType<SuperPunchProj>();
                numberOfPunches = 1;
            }
            else if (isPartialMuscleAugmentActive)
            {
                projSpeed = 24;
                baseDamage = 10;
                mainProjType = ModContent.ProjectileType<PunchAttackProj>();
                extraProjType = ModContent.ProjectileType<SuperPunchProj>();
                numberOfPunches = 1;
            }
        }

        public override void PostUpdateMiscEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.MuscleAugmentation))
            {
                StrainPenaltyPerSecond = 0;
                return;
            }

            if (isMuscleAugmentActive)
            {
                StrainPenaltyPerSecond = 1;
            }
            else
            {
                StrainPenaltyPerSecond = 0;
            }
        }

        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash,
            ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.MuscleAugmentation))
                return;

            if (isMuscleAugmentActive)
            {
                float dashSpeed = transPlayer.CurrentStage switch
                {
                    QuirkStage.Initial => 15f,
                    QuirkStage.Adequation => 20f,
                    QuirkStage.Intermediate => 25f,
                    QuirkStage.Advanced => 45f,
                    QuirkStage.Final => 50f,
                    _ => 50f
                };

                if (transPlayer.CurrentStage >= QuirkStage.Adequation)
                {
                    isEnhanced = true;
                    dustType = DustID.Cloud;
                    explosionColor = Color.White;
                }

                speed = dashSpeed;
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, Player.position);
            }
        }

        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            if (isMuscleAugmentActive)
            {
                // PlayerDrawLayers.Torso.Hide();
                // PlayerDrawLayers.ArmOverItem.Hide();
                // PlayerDrawLayers.ArmorLongCoat.Hide();
            }
        }
    }
}