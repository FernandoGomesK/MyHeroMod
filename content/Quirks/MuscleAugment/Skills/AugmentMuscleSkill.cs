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
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/MuscleAugmentation/MuscleAugmentIcon";
        public override string Category => "Muscle Augmentation";
        public override int BaseCooldown => 60;
        public override QuirkType RequiredQuirk => QuirkType.MuscleAugmentation;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;


        public override void OnUse(Player player)
        {
            int buff = ModContent.BuffType<MuscleAugmentBuff>();

            if (player.HasBuff(buff))
            {
                var musclePlayer = player.GetModPlayer<MuscleAugmentPlayer>();
                player.ClearBuff(buff);
                player.ClearBuff(ModContent.BuffType<PartialAugmentBuff>());
                musclePlayer.isPartialMuscleAugmentActive = false;
                return;
            }

            int type = ModContent.ProjectileType<Projectiles.AugmentBodyTransformProj>();
            if (player.ownedProjectileCounts[type] > 0)
                return;
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/MuscleStressSound") with { Volume = 0.6f }, player.position);
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, type, 0, 0f, player.whoAmI);
        }
    }
}