using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;



public class HellFlashFireFistSkill : QuirkSkill
{
     public override string Name => "Flash Fire Fist";
    public override string Description => "Toggle Flash Fire Fist";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
     public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();

        if (player.HasBuff(ModContent.BuffType<FlashFireFistBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<FlashFireFistBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<FlashFireFistBuff>(), 3600);
            hellPlayer.CurrentHeat += 15;
             
        }
    }
}


