using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Audio;
using System.Collections.Generic; 
using MyHeroMod.content.System;   

namespace MyHeroMod.content
{
    // --- ENUMS ---
    

    public enum QuirkType { Quirkless, OneForAll9th, OneForAll8th,
                            Explosion, HellFlames, BlueFlames, HalfColdHalfHot,
                            Float, Gearshift, FaJin, SmokeScreen, DangerSense,
                            BlackWhip }
                            
    public enum QuirkStage { Initial, Adequation, Intermediate, Advanced, Final }

    // --- CLASSE DO JOGADOR ---
    public class TransformationPlayer : ModPlayer
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

        public float DodgeChance = 0f;

        public override void ResetEffects()
        {
            DodgeChance = 0f;
        }

        // --- MÉTODO DE ATUALIZAR SKILLS (Agora dentro da classe!) ---
        public void UpdateUnlockedSkills()
        {
            // Limpa skills antigas
            UnlockedSkills.Clear();

            // Percorre todas as skills do jogo
            foreach (var skillEntry in SkillData.SkillList)
            {
                var skillId = skillEntry.Key;
                var info = skillEntry.Value;

                // 1. Verifica se a Quirk do jogador bate com a skill
                if (info.RelatedQuirks.Contains(this.SelectedQuirk))
                {
                    // 2. LÓGICA DE EXCEÇÃO (Desbloqueio Antecipado)
                    
                    
                    if (this.SelectedQuirk == QuirkType.SmokeScreen && skillId == QuirkSkills.Smokescreen)
                    {
                        UnlockedSkills.Add(QuirkSkills.Smokescreen);
                        continue; 
                    }

                    if (this.SelectedQuirk == QuirkType.Gearshift && skillId == QuirkSkills.Gearshift)
                    {
                        UnlockedSkills.Add(QuirkSkills.Gearshift);
                        continue; 
                    }

                    if (this.SelectedQuirk == QuirkType.Float && skillId == QuirkSkills.Float)
                    {
                        UnlockedSkills.Add(QuirkSkills.Float);
                        continue; 
                    }

                    if (this.SelectedQuirk == QuirkType.FaJin && skillId == QuirkSkills.FaJinStore)
                    {
                        UnlockedSkills.Add(QuirkSkills.FaJinStore);
                        continue; 
                    }
                    
                    // Se eu sou o usuário de Danger Sense Puro
                    if (this.SelectedQuirk == QuirkType.DangerSense && skillId == QuirkSkills.DangerActivate)
                    {
                        UnlockedSkills.Add(QuirkSkills.DangerActivate);
                        continue;
                    }

                    if (this.SelectedQuirk == QuirkType.BlackWhip && (skillId == QuirkSkills.BlackWhipHook || skillId == QuirkSkills.BlackWhipSurge))
                    {
                        UnlockedSkills.Add(skillId);
                        continue;
                    }

                    // 3. Verificação Padrão por Estágio
                    if (this.CurrentStage >= info.MinStage)
                    {
                        UnlockedSkills.Add(skillId);
                    }
                }
            }
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
            if (KeybindSystem.SkillMenu.JustPressed)
            {
                UISystem.ToggleSkillMenu();
            }   
        }

        // --- ATUALIZAÇÃO (Evolução Automática) ---
        public override void PreUpdate()
        {
            // O próprio Player já é 'Player', não precisa pegar GetModPlayer de si mesmo para variáveis locais
            // Mas para garantir que estamos a mexer na instância certa:
            
            if (!ManualStageOverride)
            {
                // Guarda o estágio antigo para ver se mudou
                var oldStage = CurrentStage;

                // Lógica de Evolução
                if (NPC.downedMoonlord)
                {
                    CurrentStage = QuirkStage.Final;
                }
                else if (NPC.downedPlantBoss)
                {
                    CurrentStage = QuirkStage.Advanced;
                }
                else if (Main.hardMode)
                {
                    CurrentStage = QuirkStage.Intermediate;
                }
                else 
                {
                    CurrentStage = QuirkStage.Adequation;
                }

                // Se o estágio mudou, recalcula as skills
                if (oldStage != CurrentStage)
                {
                    UpdateUnlockedSkills();
                }
            }
        }
    }
}