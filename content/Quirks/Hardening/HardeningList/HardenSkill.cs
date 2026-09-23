using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;

public class ToggleHardenSkill : BaseToggleSkill
{
    public override string Name => "Harden";
    public override string Description => "Harden your skin making you more resistant";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Hardening";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Hardening;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

    public override int BuffType => ModContent.BuffType<HardenBuff>();



}