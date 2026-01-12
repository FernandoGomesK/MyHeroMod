using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using Terraria.ID;
using MyHeroMod.content.System;
using Terraria.Audio;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.Blueflames;
using MyHeroMod.content.Quirks.Blueflames.Projectiles.BlueFireBall;
using MyHeroMod.content.Quirks.Blueflames.Projectiles.BlueVanishingFist;
using MyHeroMod.content.Quirks.Blueflames.Projectiles.BlueFlameThrower;




namespace MyHeroMod.content.Quirks.Blueflames
{
    public partial class BlueFlamesPlayer : ModPlayer
    {
        

        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (MainPlayer.SelectedQuirk == QuirkType.BlueFlames) 
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

                    case QuirkSkills.BlueRage:
                    ActivateRage(mainPlayer);


                    break;

                    
                    case QuirkSkills.BlueFlashFireFist:
                    ActivateFlashFireFist(mainPlayer);

                    SetCooldown(skill, 60);
                    break;

                    case QuirkSkills.BluePhosphor:
                    ActivatePhosphor(mainPlayer);

                    break;

                    case QuirkSkills.BlueFireBall:
                    DoFireBall(mainPlayer);
                    break;

                    case QuirkSkills.BlueVanishingFist:
                    DoVanishingFist(mainPlayer);
                    break;

                    case QuirkSkills.BlueFlamethrower:
                    if (IsFlashFireFistActive)
                    {
                        DoJetBurn(mainPlayer);
                    }
                    else{
                        DoFlameThrower(mainPlayer);}    
                    break;


                    
                    case QuirkSkills.BlueJetBurn:

                    DoJetBurn(mainPlayer);
                    break;
         
                    case QuirkSkills.BlueHellSpider:
                    DoHellSpider(mainPlayer);
                    break;

                    case QuirkSkills.BlueProminenceBurn:
                    DoProminenceBurn();
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

        private void ActivateRage(TransformationPlayer mainPlayer)
        {
            
        }
        private void ActivateFlashFireFist(TransformationPlayer mainPlayer)
        {
            if (IsFlashFireFistActive)
            {
                CombatText.NewText(Player.getRect(), Color.Blue, "Flashfire Fist Off");
                IsFlashFireFistActive = false;
                Player.ClearBuff(ModContent.BuffType<Buffs.BlueFlashFireFistBuff>());
                Main.NewText("Flash Fire Fist Deactivated", Color.OrangeRed);   
                SetCooldown(QuirkSkills.FlashFireFist, 120);
                return;
                
            }
            CombatText.NewText(Player.getRect(), Color.Blue, "Flashfire Fist!");
            CurrentHeat += 20;
            IsFlashFireFistActive = true;

            
        }

        private void ActivatePhosphor(TransformationPlayer mainPlayer)
        {
            
        }

        private void DoFireBall(TransformationPlayer mainPlayer)
        {
             Vector2 Velocity = Main.MouseWorld - Player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                Velocity,
                ModContent.ProjectileType<BlueFireBallProj>(),
                40, 
                2f, 
                Player.whoAmI);
        }

        private void DoVanishingFist(TransformationPlayer mainPlayer)
        {
            Vector2 Velocity = Main.MouseWorld - Player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                Velocity,
                ModContent.ProjectileType<BlueVanishingFistProj>(),
                40, 
                2f, 
                Player.whoAmI);
        }
            
        private void DoFlameThrower(TransformationPlayer mainPlayer)
        {
            CombatText.NewText(Player.getRect(), Color.Blue, "Flame Thrower!");
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.JetBurn.BlueJetBurnController>()] > 0)
                return;

            // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
            // Note que a velocidade aqui define apenas a direção inicial da mira.
            Vector2 direction = Main.MouseWorld - Player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                direction,
                ModContent.ProjectileType<BlueFlamethrowerController>(),
                0, // O controlador não dá dano direto
                0f,
                Player.whoAmI);
            
        }

        private void DoJetBurn(TransformationPlayer mainPlayer)
        {
            CombatText.NewText(Player.getRect(), Color.Blue, "FlashFire Fist: Jet Burn!");
            // Verifica se já existe um controlador ativo (para não spawnar duplicado)
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.JetBurn.BlueJetBurnController>()] > 0)
                return;

            // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
            // Note que a velocidade aqui define apenas a direção inicial da mira.
            Vector2 direction = Main.MouseWorld - Player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                direction,
                ModContent.ProjectileType<Projectiles.JetBurn.BlueJetBurnController>(),
                0, // O controlador não dá dano direto
                0f,
                Player.whoAmI
            
            );
            CurrentHeat += 15;
        }
        private void DoProminenceBurn()
        {
            // Evita duplicar se já estiver ativo
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.ProminenceBurn.BlueProminenceBurnController>()] > 0)
                return;

            Main.NewText("PROMINENCE BURN!!!", Color.OrangeRed);
            
            // Som inicial de explosão
            SoundEngine.PlaySound(SoundID.Item117, Player.position); 

            Vector2 direction = Main.MouseWorld - Player.Center;
            direction.Normalize();

            // Lança o Controlador
            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                direction,
                ModContent.ProjectileType<Projectiles.ProminenceBurn.BlueProminenceBurnController>(),
                0, 
                0f, 
                Player.whoAmI
            );
            CurrentHeat += 30;
        }
        
        private void DoHellSpider(TransformationPlayer mainPlayer)
        {
            // Verifica se já existe um controlador ativo (para não spawnar duplicado)
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.HellSpider.BlueHellSpiderController>()] > 0)
                return;

            // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
            // Note que a velocidade aqui define apenas a direção inicial da mira.
            Vector2 direction = Main.MouseWorld - Player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                direction,
                ModContent.ProjectileType<Projectiles.HellSpider.BlueHellSpiderController>(),
                0, // O controlador não dá dano direto
                0f,
                Player.whoAmI
            );
            CurrentHeat += 15;

        }
        }
        }
