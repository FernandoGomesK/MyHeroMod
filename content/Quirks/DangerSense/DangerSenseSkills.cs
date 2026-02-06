using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using Terraria.ID;
using MyHeroMod.content.System;
using Terraria.Audio;
using Terraria.DataStructures;
using MyHeroMod.content.Buffs;



namespace MyHeroMod.content.Quirks.DangerSense
{
    public partial class DangerSensePlayer : ModPlayer
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
            if (SkillCooldowns.ContainsKey(skill) && SkillCooldowns[skill] > 0)
            {
                Main.NewText("On cooldown!", Color.White);
                // Skill is on cooldown
                return;
            }

            switch (skill)
            {
                

                    case QuirkSkills.DangerActivate:
                    DangerActivate(mainPlayer);
                    SetCooldown(skill, 120);
                    break;
            }
        }
        private void SetCooldown(QuirkSkills skill, int timeInTicks)
        {
            if (SkillCooldowns.ContainsKey(skill))
            {
                SkillCooldowns[skill] = timeInTicks;
            }
            else
            {
                SkillCooldowns.Add(skill, timeInTicks);
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
            IsDangerSenseActive = !IsDangerSenseActive;
            
            if (IsDangerSenseActive)
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
