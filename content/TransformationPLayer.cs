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
            OneForAllFullCowling5,
            OneForAllFullCowling8,
            BlackWhipSurge,
        }
    public enum QuirkType{ Quirkless, OneForAll9th }
    public enum QuirkStage{ Initial, Adequation, Intermediate, Advanced, Final }
    public class TransformationPlayer : ModPlayer
    {
        public QuirkType SelectedQuirk = QuirkType.Quirkless;
        public QuirkStage CurrentStage = QuirkStage.Initial;
        public bool isTransformationActive = false;

        public OfaSkills Slot1 = OfaSkills.SuperJump;
        public OfaSkills Slot2 = OfaSkills.DelawareSmash;
        public OfaSkills Slot3 = OfaSkills.None;
        public OfaSkills TransformSlot = OfaSkills.None;

        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            // if (KeybindSystem.TransformKey.JustPressed && SelectedQuirk != QuirkType.Quirkless)
            // {
            //     if (CurrentStage == QuirkStage.Initial)
            //     {
            //         Terraria.Main.NewText("You cannot transform at this stage.", Microsoft.Xna.Framework.Color.Red);
            //         return;
            //     }
            //     isTransformationActive = !isTransformationActive;

            //     string msg = isTransformationActive ? "One For All: Full Cowling 5%" : "Deactvated";
            //     Main.NewText(msg, isTransformationActive ? Microsoft.Xna.Framework.Color.LimeGreen : Microsoft.Xna.Framework.Color.White);
            // }
        }
    }
}
        
        
    
    
    

  