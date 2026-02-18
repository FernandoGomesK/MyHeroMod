using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.GeneralSkills1;
using MyHeroMod.content.System.BasePlayer; //
using MyHeroMod.content.System;


namespace MyHeroMod.content.Quirks.Smokescreen
{
    public partial class SmokescreenPlayer : BasePlayer
    {
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

        
            if (MainPlayer.SelectedQuirk == QuirkType.SmokeScreen || MainPlayer.SelectedQuirk == QuirkType.OneForAll9th) 
            {
                if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot1);
                if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot2);
                if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot3);
                if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.TransformSlot);
            }      
        }

        private void ExecuteSkill(TransformationPlayer mainPlayer, QuirkSkills skill)
        {
            var skillData = SkillLibrary.GetSkill(skill);
            if (skillData != null && skillData.CanUse(Player)) {
            skillData.OnUse(Player);
            SetCooldown(skill, skillData.BaseCooldown);
            }
            var generalSkills = Player.GetModPlayer<GeneralSkills1.GeneralSkills>(); // Caminho completo para evitar confusão

            if (SkillCooldowns.ContainsKey(skill) && SkillCooldowns[skill] > 0)
            {
                Main.NewText("On cooldown!", Color.White);
                return;
            }

            
        }

    
        }
    }
