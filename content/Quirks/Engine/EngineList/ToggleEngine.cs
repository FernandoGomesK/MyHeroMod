using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;

public class ToggleEngineSkill : QuirkSkill
{
    public override string Name => "Engine";
    public override string Description => "Start your engines";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Engine;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<EngineBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<EngineBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<EngineBuff>(), 360000000);
             
        }
    }
}