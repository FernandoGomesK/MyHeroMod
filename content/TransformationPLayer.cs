using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;




namespace MyHeroMod.content
{
     public enum OfaSkills
        {
            None,
            SuperJump,
            DelawareSmash,
            DetroitSmash,
            OneForAllFullCowling5, // Full Cowling 5%
            OneForAllFullCowling8, // Full Cowling 8%
            BlackWhipSurge,
        }
    public enum QuirkType{ Quirkless, OneForAll9th, Explosion, OneForAll8th }
    public enum QuirkStage{ Initial, Adequation, Intermediate, Advanced, Final }
    public class TransformationPlayer : ModPlayer
    {
        public QuirkType SelectedQuirk = QuirkType.Quirkless;
        public QuirkStage CurrentStage = QuirkStage.Initial;
        
        public OfaSkills ActiveForm = OfaSkills.None;

        public OfaSkills Slot1 = OfaSkills.SuperJump;
        public OfaSkills Slot2 = OfaSkills.DelawareSmash;
        public OfaSkills Slot3 = OfaSkills.None;
        public OfaSkills TransformSlot = OfaSkills.OneForAllFullCowling5;

    }
}
        
        
    
    
    

  