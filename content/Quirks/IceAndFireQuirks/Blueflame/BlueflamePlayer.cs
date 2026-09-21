using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass; 
using MyHeroMod.content.System;
using Terraria;
using Terraria.ModLoader;
namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame
{
   
    public partial class BlueflamePlayer : BaseIceAndFirePlayer 
    {
        
        public override int MaxTemperature => 100;
        public override int MinTemperature => -200; 
        public override int FlashfireHeatRate => 20;
        public override int PhosphorCoolingRate => 10;
        public override bool IsLethalStrain => true;
        public override bool HasFlameResistance
        {
            get
            {
                var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
                return mainPlayer.Nature == NatureType.HeatResistance || mainPlayer.Nature == NatureType.ThermalResistance;
            }
        }
        public override string SourceName => "Blueflame";
        public override bool IsStrainActive => Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.Blueflame);

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