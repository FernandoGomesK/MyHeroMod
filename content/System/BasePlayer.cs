using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.System.BasePlayer
{
    public partial class BasePlayer : ModPlayer
    {
        

        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

        public override void OnRespawn() => ResetAll();

        public void ResetAll()
        {
            
            SkillCooldowns.Clear();
        }

        public override void ResetEffects()
        {
            
           
        }

        public override void PostUpdate()
        {
            
        }


        public override void PreUpdate()
        { 
            // 1. Gerencia Cooldowns
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
            }        
        }
        

        
        }
    }