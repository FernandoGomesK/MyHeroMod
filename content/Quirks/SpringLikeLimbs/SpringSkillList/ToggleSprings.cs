using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;

public class ToggleSpringsSkill : BaseToggleSkill
{
    public override string Name => "Spring Like Limbs";
    public override string Description => "Allow your body to behave like Springs, increasing your jump height and allowing you to bounce off the ground";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "SpringLikeLimbs";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.SpringLikeLimbs;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    
    public override int BuffType => ModContent.BuffType<SpringLikeLimbsBuff>();


}