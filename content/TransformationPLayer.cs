using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Audio;
using System.Collections.Generic; 
using MyHeroMod.content.System;
using MyHeroMod.content.System.BasePlayer;

namespace MyHeroMod.content
{
    // --- ENUMS ---
    

    public enum QuirkType { Quirkless, OneForAll9th, OneForAll8th,
                            Explosion, HellFlames, BlueFlames, HalfColdHalfHot,
                            Float, Gearshift, FaJin, SmokeScreen, DangerSense,
                            BlackWhip }

    // Waiting Implementation 
   
                            
    public enum QuirkStage { Initial, Adequation, Intermediate, Advanced, Final }

    // --- CLASSE DO JOGADOR ---
    public class TransformationPlayer : BasePlayer
    {
        public QuirkType SelectedQuirk = QuirkType.Quirkless;
        public QuirkStage CurrentStage = QuirkStage.Initial;

        public bool ManualStageOverride = false;
        
        public QuirkSkills ActiveForm = QuirkSkills.None;

        public QuirkSkills Slot1 = QuirkSkills.None;
        public QuirkSkills Slot2 = QuirkSkills.None;
        public QuirkSkills Slot3 = QuirkSkills.None;
        public QuirkSkills Slot4 = QuirkSkills.None;

        // Lista de skills desbloqueadas
        public List<QuirkSkills> UnlockedSkills = new List<QuirkSkills>();


        // --- SAVE & LOAD ---
        public override void SaveData(TagCompound tag)
        {
            tag["SelectedQuirk"] = (int)SelectedQuirk;
            tag["CurrentStage"] = (int)CurrentStage;
            tag["Slot1"] = (int)Slot1;
            tag["Slot2"] = (int)Slot2;
            tag["Slot3"] = (int)Slot3;
            tag["Slot4"] = (int)Slot4;
        }

        

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("SelectedQuirk")) SelectedQuirk = (QuirkType)tag.GetInt("SelectedQuirk");
            if (tag.ContainsKey("CurrentStage")) CurrentStage = (QuirkStage)tag.GetInt("CurrentStage");
            if (tag.ContainsKey("Slot1")) Slot1 = (QuirkSkills)tag.GetInt("Slot1");
            if (tag.ContainsKey("Slot2")) Slot2 = (QuirkSkills)tag.GetInt("Slot2");
            if (tag.ContainsKey("Slot3")) Slot3 = (QuirkSkills)tag.GetInt("Slot3");
            if (tag.ContainsKey("Slot4")) Slot4 = (QuirkSkills)tag.GetInt("Slot4");
            
            
            UpdateUnlockedSkills();
        }

        
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
{
    // 1. LÓGICA DO MENU
        if (KeybindSystem.SkillMenu.JustPressed)
        {
            UISystem.ToggleSkillMenu();
        }

        
        if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(Slot1);
        if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(Slot2);
        if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(Slot3);
        if (KeybindSystem.SkillSlot4.JustPressed) ExecuteSkill(Slot4);
        // if (KeybindSystem.SkillSlot1.JustPressed) { Main.NewText("BOTÃO APERTADO!"); ExecuteSkill(Slot1); }

        
    }

        public override void OnEnterWorld() {
        UpdateUnlockedSkills();
}

        public void UpdateUnlockedSkills() {
        UnlockedSkills.Clear();

    
        foreach (var skillId in SkillLibrary.GetAllIds()) {
            var skill = SkillLibrary.GetSkill(skillId);
            if (skill != null && skill.CheckUnlock(this)) {
            UnlockedSkills.Add(skillId);
            }
    }
}

        
    
    }
    }
