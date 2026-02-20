// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.ModLoader;
// using MyHeroMod.content;
// using Terraria.ID;
// using MyHeroMod.content.System;
// using Terraria.Audio;
// using Terraria.DataStructures;
// using MyHeroMod.content.Quirks.HellFlames;
// using MyHeroMod.content.Quirks.HellFlames.Projectiles;

// using MyHeroMod.content.Quirks.HellFlames.Projectiles.IgnitedArrow;
// using MyHeroMod.content.Quirks.HellFlames.Projectiles.JetBurn;
// using MyHeroMod.content.Quirks.HellFlames.Buffs;

// namespace MyHeroMod.content.Quirks.HellFlames
// {
//     public partial class HellFlamesPlayer : ModPlayer
//     {
//         public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
//         {
//             var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

//             if (MainPlayer.SelectedQuirk == QuirkType.HellFlames) 
//             {
//                 if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot1);
//                 if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot2);
//                 if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot3);
//                 if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.TransformSlot);
//             }      
//         }

//         private void ExecuteSkill(TransformationPlayer mainPlayer, QuirkSkills skill)
//         {
//             if (SkillCooldowns.ContainsKey(skill) && SkillCooldowns[skill] > 0)
//             {
//                 Main.NewText("On cooldown!", Color.White);
//                 // Skill is on cooldown
//                 return;
//             }

//             switch (skill)
//             {
//                     case QuirkSkills.FlashFireFist:
//                     ActivateFlashFireFist(mainPlayer);

//                     SetCooldown(skill, 60);
//                     break;
//                     case QuirkSkills.ProminenceBurn:
//                     DoProminenceBurn();
//                     break;
//                     case QuirkSkills.JetBurn:

//                     DoJetBurn(mainPlayer);
//                     break;

//                     case QuirkSkills.IgnitedArrow:

//                     DoIgnitedArrow(mainPlayer);
//                     break;

//                     case QuirkSkills.HellSpider:

//                     DoHellSpider(mainPlayer);
//                     break;
                    
                
                

//             }
//         }
//         private void SetCooldown(QuirkSkills skill, int timeInTicks)
//         {
//             if (SkillCooldowns.ContainsKey(skill))
//             {
//                 SkillCooldowns[skill] = timeInTicks;
//             }
//             else
//             {
//                 SkillCooldowns.Add(skill, timeInTicks);
//             }
//         }
//         private void ActivateFlashFireFist(TransformationPlayer mainPlayer)
//         {
//             if (IsFlashFireFistActive)
//             {
//                 IsFlashFireFistActive = false;
//                 Player.ClearBuff(ModContent.BuffType<Buffs.FlashFireFistBuff>());
//                 Main.NewText("Flash Fire Fist Deactivated", Color.OrangeRed);   
//                 SetCooldown(QuirkSkills.FlashFireFist, 120);
//                 return;
                
//             }
//             CurrentHeat += 20;
//             IsFlashFireFistActive = true;

            
//         }
//         private void DoJetBurn(TransformationPlayer mainPlayer)
//         {
//             // Verifica se já existe um controlador ativo (para não spawnar duplicado)
//             if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.JetBurn.JetBurnController>()] > 0)
//                 return;

//             // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
//             // Note que a velocidade aqui define apenas a direção inicial da mira.
//             Vector2 direction = Main.MouseWorld - Player.Center;
//             direction.Normalize();

//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 direction,
//                 ModContent.ProjectileType<Projectiles.JetBurn.JetBurnController>(),
//                 0, // O controlador não dá dano direto
//                 0f,
//                 Player.whoAmI
            
//             );
//             CurrentHeat += 35;
//         }
//         private void DoProminenceBurn()
//         {
//             // Evita duplicar se já estiver ativo
//             if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.ProminenceBurn.ProminenceBurnController>()] > 0)
//                 return;

//             Main.NewText("PROMINENCE BURN!!!", Color.OrangeRed);
            
//             // Som inicial de explosão
//             SoundEngine.PlaySound(SoundID.Item117, Player.position); 

//             Vector2 direction = Main.MouseWorld - Player.Center;
//             direction.Normalize();

//             // Lança o Controlador
//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 direction,
//                 ModContent.ProjectileType<Projectiles.ProminenceBurn.ProminenceBurnController>(),
//                 0, 
//                 0f, 
//                 Player.whoAmI
//             );
//             CurrentHeat += 60;
//         }
//         private void DoIgnitedArrow(TransformationPlayer mainPlayer)
//         {
//             // Implementação do Ignited Arrow
//             int BaseDamage = 0;

//             switch(mainPlayer.CurrentStage){
//                 case QuirkStage.Initial:
//                 BaseDamage = 20;
//                 break;
            
//                 case QuirkStage.Adequation:
//                 BaseDamage = 40;
//                 break;
          
//                 case QuirkStage.Intermediate:
//                 BaseDamage =  45;
//                 break;
            
//                 case QuirkStage.Advanced:
//                 BaseDamage = 60;
//                 break;
          
//                 case QuirkStage.Final:
//                 BaseDamage = 80;
//                 break;
        
//                 default:
//                 BaseDamage =20;
//                 break;
                    
//             }
        
//         float ModifiedDamage = 1;

//         if (IsFlashFireFistActive){
         
//         ModifiedDamage += 1.5f;        
//         }
//         int FinalDamage = (int)(BaseDamage * ModifiedDamage);




//             Vector2 Velocity = Main.MouseWorld - Player.Center;
//             Velocity.Normalize();
//             Velocity *= 15f;

//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 Velocity,
//                 ModContent.ProjectileType<IgnitedArrowProj>(),
//                 FinalDamage, 
//                 2f, 
//                 Player.whoAmI
//             );
//             CurrentHeat += 15;
//         }
//         private void DoHellSpider(TransformationPlayer mainPlayer)
//         {
//             // Verifica se já existe um controlador ativo (para não spawnar duplicado)
//             if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.HellSpider.HellSpiderController>()] > 0)
//                 return;

//             // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
//             // Note que a velocidade aqui define apenas a direção inicial da mira.
//             Vector2 direction = Main.MouseWorld - Player.Center;
//             direction.Normalize();

//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 direction,
//                 ModContent.ProjectileType<Projectiles.HellSpider.HellSpiderController>(),
//                 0, // O controlador não dá dano direto
//                 0f,
//                 Player.whoAmI
//             );
//             CurrentHeat += 15;

//         }
//         }
//         }
