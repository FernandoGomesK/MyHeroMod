using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass; 

namespace MyHeroMod.content.Quirks.HellFlames
{
    public partial class HellFlamesPlayer : BaseIceAndFirePlayer
    {
        public override int MaxTemperature
        {
            get 
            {
            
                var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

        
                if (mainPlayer.CurrentStage == QuirkStage.Final)
                {
                    return 250;
                }

            
                return 200;
            }
        }
        public override int MinTemperature => 0; 
        public override int FlashfireHeatRate => 5; 
            
       public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.HellFlames)) return;

            base.PostUpdateEquips();
        }

        public override void PostUpdate()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                HeatPerSecond = 0;
                StrainPenaltyPerSecond = 0;
                return;
            }
            base.PostUpdate();
        }
    }
}