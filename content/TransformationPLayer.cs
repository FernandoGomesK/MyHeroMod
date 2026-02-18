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
        public QuirkSkills TransformSlot = QuirkSkills.None;

        // Lista de skills desbloqueadas
        public List<QuirkSkills> UnlockedSkills = new List<QuirkSkills>();

        // public float DodgeChance = 0f;

        public override void ResetEffects()
        {
            DodgeChance = 0f;
        }

    
        

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (DodgeChance > 0)
            {
                if (Main.rand.NextFloat() < DodgeChance)
                {
                    Player.SetImmuneTimeForAllTypes(40); // Invencibilidade longa
                
                    // Tenta chamar o efeito visual do Danger Sense (se o ModPlayer existir)
                    try {
                        // Player.GetModPlayer<DangerSensePlayer>().triggerVisual(); // Descomente se tiver esse método
                    } catch { /* Ignora se der erro */ }

                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/DangerSenseSound"), Player.position);
                    
                    return true; // Retornar TRUE bloqueia o dano
                }
            }
            return false;
        }

        // --- SAVE & LOAD ---
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
            
            // Recalcula o que está desbloqueado ao entrar no mundo
            UpdateUnlockedSkills();
        }

        // --- INPUTS (Menu de Skills) ---
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
{
    // 1. LÓGICA DO MENU
        if (KeybindSystem.SkillMenu.JustPressed)
        {
            UISystem.ToggleSkillMenu();
        }

        // 2. LÓGICA DAS SKILLS (Centralizada aqui)
        // Aqui usamos as variáveis locais Slot1, Slot2, etc., que estão salvas nesta classe.
        if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(Slot1);
        if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(Slot2);
        if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(Slot3);
        if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(TransformSlot);
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
