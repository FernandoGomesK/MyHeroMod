using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;

public class ToggleSlideSkill : BaseToggleSkill
{
    public override string Name => "Slide And Glide";
    public override string Description => "Slide As long as you're in contact with the ground";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "SlideAndGlide";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.SlideAndGlide;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

    public override int BuffType => ModContent.BuffType<SlideAndGlideBuff>();


    
}