using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.GeneralSkills1;
using MyHeroMod.content.System.BasePlayer; // Certifique-se que o namespace do GeneralSkills está certo

namespace MyHeroMod.content.Quirks.Float
{
    public partial class FloatPlayer : BasePlayer
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