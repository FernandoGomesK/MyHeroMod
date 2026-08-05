using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;

public class ToggleFlightShieldSkill : BaseToggleSkill
{
    public override string Name => "Shield";
    public override string Description => "Cover yourself with a shield";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Flight";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Flight;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;

    public override int BuffType => ModContent.BuffType<FlightShieldBuff>();

   
}