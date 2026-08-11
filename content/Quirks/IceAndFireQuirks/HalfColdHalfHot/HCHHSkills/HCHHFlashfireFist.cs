using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills;
using Terraria;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.HCHHSkills
{
    
public class HCHHFlashfireFist : BaseToggleFlashfireFistSkill
    {
        public override string Name => "HCHH_FlashfireFist";
        public override string Category => "HalfColdHalfHot";
        
        public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
        public override QuirkStage RequiredStage => QuirkStage.Adequation;

        public override string GetDisplayName(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                return "Flashfire Fist";
            } 
            else
            {
                return "Ignite";
            }
            
        }
    }
}