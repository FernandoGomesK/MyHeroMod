using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.HalfColdHalfHot;


public class FlashFireFistSkill : QuirkSkill
{
     public override string Name => "Flash Fire Fist";
    public override string Description => "Toggle Flash Fire Fist";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
     public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

        if (player.HasBuff(ModContent.BuffType<FlashFireFistBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<FlashFireFistBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<FlashFireFistBuff>(), 3600);
            hchhPlayer.temperature += 15;
             
        }
    }
}


