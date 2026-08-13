using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass; 

namespace MyHeroMod.content.Quirks.HellFlames
{
    public partial class HellFlamesPlayer : BaseIceAndFirePlayer
    {
        public override int MaxTemperature => 300;
        public override int MinTemperature => 0; 
        public override int FlashfireHeatRate => 20; 
        
        public override void PostUpdate()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.HellFlames)) return;
            
            base.PostUpdate(); 
        }

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.HellFlames)) return;
            
            base.PostUpdateEquips();
        }
    }
}