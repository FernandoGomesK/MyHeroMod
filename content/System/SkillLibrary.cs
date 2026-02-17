using System.Collections.Generic;
using MyHeroMod.content.Quirks.Smokescreen; // Adiciona os namespaces das tuas skills
using MyHeroMod.content.System; // Adiciona os namespaces das tuas skills


    public static class SkillLibrary
    {
        // Dicionário que mapeia o Enum para a instância da Skill
        private static readonly Dictionary<QuirkSkills, QuirkSkill> _skills = new()
        {
            // General Skills

            
            { QuirkSkills.Dash, new DashSkill() },


            
            { QuirkSkills.Smokescreen, new SmokescreenSkill() },
            { QuirkSkills.Float, new FloatSkill() }, 
        };

        public static QuirkSkill GetSkill(QuirkSkills id)
        {
            return _skills.TryGetValue(id, out var skill) ? skill : null;
        }
    }
