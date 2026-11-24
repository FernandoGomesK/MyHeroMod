using Microsoft.Xna.Framework;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.Hardening.Projectiles;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueVanishingFist;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.MuscleAugment;
using MyHeroMod.content.Quirks.MuscleAugment.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;



namespace MyHeroMod.content.Quirks.Hardening.HardeningList
{


    public class MusclePunchThroughSkill : QuirkBaseSkill
    {

        public override string Name => "Muscle Punch Through";

        public override string GetDisplayName(Player player)
        {
            return "Punch Through";
        }

        public override string Description => "Dash and punch through a foe";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/MuscleAugmentation/MusclePunchIcon";
        public override string Category => "Muscle Augmentation";

        public override int BaseCooldown => 320;

        public override QuirkType RequiredQuirk => QuirkType.MuscleAugmentation;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            int type = ModContent.ProjectileType<MusclePunchThroughProj>();
            if (player.ownedProjectileCounts[type] > 0)
                return;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero,
                type,
                0,
                0f,
                player.whoAmI
            );
        }
    }
}