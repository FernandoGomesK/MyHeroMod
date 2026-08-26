using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;


public class SmokescreenSkill : BaseToggleSkill
{
    public override string Name => "Smoke Screen";
    public override string Description => "creates a Smokescreen";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Smokescreen/ToggleSmokescreenIcon";
    public override string Category => "Smokescreen";
    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.SmokeScreen;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override QuirkStage RequiredOfaStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    
    public override int BuffType => ModContent.BuffType<SmokescreenBuff>();


}

