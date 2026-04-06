using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;

public class ToggleFlight : QuirkSkill
{
    public override string Name => "Flight";
    public override string Description => "Begin To fly to the skies";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Flight;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<FlightBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<FlightBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<FlightBuff>(), 360000000);
             
        }
    }
}