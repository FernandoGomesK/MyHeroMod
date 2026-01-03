using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.BlackWhip;
using MyHeroMod.content.System;
using Terraria.Audio;
using System.Collections.Generic;
using System.Runtime.Serialization;



namespace MyHeroMod.content.Quirks.OFA9th
{
    public class OneForAll9thPlayer : ModPlayer
    {
        public bool isGearshiftActive = false;
        public bool isGearshiftBuffActive = false;

        public int GearshiftTimer = 0;

        public int GearshiftMaxTime = 6000;

        public bool isFullCowlingBuffActive = false;

        public bool isDangerSenseActive = false;

        public bool isFloatActive = false;

        public int Fingers = 10;

        public int ParallelProcessing = 0;
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
            GearshiftTimer = 0;
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
            if (isGearshiftActive)
            {
                GearshiftTimer++;
                if (GearshiftTimer >= GearshiftMaxTime)
                {
                    isGearshiftActive = false;
                    isGearshiftBuffActive = false;
                    Main.NewText("Gearshift Deactivated due to limit!", Color.White);
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
                    
                    SetCooldown(skill, 6000);
                    break;
                case QuirkSkills.DangerSense:
                    ToggleDangerSense(mainPlayer, QuirkSkills.DangerSense);
                    
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

            isGearshiftActive = !isGearshiftActive;

            if (isGearshiftActive)
            {
                Main.NewText("One For All 2nd: Gearshift", Color.LimeGreen);
                CombatText.NewText(Player.getRect(), Color.Blue, "One For All 2nd: Gearshift");
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/GearShiftSound"), Player.position);
                GearshiftTimer = 0;
                GearshiftTimer++;
                
            }
            else
            {
                Main.NewText("Gearshift Deactivated!", Color.White);
            }

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
            else if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling8)
            {
                FinalDamage = (int)(MaxDamage * 0.08f);
            }
            else if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling45)
            {
                FinalDamage = (int)(MaxDamage * 0.45f);
            }
            else
            {
                FinalDamage = MaxDamage;
                hurtPlayer = true;
            }

            
            Vector2 Direction = Main.MouseWorld - Player.Center;
            Direction.Normalize();

    // 2. Velocidade
            Vector2 Velocity = Direction * 15f;

    // 3. Posição de Nascimento (Offset)
    // 90 pixels na frente do "umbigo" do player.
    // Como agora o projétil desenha pelo centro, isso vai ficar perfeito.
            Vector2 SpawnLocation = Player.Center + (Direction * 90f);

    // Criação do Projétil
            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                SpawnLocation, 
                Velocity, 
                ModContent.ProjectileType<DetroitSmashProj>(), 
                FinalDamage, 
                2f, 
                Player.whoAmI
            );

            

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

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (isDangerSenseActive)
            {
                Player.AddBuff(ModContent.BuffType<DangerSenseBuff>(), 2);
            }

            if (isFloatActive && !Player.mount.Active && Player.velocity.Y != 0)
            {
        // If holding JUMP, stop falling (Hover)
        if (Player.controlJump) 
        {
            Player.velocity.Y = 0f; 
            Player.fallStart = (int)(Player.position.Y / 16f); // Prevents fall damage accumulating
        }
        // If NOT holding jump, fall very slowly (feather fall)
        else if (Player.velocity.Y > 0)
        {
            Player.velocity.Y *= 0.2f; // Slows down falling speed significantly
        }
    }
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && mainPlayer.ActiveForm != QuirkSkills.None)
            {
                Player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 2);
                 Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
                 ElectricSoundTimer++;

                 if (ElectricSoundTimer >= 900 + Main.rand.Next(-120, 120))
                 {
                     SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingAura"), Player.position);
                     ElectricSoundTimer = 0;

                     Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, default, 0.5f);
                 }
            }
            else
            {
                isFullCowlingBuffActive = false;
            }
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && isGearshiftActive)
            {
                Player.AddBuff(ModContent.BuffType<GearshiftBuff>(), 2);
                
            }
            else
            {
                isGearshiftActive = false;
                isGearshiftBuffActive = false;
                
            }
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
        if (isGearshiftActive)
        {
        // This adds a blue tint/glow to the character sprite itself
        drawInfo.colorArmorBody = Color.RoyalBlue;
        drawInfo.colorArmorHead = Color.RoyalBlue;
        drawInfo.colorArmorLegs = Color.RoyalBlue;
        
        // This creates a "God Mode" style afterimage trail which looks like a contour
        Player.armorEffectDrawShadow = true; 
        Player.armorEffectDrawOutlines = true; // This forces a faint outline
        }
        }

    }
}

