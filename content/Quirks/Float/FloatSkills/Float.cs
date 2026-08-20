using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;
using MyHeroMod.content.Projectiles;

public class FloatSkill : BaseToggleSkill
{
    public override string Name => "Float";
    public override string Description => "Begin To float to the skies";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Float/Float";
    public override string Category => "Float";
    public override int OnomatopoeiaProjType => ModContent.ProjectileType<FloatOnomatopoeia>();


    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Float ;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override QuirkStage RequiredOfaStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;

    public override int BuffType => ModContent.BuffType<FloatBuff>();


}