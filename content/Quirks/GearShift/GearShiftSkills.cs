using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.System;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.Gearshift
{
    // PARTE 2: INPUTS E SKILLS
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
                return;
            }

            switch (skill)
            {
                case QuirkSkills.Gearshift:
                    ToggleGearshift(mainPlayer);
                    SetCooldown(skill, 30);
                    break;
            }
        }

        private void SetCooldown(QuirkSkills skill, int timeInTicks)
        {
            if (SkillCooldowns.ContainsKey(skill)) SkillCooldowns[skill] = timeInTicks;
            else SkillCooldowns.Add(skill, timeInTicks);
        }

        private void ToggleGearshift(TransformationPlayer mainPlayer)
        {
            // 1. DESLIGA se tiver o Buff ativo
            if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
            {
                Player.ClearBuff(ModContent.BuffType<GearshiftBuff>());
                
                Main.NewText("Gearshift Deactivated!", Color.White);
                SetCooldown(QuirkSkills.Gearshift, 600); // 10s cooldown
                
                GearActivation = false;
                ActivationTimer = 0;
                return;
            }
            // 2. CANCELA se estiver carregando
            else if (GearActivation)
            {
                GearActivation = false;
                ActivationTimer = 0;
                Main.NewText("Cancelled.", Color.Gray);
            }
            // 3. LIGA (Começa a carregar)
            else
            {
                ActivationTimer = 0;
                GearActivation = true;
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/GearShiftSound") with { Volume = 0.20f }, Player.position);
            }
        }
    }
}