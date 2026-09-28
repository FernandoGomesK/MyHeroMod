using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass; 
namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot
{
   
    public partial class HalfColdHalfHotPlayer : BaseIceAndFirePlayer 
    {
        public override string SourceName => "Half-Cold Half-Hot";
        public override bool HasFlameResistance => false;
        public override bool IsStrainActive => Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.HalfColdHalfHot);

        public override int MaxTemperature => 300;
        public override int MinTemperature => -300; 
        public override int FlashfireHeatRate => 10;
        public override bool PhosphorFreezesTemperature => true;
        public override bool PhosphorTurnsOff => true;

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot)) return;

            base.PostUpdateEquips();
        }

        public override void PostUpdate()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot)) 
            {
                HeatPerSecond = 0;
                StrainPenaltyPerSecond = 0;
                return;
            }
            base.PostUpdate(); 
        }

       
    }
}