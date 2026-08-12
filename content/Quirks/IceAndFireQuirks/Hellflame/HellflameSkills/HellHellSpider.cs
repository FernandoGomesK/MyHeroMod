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

        protected override int HellSpiderProjType => ModContent.ProjectileType<HellSpiderController>();

        protected override int CalculateDamage(TransformationPlayer transPlayer)
        {
            return transPlayer.CurrentStage switch {
                QuirkStage.Adequation => 180, 
                QuirkStage.Intermediate => 280,
                QuirkStage.Advanced => 480, 
                QuirkStage.Final => 950, 
                _ => 180
            };
        }
    }
}