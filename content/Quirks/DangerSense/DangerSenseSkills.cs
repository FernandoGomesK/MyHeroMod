using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using Terraria.ID;
using MyHeroMod.content.System;
using Terraria.Audio;
using Terraria.DataStructures;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;



namespace MyHeroMod.content.Quirks.DangerSense
{
    public partial class DangerSensePlayer : BasePlayer
    {
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (MainPlayer.SelectedQuirk == QuirkType.DangerSense) 
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
    

        private void DangerActivate(TransformationPlayer mainPlayer)
        {
            

            if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                Player.AddBuff(ModContent.BuffType<OvertimeBuff>(), 300);
                IsOvertimeActive = true;
                IsDangerSenseActive = true;
                CombatText.NewText(Player.getRect(), Color.Yellow, "Overtime!");
            }
            else
            {
                ToggleDangerSense();
            }
            }
            
            
            

        private void ToggleDangerSense()
        {
            
            
            if (!IsDangerSenseActive)
            {
                CombatText.NewText(Player.getRect(), Color.Orange, "Danger Sense: ON");
                SoundEngine.PlaySound(SoundID.Item4, Player.position);
            }
            else
            {
                CombatText.NewText(Player.getRect(), Color.Gray, "Danger Sense: OFF");
                SoundEngine.PlaySound(SoundID.Item4, Player.position);
            }
        }

             
            
        }

        
        
}
