using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using Terraria.ID;
using MyHeroMod.content.System;
using Terraria.Audio;
using Terraria.DataStructures;
using MyHeroMod.content.Buffs;



namespace MyHeroMod.content.Quirks.FaJin
{
    public partial class FaJinPlayer : ModPlayer
    {
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (MainPlayer.SelectedQuirk == QuirkType.FaJin) 
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
                
                    case QuirkSkills.FaJinStore:
                    StoreFaJin(mainPlayer, QuirkSkills.FaJinStore);
                    SetCooldown(skill, 60);
                    break;
                    // case QuirkSkills.DangerActivate:
                    // DangerActivate(mainPlayer);
                    // SetCooldown(skill, 120);
                    // break;
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


        private void StoreFaJin(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        {
            FaJinCharges++;
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FaJinStoringSound"), Player.position);
            if (FaJinCharges >= MaxFaJinCharges)
            {
                FaJinCharges = MaxFaJinCharges;
                Main.NewText("Fa Jin storage is full!", Color.Red);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FaJinSound"), Player.position);
                return;
            }
            else
            {
                Main.NewText($"Stored Fa Jin energy! Current charges: {FaJinCharges}", Color.LimeGreen);
                CombatText.NewText(Player.getRect(), Color.Orange, $"Fa Jin Charges: {FaJinCharges}");
            }

            // Implement Fa Jin store logic here
        }

        
            
            

        

             
            
        }

        
        
}
