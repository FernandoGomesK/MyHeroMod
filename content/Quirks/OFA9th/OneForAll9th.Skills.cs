using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.BlackWhip;
using MyHeroMod.content.Quirks.OFA9th.Buffs;
using Terraria.Audio;
using Terraria.DataStructures;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer : ModPlayer
    {
        
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
            if (ActivationTimer > 0)
            
                
                return;
            
        
            switch (skill)
            {
                case QuirkSkills.SuperJump:
                    DoSuperJump(mainPlayer);
                    
                    SetCooldown(skill, 120);
                    break;
                case QuirkSkills.DelawareSmash:
                    DoDelawareSmash(mainPlayer);
                    
                    SetCooldown(skill, 30);
                    break;
                case QuirkSkills.DetroitSmash:
                    DoDetroitSmash(mainPlayer);
                    
                    SetCooldown(skill, 450);
                    break;
                case QuirkSkills.OneForAllFullCowling5:
                    ToggleForm(mainPlayer, QuirkSkills.OneForAllFullCowling5);
                    
                    SetCooldown(skill, 60);
                    break;
                case QuirkSkills.OneForAllFullCowling8:
                    ToggleForm(mainPlayer, QuirkSkills.OneForAllFullCowling8);
                    
                    SetCooldown(skill, 60);
                    break;
                case QuirkSkills.OneForAllFullCowling45:
                    ToggleForm(mainPlayer, QuirkSkills.OneForAllFullCowling45);
                    
                    SetCooldown(skill, 60);
                    break;
                case QuirkSkills.BlackWhipHook:
                    DoBlackWhipHook(mainPlayer);
                    
                    SetCooldown(skill, 60);
                    break;
                case QuirkSkills.Float:
                    ToggleFloat(mainPlayer, QuirkSkills.Float);
                    
                    SetCooldown(skill, 60);
                    break;
                case QuirkSkills.Gearshift:
                    ToggleGearshift(mainPlayer, QuirkSkills.Gearshift);
                    
                    SetCooldown(skill, 30);
                    break;
                case QuirkSkills.DangerSense:
                    ToggleDangerSense(mainPlayer, QuirkSkills.DangerSense);
                    
                    SetCooldown(skill, 60);
                    break;
                case QuirkSkills.SmokeScreen:
                    ToggleSmokesScreen(mainPlayer, QuirkSkills.SmokeScreen);

                    SetCooldown(skill, 60);
                    break;
                case QuirkSkills.FaJinStore:
                    StoreFaJin(mainPlayer, QuirkSkills.FaJinStore);
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
                PendingForm = targetForm;
                ActivationTimer = 1;
                Main.NewText($"Transformed into {SkillData.SkillList[targetForm].Name} form!", Color.LimeGreen);
                CombatText.NewText(Player.getRect(), Color.Green, SkillData.SkillList[targetForm].Name);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), Player.position);
            }
        }
        private void ToggleFloat(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        {

            isFloatActive = !isFloatActive;

            if (isFloatActive)
            {
                Main.NewText("One For All 7th: Float Activated!", Color.LimeGreen);
                CombatText.NewText(Player.getRect(), Color.DeepPink, "One For All 7h: Float");
            }
            else
            {
                Main.NewText("Float Deactivated!", Color.White);
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

        private void ToggleDangerSense(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        {

            isDangerSenseActive = !isDangerSenseActive;

            if (isDangerSenseActive)
            {
                Main.NewText("One For All 4th: Danger Sense", Color.LimeGreen);
                CombatText.NewText(Player.getRect(), Color.Yellow, "One For All 4th: Danger Sense");
            }
            else
            {
                Main.NewText("Danger Sense Deactivated!", Color.White);
                
            }
        }

        private void ToggleSmokesScreen(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        {
            isSmokeScreenActive = !isSmokeScreenActive;

            if (isSmokeScreenActive)
            {
                Main.NewText("One For All 6th: Smoke Screen ", Color.LimeGreen);
                CombatText.NewText(Player.getRect(), Color.Purple, "One For All 6th: Full Blast!");
            }
            else
            {
                Main.NewText("Smoke Screen Deactivated!", Color.White);
            } 

            // Implement smoke screen logic here
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
        //Detroit Smash

        private void DoDetroitSmash(TransformationPlayer mainPlayer)
        {
            int MaxDamage = 450;
            float DamageMultiplier = 1f;
            bool hurtPlayer = false;
            bool usedFaJin = false;

            switch (mainPlayer.ActiveForm)
            {
                case QuirkSkills.OneForAllFullCowling5:
                    DamageMultiplier = 0.05f;
                    hurtPlayer = false;
                    break;
                case QuirkSkills.OneForAllFullCowling8:
                    DamageMultiplier = 0.08f;
                    break;
                case QuirkSkills.OneForAllFullCowling45:
                    DamageMultiplier = 0.45f;
                    break;
                default:
                    DamageMultiplier = 1f;
                    hurtPlayer = true;
                    break;
            }
            if (FaJinStored)
            {
                DamageMultiplier += 0.55f; // Increase damage by 25% if Fa Jin is stored
                FaJinCharges = 0; // Consume all Fa Jin charges
                FaJinStored = false;
                Player.ClearBuff(ModContent.BuffType<FaJinBuff>());
                usedFaJin = true;
            }

            int FinalDamage = (int)(MaxDamage * DamageMultiplier);

            string attackName = "";

            
            if (usedFaJin)
            {
                attackName += "Faux ";
            }
            if (usedFaJin || !hurtPlayer)
            {
                attackName += (DamageMultiplier * 100).ToString("0") + "% Detroit Smash";
            }
            else
            {
                attackName += "Detroit Smash";
            }
            if (isGearshiftActive)
            {
                attackName += ": Quintuple";
            }
            else if (!usedFaJin || hurtPlayer)
            {
                attackName += "!";
            }
            
            CombatText.NewText(Player.getRect(), Color.LimeGreen, attackName);

            

            
            Vector2 Direction = Main.MouseWorld - Player.Center;
            Direction.Normalize();
            Vector2 Velocity = Direction * 15f;

            Vector2 BaseSpawnLocation = Player.Center + (Direction * 90f);

            
            
            int numberOfPunches = isGearshiftActive ? 5 : 1; // 5 hits if Gearshift is active, else 1

            for (int i = 0; i < numberOfPunches; i++)
            {
                Vector2 spacing = Direction * (25f * i);
                Vector2 currentSpawn = BaseSpawnLocation - spacing;
                
    
                Projectile.NewProjectile(
                    Player.GetSource_FromThis(), 
                    currentSpawn, 
                    Velocity, // Use the new speed with spread
                    ModContent.ProjectileType<DetroitSmashProj>(), 
                    FinalDamage, 
                    2f, 
                    Player.whoAmI
                );
                Projectile.NewProjectile(
                Player.GetSource_FromThis(), 
                BaseSpawnLocation, 
                Velocity, // Use the new speed with spread
                ModContent.ProjectileType<PunchAttackProj>(), 
                0,
                0f, 
                Player.whoAmI
            );
 
            
            }
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
            else if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling45)
            {
                FinalDamage = (int)(MaxDamage * 0.45f);
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

            if (isFloatActive)
            {
                float recoil = 2f;

                Player.velocity = -Velocity * recoil;

                for (int i = 0; i < 10; i++)
        {
            Dust.NewDust(Player.position, Player.width, Player.height, DustID.Cloud, Velocity.X * 2, Velocity.Y * 2, 0, default, 1f);
        }
            }

            

            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Velocity, ModContent.ProjectileType<DelawareSmashProj>(), FinalDamage, 2f, Player.whoAmI);

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

        private void DoBlackWhipHook(TransformationPlayer mainPlayer)


        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<BlackWhipProjectile>()] >= 2) 
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
                ModContent.ProjectileType<BlackWhipProjectile>(), 
                0,  // Dano (0 se for só mobilidade)
                0f, // Knockback
                Player.whoAmI
                
            );
        }
    }
    }