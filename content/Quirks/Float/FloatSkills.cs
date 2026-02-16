using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.GeneralSkills1; // Certifique-se que o namespace do GeneralSkills está certo

namespace MyHeroMod.content.Quirks.Float
{
    public partial class FloatPlayer : ModPlayer
    {
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Verifica se tem a Quirk certa (Float ou One For All)
            if (MainPlayer.SelectedQuirk == QuirkType.Float || MainPlayer.SelectedQuirk == QuirkType.OneForAll9th) 
            {
                if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot1);
                if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot2);
                if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot3);
                if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.TransformSlot);
            }      
        }

        private void ExecuteSkill(TransformationPlayer mainPlayer, QuirkSkills skill)
        {
            var generalSkills = Player.GetModPlayer<GeneralSkills1.GeneralSkills>(); // Caminho completo para evitar confusão

            if (SkillCooldowns.ContainsKey(skill) && SkillCooldowns[skill] > 0)
            {
                Main.NewText("On cooldown!", Color.White);
                return;
            }

            switch (skill)
            {
                case QuirkSkills.Float:
                    ToggleFloat();
                    SetCooldown(skill, 30); // Pequeno cooldown para não spammar
                    break;

                case QuirkSkills.Dash:
                    
                    generalSkills.Dash();
                    SetCooldown(skill, 60);
                    break;
            }
        }

        private void SetCooldown(QuirkSkills skill, int timeInTicks)
        {
            if (SkillCooldowns.ContainsKey(skill)) SkillCooldowns[skill] = timeInTicks;
            else SkillCooldowns.Add(skill, timeInTicks);
        }

        private void ToggleFloat()
        {
            // Se já tem o buff -> Remove
            if (Player.HasBuff(ModContent.BuffType<FloatBuff>()))
            {
                Player.ClearBuff(ModContent.BuffType<FloatBuff>());
                CombatText.NewText(Player.getRect(), Color.Cyan, "Float OFF");
            }
            // Se não tem -> Adiciona (Tempo infinito/longo)
            else
            {
                Player.AddBuff(ModContent.BuffType<FloatBuff>(), 3600); // 1 minuto (o buff deve ser infinito se tiver Main.buffNoTimeDisplay)
                CombatText.NewText(Player.getRect(), Color.Cyan, "Float ON");
            }
        }
    }
}