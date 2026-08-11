using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills;
using Terraria;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.HellFlameSkills
{
    
public class HellflameFlashFireFist : BaseToggleFlashfireFistSkill
    {
      
        public override string Name => "Hellflame_FlashfireFist"; 
        public override string Category => "HellFlames";
        
        public override QuirkType RequiredQuirk => QuirkType.HellFlames;
        public override QuirkStage RequiredStage => QuirkStage.Adequation;
      
        public override string GetDisplayName(Player player) => "Flashfire Fist";
    }
}