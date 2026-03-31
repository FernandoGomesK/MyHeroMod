using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;

public class ToggleSlideSkill : QuirkSkill
{
    public override string Name => "Slide And Glide";
    public override string Description => "Slide As long as you're in contact with the ground";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.SlideAndGlide;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<SlideAndGlideBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<SlideAndGlideBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<SlideAndGlideBuff>(), 3600);
             
        }
    }
}