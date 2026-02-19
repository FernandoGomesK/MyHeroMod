using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Buffs;
using Terraria.Audio;
using Terraria.DataStructures;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer : ModPlayer
    {
        

    }

        

        // Transformar 

        // private void ToggleForm(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        // {
        //     if (mainPlayer.ActiveForm == targetForm)
        //     {
        //         mainPlayer.ActiveForm = QuirkSkills.None;
        //         Main.NewText("Reverted to normal form.", Color.White);
        //     }
        //     else
        //     {
        //         if (targetForm == QuirkSkills.OneForAllFullCowling5 && mainPlayer.CurrentStage < QuirkStage.Adequation)
        //         {
        //             Main.NewText("You don't quite get how to use Full Cowling yet.", Color.Red);
        //             return;
        //         }
        //         if (targetForm == QuirkSkills.OneForAllFullCowling8 && mainPlayer.CurrentStage < QuirkStage.Intermediate)
        //         {
        //             Main.NewText("You haven't mastered Full Cowling 8% yet.", Color.Red);
        //             return;
        //         }
        //         PendingForm = targetForm;
        //         ActivationTimer = 1;
        //         Main.NewText($"Transformed into {SkillLibrary.SkillList[targetForm].Name} form!", Color.LimeGreen);
        //         CombatText.NewText(Player.getRect(), Color.Green, SkillLibrary.SkillList[targetForm].Name);
        //         SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), Player.position);
        //     }
        // }
        // private void ToggleFloat(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        // {

        //     isFloatActive = !isFloatActive;

        //     if (isFloatActive)
        //     {
        //         Main.NewText("One For All 7th: Float Activated!", Color.LimeGreen);
        //         CombatText.NewText(Player.getRect(), Color.DeepPink, "One For All 7h: Float");
        //     }
        //     else
        //     {
        //         Main.NewText("Float Deactivated!", Color.White);
        //     } 
        // }

        // private void ToggleGearshift(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        // {

            

        //     if (isGearshiftActive)
        //     {
        //         isGearshiftActive = false;
        //         Main.NewText("Gearshift Deactivated!", Color.White);
        //         SetCooldown(QuirkSkills.Gearshift, 6000);
                
        //         return;
        //     }
            
        //         ActivationTimer = 1;
        //         GearActivation = true;

        //         GearshiftTimer = 0;
        //         GearshiftTimer++;
            

        // }

        // private void ToggleDangerSense(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        // {

        //     isDangerSenseActive = !isDangerSenseActive;

        //     if (isDangerSenseActive)
        //     {
        //         Main.NewText("One For All 4th: Danger Sense", Color.LimeGreen);
        //         CombatText.NewText(Player.getRect(), Color.Yellow, "One For All 4th: Danger Sense");
        //     }
        //     else
        //     {
        //         Main.NewText("Danger Sense Deactivated!", Color.White);
                
        //     }
        // }

        // private void ToggleSmokesScreen(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        // {
        //     isSmokeScreenActive = !isSmokeScreenActive;

        //     if (isSmokeScreenActive)
        //     {
        //         Main.NewText("One For All 6th: Smoke Screen ", Color.LimeGreen);
        //         CombatText.NewText(Player.getRect(), Color.Purple, "One For All 6th: Full Blast!");
        //     }
        //     else
        //     {
        //         Main.NewText("Smoke Screen Deactivated!", Color.White);
        //     } 

        //     // Implement smoke screen logic here
        // }

        // private void StoreFaJin(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        // {
        //     FaJinCharges++;
        //     SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FaJinStoringSound"), Player.position);
        //     if (FaJinCharges >= MaxFaJinCharges)
        //     {
        //         FaJinCharges = MaxFaJinCharges;
        //         Main.NewText("Fa Jin storage is full!", Color.Red);
        //         SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FaJinSound"), Player.position);
        //         return;
        //     }
        //     else
        //     {
        //         Main.NewText($"Stored Fa Jin energy! Current charges: {FaJinCharges}", Color.LimeGreen);
        //         CombatText.NewText(Player.getRect(), Color.Orange, $"Fa Jin Charges: {FaJinCharges}");
        //     }

            // Implement Fa Jin store logic here
        }
        //Detroit Smash

       
        
        // Super Pulo
        
        // private void DoSuperJump(TransformationPlayer mainPlayer)
        // {
        //     bool isDangerous = (mainPlayer.CurrentStage == QuirkStage.Initial) || (mainPlayer.ActiveForm != QuirkSkills.None);

        //     if (isDangerous)
        //     {
        //         Player.velocity.Y = -15f;
        //         int damageTaken = 25;
        //         Player.statLife -= damageTaken;

        //         if (Player.statLife <= 0)
        //         {
        //             var reason = PlayerDeathReason.ByCustomReason(
        //                 Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));
        //             Player.KillMe(reason, damageTaken, 0);
        //         }
        //         CombatText.NewText(Player.getRect(), Color.Red, "Leg Broken!");

        //         for (int i = 0; i < 15; i++)
        //         {
        //             Dust.NewDust(Player.position, Player.width, Player.height, DustID.Cloud, 0, 5, 100, default, 1.5f);
        //         }   
        //     }
        //     else
        //     {
        //         Main.NewText("Cannot use Super Jump in current state.", Color.Red);
        //     }
        // }

        // Delaware Smash

        

        // private void DoBlackWhipHook(TransformationPlayer mainPlayer)


        // {
        //     if (Player.ownedProjectileCounts[ModContent.ProjectileType<BlackWhipProjectile>()] >= 2) 
        //     {
        //     return; 
        //     }
        //     CombatText.NewText(Player.getRect(), Color.Orange, "One For All 5th: BlackWhip");
        //     Vector2 velocity = Main.MouseWorld - Player.Center;
        //     velocity.Normalize();
        //     velocity *= 18f; // Velocidade do tiro (deve bater com a do projétil)

        //     // Cria o Gancho
        //     // Ganchos nascem no Player.Center para a corrente ficar conectada visualmente
        //     Projectile.NewProjectile(
        //         Player.GetSource_FromThis(), 
        //         Player.Center, 
        //         velocity, 
        //         ModContent.ProjectileType<BlackWhipProjectile>(), 
        //         0,  // Dano (0 se for só mobilidade)
        //         0f, // Knockback
        //         Player.whoAmI
                
        //     );  
        // }
    
    