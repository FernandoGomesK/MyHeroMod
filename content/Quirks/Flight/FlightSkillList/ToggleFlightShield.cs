using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;

public class ToggleFlightShieldSkill : QuirkSkill
{
    public override string Name => "Shield";
    public override string Description => "Cover yourself with a shield";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Flight;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<FlightShieldBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<FlightShieldBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<FlightShieldBuff>(), 360000000);
             
        }
    }
}