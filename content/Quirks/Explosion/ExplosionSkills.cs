// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.ModLoader;
// using MyHeroMod.content;
// using Terraria.ID;
// using MyHeroMod.content.System;
// using Terraria.Audio;
// using Terraria.DataStructures;
// using MyHeroMod.content.Quirks.Explosion;
// using  MyHeroMod.content.Quirks.Explosion.Buffs;
// using MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot;
// using MyHeroMod.content.Quirks.Explosion.Projectiles.StunGrenade;
// using MyHeroMod.content.Quirks.Explosion.Projectiles;
// using MyHeroMod.content.Quirks.Explosion.Projectiles.FullPower;



// namespace MyHeroMod.content.Quirks.Explosion
// {
//     public partial class ExplosionPlayer : ModPlayer
//     {
//         public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
//         {
//             var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

//             if (MainPlayer.SelectedQuirk == QuirkType.Explosion) 
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
//                     case QuirkSkills.StunGrenade:
//                     DoStunGrenade(mainPlayer);
                    
//                     SetCooldown(skill, 300);
//                     break;

//                     case QuirkSkills.FullPowerBlast:
//                     DoFullPowerBlast(mainPlayer);

//                     break;
//                     case QuirkSkills.ApShot:
//                     DoApShot(mainPlayer);

//                     SetCooldown(skill, 60);
//                     break;
//                     case QuirkSkills.ApMachineGun:
//                     DoApMachineGun(mainPlayer);

//                     SetCooldown(skill, 60);
//                     break;
//                     case QuirkSkills.HowitzerImpact:
//                     DoHowitzerImpact(mainPlayer);
                    
//                     SetCooldown(skill, 300);
//                     break;
                    
//                     case QuirkSkills.Cluster:
//                     ActivateCluster(mainPlayer);

//                     SetCooldown(skill, 60);
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

//         private void DoApShot(TransformationPlayer mainPlayer)
//         {
//             CombatText.NewText(Player.getRect(), Color.Orange, "AP-SHOT!");
//             Vector2 Velocity = Main.MouseWorld - Player.Center;
//             Velocity.Normalize();
//             Velocity *= 15f;

//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 Velocity,
//                 ModContent.ProjectileType<ApShotProj>(),
//                 40, 
//                 2f, 
//                 Player.whoAmI
//             );
//             CurrentSweat += 15;
//         }

//         private void DoFullPowerBlast(TransformationPlayer mainPlayer)
//         {

//         int BaseDamage = 80; 
           
//         float ModifiedDamage = 1;

//         if (IsGrenadierBracersOn && CurrentSweat > MaxSweat){
//         CurrentSweat -= 30;    
//         ModifiedDamage += 1f;        
//         }
//         int FinalDamage = (int)(BaseDamage * ModifiedDamage);

//         CombatText.NewText(Player.getRect(), Color.Orange, "DIE!");
            



//          Vector2 Velocity = Main.MouseWorld - Player.Center;
//             Velocity.Normalize();
//             Velocity *= 15f;

//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 Velocity,
//                 ModContent.ProjectileType<FullPowerProj>(),
//                 FinalDamage, 
//                 2f, 
//                 Player.whoAmI
//             );

//             if (IsGrenadierBracersOn != true)
//             {
//                 Player.statLife -= 5;
//             if (Player.statLife <= 0)
//             {
//                 var reason = PlayerDeathReason.ByCustomReason(
//                 Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.BlueFireDeathMessage", Player.name));
//                 Player.KillMe(reason, 5, 0);
//             }
                
//             }

            
//             CurrentSweat += 15;   
//         }
//         private void DoApMachineGun(TransformationPlayer mainPlayer)
//         {
//             if (Player.ownedProjectileCounts[ModContent.ProjectileType<ApMachineGunProj>()] > 0)
//             return;

//             if (Player.GetModPlayer<TransformationPlayer>().CurrentStage >= QuirkStage.Advanced)
//             {
//                 CombatText.NewText(Player.getRect(), Color.Orange, "AP MACHINE GUN!");
//             }
//             else
//             {
//                 CombatText.NewText(Player.getRect(), Color.Orange, "AP-SHOT: AUTO-CANNON!");
//             }

//             Vector2 direction = Main.MouseWorld - Player.Center;
//             direction.Normalize();

//             // Lança o Controlador
//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 direction,
//                 ModContent.ProjectileType<Projectiles.ApShot.ApMachineGunProj>(),
//                 0, 
//                 0f, 
//                 Player.whoAmI

//              );
            
//         }

//         private void DoHowitzerImpact(TransformationPlayer mainPlayer)
//         {
//             if (Player.ownedProjectileCounts[ModContent.ProjectileType<HowitzerImpactProj>()] > 0)
//                 return;


//             int BaseDamage = 0;

//             switch(mainPlayer.CurrentStage){
//                 case QuirkStage.Initial:
//                 BaseDamage = 200;
//                 break;
            
//                 case QuirkStage.Adequation:
//                 BaseDamage = 250;
//                 break;
          
//                 case QuirkStage.Intermediate:
//                 BaseDamage = 300;
//                 break;
            
//                 case QuirkStage.Advanced:
//                 BaseDamage = 450;
//                 break;
          
//                 case QuirkStage.Final:
//                 BaseDamage = 700;
//                 break;
        
//                 default:
//                 BaseDamage =100;
//                 break;
                    
//             }
        
            

             
           
//         float ModifiedDamage = 1;

//         if (IsClusterActive){
         
//         ModifiedDamage += 2.5f;        
//         }
//         int FinalDamage = (int)(BaseDamage * ModifiedDamage);



            

//             if (IsClusterActive){
//                 CombatText.NewText(Player.getRect(), Color.Orange, "HOWITZER IMPACT: CLUSTER!");
//             }
//             else
//             {
//                 CombatText.NewText(Player.getRect(), Color.Orange, "HOWITZER IMPACT!");
//             }

//             // Spawna o projétil que vai controlar o player
//             // A velocidade inicial não importa aqui, pois a AI[0] controla a subida
//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 Vector2.Zero, 
//                 ModContent.ProjectileType<HowitzerImpactProj>(),
//                 FinalDamage, // Dano alto (Impacto)
//                 10f, // Knockback alto
//                 Player.whoAmI
//             );
//             CurrentSweat += 15;
//         }

//         private void DoStunGrenade(TransformationPlayer mainPlayer)
//         {
//             CombatText.NewText(Player.getRect(), Color.Orange, "STUN GRENADE!");
//             // Evita usar se já estiver usando
//             Vector2 Velocity = Main.MouseWorld - Player.Center;
//             Velocity.Normalize();
//             Velocity *= 15f;

//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 Velocity,
//                 ModContent.ProjectileType<StunGrenadeProj>(),
//                 40, 
//                 2f, 
//                 Player.whoAmI
//             );
//             CurrentSweat += 15;
//         }
//         private void ActivateCluster(TransformationPlayer mainPlayer)
//         {
//             if (IsClusterActive)
//             {
//                 IsClusterActive = false;
//                 Player.ClearBuff(ModContent.BuffType<Buffs.ClusterBuff>());
//                 Main.NewText("Flash Fire Fist Deactivated", Color.OrangeRed);   
//                 SetCooldown(QuirkSkills.FlashFireFist, 120);
//                 return;
                
//             }
//             CombatText.NewText(Player.getRect(), Color.Orange, "CLUSTER!");
//             IsClusterActive = true;
            
//         }
// }
// }