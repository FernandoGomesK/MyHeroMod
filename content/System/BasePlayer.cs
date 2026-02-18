using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.Audio;

namespace MyHeroMod.content.System.BasePlayer
{
   
    public abstract class BasePlayer : ModPlayer
    {
       

        public float DodgeChance = 0f;

        
    // A lógica de esquiva genérica
        public override bool FreeDodge(Player.HurtInfo info) {
            if (DodgeChance > 0 && Main.rand.NextFloat() < DodgeChance) {
                Player.SetImmuneTimeForAllTypes(40);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/DangerSenseSound"), Player.position);
                return true;
            }
            return false;
        }
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

        public int GetCooldown(QuirkSkills skill) {
            return SkillCooldowns.TryGetValue(skill, out int timer) ? timer : 0;
        }

        public void SetCooldown(QuirkSkills skill, int ticks) {
            if (SkillCooldowns.ContainsKey(skill))
                SkillCooldowns[skill] = ticks;
            else
                SkillCooldowns.Add(skill, ticks);
        }

        public override void OnRespawn() => ResetAll();

        public void ResetAll() {
            SkillCooldowns.Clear();
        }

        public override void PreUpdate() { 
            
            var keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skillId in keys) {
                if (SkillCooldowns[skillId] > 0) 
                    SkillCooldowns[skillId]--;
            }        
        }

        // parte das Skills


        public virtual void ExecuteSkill(QuirkSkills skillId) {
            
            var skill = SkillLibrary.GetSkill(skillId);

            if (skill != null && skill.CanUse(Player) && GetCooldown(skillId) <= 0) {
                // Executa a lógica (OnUse)
                skill.OnUse(Player);

                // Define o cooldown automaticamente baseado na classe da skill
                SetCooldown(skillId, skill.BaseCooldown);
            }
        }

         public TransformationPlayer TransPlayer => Player.GetModPlayer<TransformationPlayer>();
        public QuirkSkills Slot1 = QuirkSkills.None;
        public QuirkSkills Slot2 = QuirkSkills.None;
        public QuirkSkills Slot3 = QuirkSkills.None;
        public QuirkSkills TransformSlot = QuirkSkills.None;

        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet) {
            // Centraliza o uso das skills
            if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(Slot1);
            if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(Slot2);
            if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(Slot3);
            if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(TransformSlot);
        }

    }

    
}
    