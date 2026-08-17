
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills;
using Terraria;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.BlueflameSkills
{
 public class BlueflameFlashfireFist : BaseToggleFlashfireFistSkill
    {
        public override string Name => "Blueflame_FlashfireFist";
        public override string Category => "Blueflame";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/Blueflame/BlueTorchIcon";
        
        public override QuirkType RequiredQuirk => QuirkType.Blueflame;
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
                return "Crazy Torch";
            }
            
        }
    }
}