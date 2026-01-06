using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using Terraria.ID;
using MyHeroMod.content.System;
using Terraria.Audio;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.HellFlames.Projectiles;
using System.Security.Cryptography.Pkcs;

namespace MyHeroMod.content.Quirks.HellFlames
{
    public partial class HellFlamesPlayer : ModPlayer
    {
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (MainPlayer.SelectedQuirk == QuirkType.HellFlames) 
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
                    case QuirkSkills.FlashFireFist:
                    ActivateFlashFireFist(mainPlayer);

                    SetCooldown(skill, 60);
                    break;
                    case QuirkSkills.ProminenceBurn:
                    DoProminenceBurn();
                    break;
                    
                
                //aadsada

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
        private void ActivateFlashFireFist(TransformationPlayer mainPlayer)
        {
            if (IsFlashFireFistActive)
            {
                IsFlashFireFistActive = false;
                Player.ClearBuff(ModContent.BuffType<Buffs.FlashFireFistBuff>());
                Main.NewText("Flash Fire Fist Deactivated", Color.OrangeRed);   
                SetCooldown(QuirkSkills.FlashFireFist, 120);
                return;
                
            }
            CurrentHeat += 20;
            IsFlashFireFistActive = true;

            // Implementation for Flash Fire Fist
        }
        private void DoProminenceBurn()
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.ProminenceBurnProj>()] > 0)
                return;

            Main.NewText("PROMINENCE BURN!", Color.OrangeRed);
            SoundEngine.PlaySound(SoundID.Item117, Player.position); // Som de laser/fogo forte

            // Calcula a direção do mouse
            Vector2 direction = Main.MouseWorld - Player.Center;
            direction.Normalize();

            // Lança o projétil que SERÁ o laser
            // Dano: 100 (ajuste conforme necessário)
            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                direction, // A velocidade define a direção inicial
                ModContent.ProjectileType<Projectiles.ProminenceBurnProj>(),
                100, 
                5f, 
                Player.whoAmI);
            
            
        }
        

        

    }
}