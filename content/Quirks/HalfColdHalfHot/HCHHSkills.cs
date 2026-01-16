using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.HeavenPiercingWall;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.HCHellSpider;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.JetKindling;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.FlashFreezeHeatWave;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Buffs;

using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.HalfColdHalfHot
{
    public partial class HalfColdHalfHotPlayer : ModPlayer
    {
                public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (MainPlayer.SelectedQuirk == QuirkType.HalfColdHalfHot) 
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
                    // case QuirkSkills.FlashFireFist:
                    // ActivateFlashFireFist(mainPlayer);

                    // SetCooldown(skill, 60);
                    // break;
                    case QuirkSkills.HeavenPiercingWall:

                    DoHeavenPiercingWall(mainPlayer);
                    break;
                    case QuirkSkills.JetKindling:

                    DoJetKindling(mainPlayer);
                    break;

                    // case QuirkSkills.IgnitedArrow:

                    // DoIgnitedArrow(mainPlayer);
                    // break;

                    case QuirkSkills.HCHellSpider:

                    DoHellSpider(mainPlayer);
                    break;
                    case QuirkSkills.HCFireFist:

                    ActivateFlashFireFist(mainPlayer);
                    break;

                    case QuirkSkills.HCPhosphor:

                    ActivatePhosphor(mainPlayer);
                    break;
                    case QuirkSkills.FlashFreezeHeatWave:
                    // AQUI ESTÁ A MUDANÇA:
                    // Em vez de chamar a lógica inteira, nós apenas ATIVAMOS o modo automático.
                    if (!IsFlashFreezeActive)
                    {
                        IsFlashFreezeActive = true;
                        FlashFreezeTimer = 0;
                        SetCooldown(skill, 20); // Exemplo de Cooldown
                    }
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

        private void DoHeavenPiercingWall(TransformationPlayer mainPlayer)
        {
        // Define a direção (Esquerda ou Direita baseado no mouse)
        float direction = Main.MouseWorld.X > Player.Center.X ? 1f : -1f;
    
        // Velocidade da onda (Rápida)
        Vector2 velocity = new Vector2(10f * direction, 0f);

        // Spawna o Controlador um pouco na frente do player
        Projectile.NewProjectile(
            Player.GetSource_FromThis(),
            Player.Center + new Vector2(20f * direction, 0), // Começa um pouco a frente
            velocity,
            ModContent.ProjectileType<IceWaveController>(),
            50, // Dano
            5f,
            Player.whoAmI

            
        );
        temperature -= 15;
}

        private void UpdateFlashFreeze()
        {
            
            FlashFreezeTimer++;

            // Configuração da Posição (Costas do Player)
            float offsetCostas = 20f; 
            Vector2 spawnPos = Player.Center - new Vector2(offsetCostas * Player.direction, 0f);
            spawnPos.Y += Main.rand.NextFloat(-10f, 10f);

            // FASE 1: GELO (0 a 2 segundos)
            if (FlashFreezeTimer < 120)
            {
                int iceDust = Dust.NewDust(spawnPos, 4, 4, DustID.IceTorch, 0, 0, 100, default, 3.5f);
                Main.dust[iceDust].noGravity = true;
                Main.dust[iceDust].velocity = new Vector2(-3f * Player.direction, 0f);
                Player.velocity *= 0.1f; 
            }
            // FASE 2: FOGO E DISPARO (Após 2 segundos)
            else
            {
                int fireDust = Dust.NewDust(spawnPos, 4, 4, DustID.Torch, 0, 0, 100, default, 4.5f);
                Main.dust[fireDust].noGravity = true;
                Main.dust[fireDust].velocity = new Vector2(-6f * Player.direction, 0f);

                // O DISPARO (Acontece uma única vez no frame 120)
                if (FlashFreezeTimer == 120)
                {
                    

                    if (Player.whoAmI == Main.myPlayer)
                    {
                        // Se você não tiver o "FlashFreezeProj", use "IceSpikeProj" ou outro existente
                        int projType = ModContent.ProjectileType<FlashFreezeProj>(); 

                        Vector2 Velocity = Main.MouseWorld - Player.Center;
                        Velocity.Normalize();
                        Velocity *= 15f;
                        // int projType = ModContent.ProjectileType<FlashFreezeProj>(); 

                        Projectile.NewProjectile(
                            Player.GetSource_FromThis(),
                            Player.Center,
                            Velocity,
                            projType,   
                            400, 
                            5f, 
                            Player.whoAmI
                        );
                    }
                }

                // DESLIGA SOZINHO (Dá 10 frames de fogo extra e desliga)
                if (FlashFreezeTimer >= 130)
                {
                    IsFlashFreezeActive = false;
                    FlashFreezeTimer = 0;
                }
            }
        }
            
        private void DoJetKindling(TransformationPlayer mainPlayer)
        {
            // Verifica se já existe um controlador ativo (para não spawnar duplicado)
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0)
                return;

            // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
            // Note que a velocidade aqui define apenas a direção inicial da mira.
            Vector2 direction = Main.MouseWorld - Player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                direction,
                ModContent.ProjectileType<JetKindlingController>(),
                0, // O controlador não dá dano direto
                0f,
                Player.whoAmI
            
            );
            temperature += 15;
        }
        
        
        private void DoHellSpider(TransformationPlayer mainPlayer)
        {
            // Verifica se já existe um controlador ativo (para não spawnar duplicado)
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<HCHellSpiderController>()] > 0)
                return;

            // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
            // Note que a velocidade aqui define apenas a direção inicial da mira.
            Vector2 direction = Main.MouseWorld - Player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                direction,
                ModContent.ProjectileType<HCHellSpiderController>(),
                0, // O controlador não dá dano direto
                0f,
                Player.whoAmI
            );
            temperature += 15;

        }
        private void ActivatePhosphor(TransformationPlayer mainPlayer)
        {
            if (IsPhosphorActive)
            {
                IsPhosphorActive = false;
                Player.ClearBuff(ModContent.BuffType<PhosphorBuff>());
                Main.NewText("Phosphor Deactivated", Color.OrangeRed);   
                
                return;
                
            }
            
            IsPhosphorActive = true;
        }
        private void ActivateFlashFireFist(TransformationPlayer mainPlayer)
        {
            if (IsFlashFireFistActive)
            {
                IsFlashFireFistActive = false;
                Player.ClearBuff(ModContent.BuffType<HCFireFistBuff>());
                Main.NewText("Flash Fire Fist Deactivated", Color.OrangeRed);   
                
                return;
                
            }
            
            IsFlashFireFistActive = true;
    }
}}