using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using Terraria.ID;
using MyHeroMod.content.System;
using Terraria.Audio;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.Explosion;
using MyHeroMod.content.Quirks.Explosion.Projectiles;




namespace MyHeroMod.content.Quirks.Explosion
{
    public partial class ExplosionPlayer : ModPlayer
    {
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
//             

//         private void DoFullPowerBlast(TransformationPlayer mainPlayer)
//         {


            
//             CurrentSweat += 15;   
//         }
//         private void DoApMachineGun(TransformationPlayer mainPlayer)
//         {
//             
            
//         }

//         private void DoHowitzerImpact(TransformationPlayer mainPlayer)
//         {
//             
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
    }}