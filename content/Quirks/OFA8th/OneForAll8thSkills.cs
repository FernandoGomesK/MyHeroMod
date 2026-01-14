using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

using MyHeroMod.content.Quirks.OFA8th.Projectiles;
using MyHeroMod.content.System;
using System.Collections.Generic;

namespace MyHeroMod.content.Quirks.OFA8th
{
    public partial class OneForAll8thPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll8th)
            {
                if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot1);
                if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot2);
                if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot3);
                if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.TransformSlot);
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

                // case QuirkSkills.DetroitSmash:
                //     DoDetroitSmash(mainPlayer);
                //     break;
                // case QuirkSkills.DelawareSmash:
                //     DoDelawareSmash(mainPlayer);
                //     break;
                case QuirkSkills.StockPile:
                    ToggleForm(mainPlayer, QuirkSkills.StockPile);
                    break;
                case QuirkSkills.StockPileMaximum:
                    ToggleForm(mainPlayer, QuirkSkills.StockPileMaximum);
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


            
            private void ToggleForm(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        {
            if (mainPlayer.ActiveForm == targetForm)
            {
                mainPlayer.ActiveForm = QuirkSkills.None;
                Main.NewText("Reverted to normal form.", Color.White);
            }
            else
            {
                if (targetForm == QuirkSkills.StockPile && mainPlayer.CurrentStage < QuirkStage.Intermediate)
                {
                    Main.NewText("You don't quite get how to use all of your power yet.", Color.Red);
                    return;
                }
                if (targetForm == QuirkSkills.StockPileMaximum && mainPlayer.CurrentStage < QuirkStage.Advanced)
                {
                    Main.NewText("You haven't mastered all of your power yet.", Color.Red);
                    return;
                }
                mainPlayer.ActiveForm = targetForm;
                Main.NewText($"Transformed into {SkillData.SkillList[targetForm].Name} form!", Color.Yellow);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), Player.position);

                
                
                // Efeito de fumaça na transformação
                for(int i=0; i<30; i++) Dust.NewDust(Player.position, Player.width, Player.height, DustID.Smoke, 0,0, 100, default, 2f);
            }
        }
        }
    }
