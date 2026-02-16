using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using MyHeroMod.content.System.BasePlayer; // Importa a classe BasePlayer para acessar os

namespace MyHeroMod.content.System.BasePlayer
{
   
    public abstract class BasePlayer : ModPlayer
    {
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
            // Gerencia Cooldowns
            // Criamos uma lista temporária para evitar erros de "coleção modificada" ao iterar
            var keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skillId in keys) {
                if (SkillCooldowns[skillId] > 0) 
                    SkillCooldowns[skillId]--;
            }        
        }

        public virtual void ExecuteSkill(QuirkSkills skillId) {
            // Busca a lógica da skill na biblioteca central
            var skill = SkillLibrary.GetSkill(skillId);

            if (skill != null && skill.CanUse(Player) && GetCooldown(skillId) <= 0) {
                // Executa a lógica (OnUse)
                skill.OnUse(Player);

                // Define o cooldown automaticamente baseado na classe da skill
                SetCooldown(skillId, skill.BaseCooldown);
            }
        }
    }
}
    