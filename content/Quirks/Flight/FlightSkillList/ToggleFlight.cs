using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;
using KhacesCore.Content.Buffs;

public class ToggleFlight : BaseToggleSkill
{
    public override string Name => "Flight";
    public override string Description => "Begin To fly to the skies";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Flight";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Flight;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    
    public override int BuffType => ModContent.BuffType<FlightBuff>();


    
}