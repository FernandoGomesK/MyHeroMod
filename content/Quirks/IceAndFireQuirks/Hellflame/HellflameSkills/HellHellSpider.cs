using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles.ContinuousBlast.HellSpider;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills;
using MyHeroMod.content.System;
using Terraria.ModLoader;


namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Skills 
{
    public class EndeavorHellSpiderSkill : BaseHellSpiderSkill
    {
        public override string Name => "EndeavorHellSpider"; 
        
        public override QuirkType RequiredQuirk => QuirkType.HellFlames;
        public override QuirkStage RequiredStage => QuirkStage.Intermediate;
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/Hellflame/HellHellSpiderIcon";
        protected override int HeatCost => 45;

        public override int BaseCooldown => 1500;

        protected override int HellSpiderProjType => ModContent.ProjectileType<HellSpiderController>();

        protected override int CalculateDamage(TransformationPlayer transPlayer)
        {
            return transPlayer.CurrentStage switch {
                QuirkStage.Adequation => 65, 
                QuirkStage.Intermediate => 80,
                QuirkStage.Advanced => 110, 
                QuirkStage.Final => 150, 
                _ => 65
            };
        }
    }
}