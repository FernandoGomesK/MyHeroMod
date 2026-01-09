using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;




namespace MyHeroMod.content
{
    
     public enum QuirkSkills
        {
            None,
            // Ofa
            SuperJump,

            // Ofa 8th
            PrimeDetroitSmash,
            StockPile,
            StockPileMaximum,


            // Ofa 9th
            DelawareSmash,
            DetroitSmash,
            OneForAllFullCowling5, // Full Cowling 5%
            OneForAllFullCowling8, // Full Cowling 8%
            OneForAllFullCowling45, // Full Cowling 45%
            BlackWhipHook, 
            OneForAllFullCowling100, // Full Cowling 100%
            BlackWhipSurge,
            Float,
            DangerSense,
            FaJinStore,
            SmokeScreen,
            Gearshift,

            // Hell Flames
            FlashFireFist,
            ProminenceBurn,
            JetBurn,
            HellSpider,
            IgnitedArrow,
            // Blue Flames

            // Explosion
            ExplosionBlast,
            StunGrenade,
            ApShot,
            ApMachineGun,
            HowitzerImpact,
            Cluster,

            

        }
    public enum QuirkType{ Quirkless, OneForAll9th, OneForAll8th, Explosion, HellFlames, BlueFlames, HalfColdHalfHot }
    public enum QuirkStage{ Initial, Adequation, Intermediate, Advanced, Final }
    public class TransformationPlayer : ModPlayer
    {
        public QuirkType SelectedQuirk = QuirkType.Quirkless;
        public QuirkStage CurrentStage = QuirkStage.Initial;
        
        public QuirkSkills ActiveForm = QuirkSkills.None;

        public QuirkSkills Slot1 = QuirkSkills.SuperJump;
        public QuirkSkills Slot2 = QuirkSkills.DelawareSmash;
        public QuirkSkills Slot3 = QuirkSkills.None;
        public QuirkSkills TransformSlot = QuirkSkills.OneForAllFullCowling5;
    

    public override void SaveData(TagCompound tag)
        {
            tag["SelectedQuirk"] = (int)SelectedQuirk;
            tag["CurrentStage"] = (int)CurrentStage;
            tag["Slot1"] = (int)Slot1;
            tag["Slot2"] = (int)Slot2;
            tag["Slot3"] = (int)Slot3;
            tag["TransformSlot"] = (int)TransformSlot;
        }

    public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("SelectedQuirk")) SelectedQuirk = (QuirkType)tag.GetInt("SelectedQuirk");
            if (tag.ContainsKey("CurrentStage")) CurrentStage = (QuirkStage)tag.GetInt("CurrentStage");
            if (tag.ContainsKey("Slot1")) Slot1 = (QuirkSkills)tag.GetInt("Slot1");
            if (tag.ContainsKey("Slot2")) Slot2 = (QuirkSkills)tag.GetInt("Slot2");
            if (tag.ContainsKey("Slot3")) Slot3 = (QuirkSkills)tag.GetInt("Slot3");
            if (tag.ContainsKey("TransformSlot")) TransformSlot = (QuirkSkills)tag.GetInt("TransformSlot");
        }
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
{
    // Agora o menu abre independente da Quirk, e só roda UMA vez por frame.
    if (KeybindSystem.SkillMenu.JustPressed)
    {
        UISystem.ToggleSkillMenu();
    }
}
    }
}
        
        
    
    
    

  