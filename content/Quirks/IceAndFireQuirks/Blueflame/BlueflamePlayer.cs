using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass; 
namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame
{
   
    public partial class BlueflamePlayer : BaseIceAndFirePlayer 
    {
        
        public override int MaxTemperature => 100;
        public override int MinTemperature => -200; 
        public override int FlashfireHeatRate => 20;
        public override int PhosphorCoolingRate => 10;

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.Blueflame)) return;

            base.PostUpdateEquips();
        }

        public override void PostUpdate()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.Blueflame)) 
            {
                HeatPerSecond = 0;
                StrainPenaltyPerSecond = 0;
                return;
            }
            base.PostUpdate(); 
        }

        protected override void ApplyMaxStrainPenalty()
        {
            
        }
    }
}