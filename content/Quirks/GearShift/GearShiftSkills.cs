using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using Terraria.ID;
using MyHeroMod.content.System;
using Terraria.Audio;
using Terraria.DataStructures;
using MyHeroMod.content.Buffs;



namespace MyHeroMod.content.Quirks.Gearshift
{
    public partial class GearshiftPlayer : ModPlayer
    {
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (MainPlayer.SelectedQuirk == QuirkType.Gearshift) 
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
                

                    case QuirkSkills.Gearshift:
                    ToggleGearshift(mainPlayer, QuirkSkills.Gearshift);
                    
                    SetCooldown(skill, 30);
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

       private void ToggleGearshift(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        {

            

            if (isGearshiftActive)
            {
                isGearshiftActive = false;
                Main.NewText("Gearshift Deactivated!", Color.White);
                SetCooldown(QuirkSkills.Gearshift, 6000);
                
                return;
            }
            
                ActivationTimer = 1;
                GearActivation = true;

                GearshiftTimer = 0;
                GearshiftTimer++;
            

        }
    }}
        
        

