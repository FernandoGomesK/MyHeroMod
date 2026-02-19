using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DangerSense;

namespace MyHeroMod.content.System.BasePlayer
{
   
    public abstract class BasePlayer : ModPlayer
    {
       

        public float DodgeChance = 0f;

        
    
        
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

        public override void ResetEffects()
        {
            DodgeChance = 0f;
        }

    
        public override bool FreeDodge(Player.HurtInfo info) {
            if (DodgeChance > 0 && Main.rand.NextFloat() < DodgeChance) {
                Player.SetImmuneTimeForAllTypes(40);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/DangerSenseSound"), Player.position);

                if (Player.TryGetModPlayer<DangerSensePlayer>(out var dsPlayer)) {
                dsPlayer.triggerVisual(); 
                }
                return true;
            }
            return false;
        }

        public override void PreUpdate() { 
    
        var skills = new List<QuirkSkills>(SkillCooldowns.Keys);
    
        foreach (var skillId in skills) {
            if (SkillCooldowns[skillId] > 0) {
                SkillCooldowns[skillId]--;
        }
        }        
        }

        // parte das Skills


       public virtual void ExecuteSkill(QuirkSkills skillId) {
        if (skillId == QuirkSkills.None) return;
        var skill = SkillLibrary.GetSkill(skillId);
        if (skill == null) return;

        if (GetCooldown(skillId) <= 0 && skill.CanUse(Player)) {
            skill.OnUse(Player);
            SetCooldown(skillId, skill.BaseCooldown);
        }
    }
        public TransformationPlayer TransPlayer => Player.GetModPlayer<TransformationPlayer>();
       

        

    }

    
}
    