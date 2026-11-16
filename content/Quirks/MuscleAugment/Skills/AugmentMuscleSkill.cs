using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Microsoft.Xna.Framework;
using Terraria.ID;

using Terraria.Audio;
using MyHeroMod.content.Quirks.Overclock;
using MyHeroMod.content.Quirks.DarkShadow;
using MyHeroMod.content.Buffs.Darkshadow;
using MyHeroMod.content.Buffs.MuscleAugment;

namespace MyHeroMod.content.Quirks.MuscleAugment.Skills
{

    public class AugmentMuscleSkill : QuirkBaseSkill
    {
        public override string Name => "Augment Muscle";
        public override string Description => "Increase your muscle mass, increasing strength and speed.";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/Darkshadow/DarkshadowIcon";
        public override string Category => "Muscle Augmentation";
        public override int BaseCooldown => 60;
        public override QuirkType RequiredQuirk => QuirkType.MuscleAugmentation;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;


        public override void OnUse(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<MuscleAugmentBuff>()))
            {
                player.ClearBuff(ModContent.BuffType<MuscleAugmentBuff>());


            }
            else
            {

                var transformPlayer = player.GetModPlayer<TransformationPlayer>();

                player.AddBuff(ModContent.BuffType<MuscleAugmentBuff>(), 360000);


            }
        }
    }
}