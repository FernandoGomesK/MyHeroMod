using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.GeneralSkills1; 
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhip;

namespace MyHeroMod.content.Quirks.BlackWhip
{
    public partial class BlackWhipPlayer : ModPlayer
    {
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Verifica se tem a Quirk certa (Float ou One For All)
            if (MainPlayer.SelectedQuirk == QuirkType.BlackWhip || MainPlayer.SelectedQuirk == QuirkType.OneForAll9th) 
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
                case QuirkSkills.BlackWhipHook:
                    DoBlackWhipHook(mainPlayer);
                    
                    SetCooldown(skill, 60);
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

        private void DoBlackWhipHook(TransformationPlayer mainPlayer)


        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<BlackWhipProjectileUnique>()] >= 2) 
            {
            return; 
            }
            CombatText.NewText(Player.getRect(), Color.Orange, "One For All 5th: BlackWhip");
            Vector2 velocity = Main.MouseWorld - Player.Center;
            velocity.Normalize();
            velocity *= 18f; // Velocidade do tiro (deve bater com a do projétil)

            // Cria o Gancho
            // Ganchos nascem no Player.Center para a corrente ficar conectada visualmente
            Projectile.NewProjectile(
                Player.GetSource_FromThis(), 
                Player.Center, 
                velocity, 
                ModContent.ProjectileType<BlackWhipProjectileUnique>(), 
                0,  // Dano (0 se for só mobilidade)
                0f, // Knockback
                Player.whoAmI
                
            );      
        }
    }
}