using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

using MyHeroMod.content.Quirks.OFA8th.Projectiles.DetroitSmash;

using MyHeroMod.content.System;
using System.Collections.Generic;
using MyHeroMod.content.Quirks.OFA8th.Projectiles.CaliforniaSmash;
using MyHeroMod.content.Quirks.OFA8th.Projectiles.TexasSmash;
using MyHeroMod.content.Quirks.OFA8th.Projectiles.CarolinaSmash;
using Microsoft.Build.Framework;

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

                case QuirkSkills.PrimeDetroitSmash:
                    DoDetroitSmash(mainPlayer);
                    break;
                case QuirkSkills.PrimeTexasSmash:
                    DoTexasSmash(mainPlayer);
                break;
                case QuirkSkills.PrimeCaliforniaSmash:
                    DoCaliforniaSmash(mainPlayer);
                    break;
                case QuirkSkills.PrimeCarolinaSmash:
                    DoCarolinaSmash(mainPlayer);
                    break;
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

        private void DoDetroitSmash(TransformationPlayer mainPlayer)
        {
            if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                CombatText.NewText(Player.getRect(), Color.Yellow, "Detroit Smash!");
            }
            else
            {
                CombatText.NewText(Player.getRect(), Color.White, "Super Punch!");
            }
            
             Vector2 Velocity = Main.MouseWorld - Player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                Velocity,
                ModContent.ProjectileType<DetroitPunchProj>(),
                500, 
                2f, 
                Player.whoAmI);

                
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2"), Player.position);

            
        }

        private void DoCaliforniaSmash(TransformationPlayer mainPlayer)
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<PrimeCaliforniaSmashController>()] > 0)
                return;

                if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                CombatText.NewText(Player.getRect(), Color.Yellow, "California Smash!");
            }
            else
            {
                CombatText.NewText(Player.getRect(), Color.White, "Roll Punch");
            }

            // Spawna o projétil que vai controlar o player
            // A velocidade inicial não importa aqui, pois a AI[0] controla a subida
            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<PrimeCaliforniaSmashController>(),
                80, // Dano alto (Impacto)
                10f, // Knockback alto
                Player.whoAmI
            );
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2"), Player.position);
            
        }

        private void DoTexasSmash(TransformationPlayer mainPlayer)
        {
            
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<PrimeTexasSmashProj>()] > 0)
                return;

                if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                CombatText.NewText(Player.getRect(), Color.Yellow, "Texas Smash!");
            }
            else
            {
                CombatText.NewText(Player.getRect(), Color.White, "Air Pressure!");
            }

            Vector2 Velocity = Main.MouseWorld - Player.Center;
            Velocity.Normalize();
            Velocity *= 30f;

            // Spawna o projétil que vai controlar o player
            // A velocidade inicial não importa aqui, pois a AI[0] controla a subida
            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                Velocity, 
                ModContent.ProjectileType<PrimeTexasSmashProj>(),
                10, // Dano alto (Impacto)
                30f, // Knockback alto
                Player.whoAmI
            
        );
        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1"), Player.position);
            
            
        }

        private void DoCarolinaSmash(TransformationPlayer mainPlayer)
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<PrimeCarolinaSmashController>()] > 0)
                return;

                if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                CombatText.NewText(Player.getRect(), Color.Yellow, "Carolina Smash!");
            }
            else
            {
                CombatText.NewText(Player.getRect(), Color.White, "Dash Slash!");
            }

            // Spawna o projétil que vai controlar o player
            // A velocidade inicial não importa aqui, pois a AI[0] controla a subida
            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<PrimeCarolinaSmashController>(),
                80, // Dano alto (Impacto)
                10f, // Knockback alto
                Player.whoAmI
                
            );
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1"), Player.position);
            
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
                CombatText.NewText(Player.getRect(), Color.Yellow, "Watashi Ga Kita!");
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/watashigakita"), Player.position);

                
                
                // Efeito de fumaça na transformação
                for(int i=0; i<30; i++) Dust.NewDust(Player.position, Player.width, Player.height, DustID.Smoke, 0,0, 100, default, 2f);
            }
        }
        }
    }
