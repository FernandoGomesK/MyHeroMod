using System.Configuration;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles.ContinuousBlast.HellSpider;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Skills
{
    public class BlueHellSpiderSkill : BaseHellSpiderSkill
    {
        public override string Name => "DabiHellSpider"; 
         public override string GetDisplayName(Player player)
        {
            
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
   
            return "Flashfire Fist: Hell Spider"; 
        }
        
        public override QuirkType RequiredQuirk => QuirkType.Blueflame;
        public override QuirkStage RequiredStage => QuirkStage.Intermediate;    
        protected override int HellSpiderProjType => ModContent.ProjectileType<HellSpiderController>();

        protected override int HeatCost => 50;
        public override float FlashfireFistModifier => 2.0f; 
        public override float SurgeArmGauntletModifier => 1.5f;

        public override int BaseCooldown => 1500;

        protected override int CalculateDamage(TransformationPlayer transPlayer)
        {
            return transPlayer.CurrentStage switch {
                QuirkStage.Adequation => 90, 
                QuirkStage.Intermediate => 120,
                QuirkStage.Advanced => 160, 
                QuirkStage.Final => 220, 
                _ => 180
            };
        }
    }
}