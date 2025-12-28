using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.System;
using Terraria.Audio;
using System.Collections.Generic;


namespace MyHeroMod.content.Quirks.OFA9th
{
    public class OneForAll9thPlayer : ModPlayer
    {

        public bool isFullCowlingBuffActive = false;

        public int Fingers = 10;
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();
        private int ElectricSoundTimer = 0;

        public int ActivationTimer = 0;
        public int ActivationMaxTime = 40;
        private QuirkSkills PendingForm = QuirkSkills.None;

        // Resetar no renascer

        public override void OnRespawn()
        {
            Fingers = 10;
            Player.GetModPlayer<TransformationPlayer>().ActiveForm = QuirkSkills.None;
            SkillCooldowns.Clear();
            ElectricSoundTimer = 0;
            ActivationTimer = 0;
            PendingForm = QuirkSkills.None;
        }

        public override void PreUpdate()
        {
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0)
                {
                    SkillCooldowns[skill]--;
                }
            }
            if (ActivationTimer > 0)
            {
                ActivationTimer++;
                Player.velocity *= 0.6f;

                if (ActivationTimer >= ActivationMaxTime)
                {
                    var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
                    mainPlayer.ActiveForm = PendingForm;

                    ActivationTimer = 0;
                    PendingForm = QuirkSkills.None;


                }
            }
        }



        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th)
            {
                if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot1);
                if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot2);
                if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot3);
                if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.TransformSlot);
            }
        }

        // Realizar Habilidades

        private void ExecuteSkill(TransformationPlayer mainPlayer, QuirkSkills skill)
        {
            if (SkillCooldowns.ContainsKey(skill) && SkillCooldowns[skill] > 0)
            {
                Main.NewText("On Cooldown.", Color.White);
                return;
            }

            bool skillUsed = false;
            switch (skill)
            {
                case QuirkSkills.SuperJump:
                    DoSuperJump(mainPlayer);
                    skillUsed = true;
                    SetCooldown(skill, 120);
                    break;
                case QuirkSkills.DelawareSmash:
                    DoDelawareSmash(mainPlayer);
                    skillUsed = true;
                    SetCooldown(skill, 30);
                    break;
                case QuirkSkills.DetroitSmash:
                    DoDetroitSmash(mainPlayer);
                    skillUsed = true;
                    SetCooldown(skill, 600);
                    break;
                case QuirkSkills.OneForAllFullCowling5:
                    ToggleForm(mainPlayer, QuirkSkills.OneForAllFullCowling5);
                    skillUsed = true;
                    SetCooldown(skill, 60);
                    break;
                case QuirkSkills.OneForAllFullCowling8:
                    ToggleForm(mainPlayer, QuirkSkills.OneForAllFullCowling8);
                    skillUsed = true;
                    SetCooldown(skill, 60);
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

        // Transformar 

        private void ToggleForm(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        {
            if (mainPlayer.ActiveForm == targetForm)
            {
                mainPlayer.ActiveForm = QuirkSkills.None;
                Main.NewText("Reverted to normal form.", Color.White);
            }
            else
            {
                if (targetForm == QuirkSkills.OneForAllFullCowling5 && mainPlayer.CurrentStage < QuirkStage.Adequation)
                {
                    Main.NewText("You don't quite get how to use Full Cowling yet.", Color.Red);
                    return;
                }
                if (targetForm == QuirkSkills.OneForAllFullCowling8 && mainPlayer.CurrentStage < QuirkStage.Intermediate)
                {
                    Main.NewText("You haven't mastered Full Cowling 8% yet.", Color.Red);
                    return;
                }
                mainPlayer.ActiveForm = targetForm;
                Main.NewText($"Transformed into {SkillData.SkillList[targetForm].Name} form!", Color.LimeGreen);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), Player.position);
            }
        }

        //Detroit Smash

        private void DoDetroitSmash(TransformationPlayer mainPlayer)
        {
            int MaxDamage = 450;
            int FinalDamage = 0;
            bool hurtPlayer = false;

            if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling5)
            {
                FinalDamage = (int)(MaxDamage * 0.05f);
                hurtPlayer = false;
            }
            if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling8)
            {
                FinalDamage = (int)(MaxDamage * 0.08f);
            }
            else
            {
                FinalDamage = MaxDamage;
                hurtPlayer = true;
            }

            Vector2 Velocity = Main.MouseWorld - Player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Velocity, ModContent.ProjectileType<DetroitSmashProj>(), FinalDamage, 2f, Player.whoAmI);

            if (hurtPlayer)
            {
                Player.statLife -= 10;
                if (Player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));
                    Player.KillMe(reason, FinalDamage, 0);
                }
            }
        }
        
        // Super Pulo
        
        private void DoSuperJump(TransformationPlayer mainPlayer)
        {
            bool isDangerous = (mainPlayer.CurrentStage == QuirkStage.Initial) || (mainPlayer.ActiveForm != QuirkSkills.None);

            if (isDangerous)
            {
                Player.velocity.Y = -15f;
                int damageTaken = 25;
                Player.statLife -= damageTaken;

                if (Player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));
                    Player.KillMe(reason, damageTaken, 0);
                }
                CombatText.NewText(Player.getRect(), Color.Red, "Leg Broken!");

                for (int i = 0; i < 15; i++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.Cloud, 0, 5, 100, default, 1.5f);
                }   
            }
            else
            {
                Main.NewText("Cannot use Super Jump in current state.", Color.Red);
            }
        }

        // Delaware Smash

        private void DoDelawareSmash(TransformationPlayer mainPlayer)
        {
            int MaxDamage = 100;
            int FinalDamage = 0;
            bool consumeFinger = false;
            bool hurtPlayer = false;

            if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling5)
            {
                FinalDamage = (int)(MaxDamage * 0.05f);
                hurtPlayer = false;
                consumeFinger = false;
            }
            else if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling8)
            {
                FinalDamage = (int)(MaxDamage * 0.08f);
                hurtPlayer = false;
                consumeFinger = false;
            }
            else
            {
                FinalDamage = MaxDamage;
                hurtPlayer = true;
                consumeFinger = true;
            }
            if (consumeFinger && Fingers <= 0)
            {
                CombatText.NewText(Player.getRect(), Color.Red, "No fingers left!");
                return;
            }

            if (consumeFinger) Fingers--;

            Vector2 Velocity = Main.MouseWorld - Player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Velocity, ModContent.ProjectileType<DelawareSmashProj>(), FinalDamage, 2f, Player.whoAmI);

            if (hurtPlayer)
            {
                Player.statLife -= 10;
                if (Player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));
                    Player.KillMe(reason, MaxDamage], 0);
                }
            }
        }

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && mainPlayer.ActiveForm != QuirkSkills.None)
            {
                Player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 2);


                 Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
            }
            else
            {
                isFullCowlingBuffActive = false;
            }
        }

    }}
