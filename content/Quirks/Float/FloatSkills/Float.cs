using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;

public class FloatSkill : BaseToggleSkill
{
    public override string Name => "Float";
    public override string Description => "Begin To float to the skies";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Float ;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;


    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<FloatBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<FloatBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<FloatBuff>(), 3600);
             
        }
    }
}