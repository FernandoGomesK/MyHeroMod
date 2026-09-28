using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.GeneralSkills;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Hardening.HardeningList
{

    public class ToggleHardenSkill : BaseToggleSkill
    {
        public override string Name => "Hardening";
        public override string Description => "Harden your skin making you more resistant";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/Harden/HardenIcon";
        
        public override string Category => "Hardening";
        public override string ToggleOnText => "";
        public override string ToggleOffText => "";

        public override int BaseCooldown => 30;
        public override QuirkType RequiredQuirk => QuirkType.Hardening;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;
        public override int OnomatopoeiaProjType => ModContent.ProjectileType<SklitOnomatopoeia>();

        public override int BuffType => ModContent.BuffType<HardenBuff>();



    }
}