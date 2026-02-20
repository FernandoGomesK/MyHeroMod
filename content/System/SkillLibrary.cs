using System.Collections.Generic;
using MyHeroMod.content.Quirks.Smokescreen; 
using MyHeroMod.content.System; // Adiciona os namespaces das tuas skills



    public static class SkillLibrary
    {
        // Dicionário que mapeia o Enum para a instância da Skill
        private static readonly Dictionary<QuirkSkills, QuirkSkill> _skills = new()
        {
            // General Skills

            
            { QuirkSkills.Dash, new DashSkill() },


            // Smokescreen 

            { QuirkSkills.Smokescreen, new SmokescreenSkill() },

            // Float
            { QuirkSkills.Float, new FloatSkill() }, 

            // DangerSense
            
            { QuirkSkills.DangerSense, new DangerSenseSkill() },

            // GearShift

            { QuirkSkills.Gearshift, new GearShiftSkill() },

            {QuirkSkills.DelawareSmash, new DelawareSmashSkill() },

            {QuirkSkills.DetroitSmash, new DetroitSmashSkill() },

            {QuirkSkills.OneForAllFullCowling5, new FullCowling5() },

            {QuirkSkills.OneForAllFullCowling8, new FullCowling10() },


             

        
        };

        public static QuirkSkill GetSkill(QuirkSkills id)
        {
            return _skills.TryGetValue(id, out var skill) ? skill : null ;
        }

        public static List<QuirkSkills> GetAllIds() => new List<QuirkSkills>(_skills.Keys);
    }
