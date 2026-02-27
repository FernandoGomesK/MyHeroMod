// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.DataStructures;
// using Microsoft.Xna.Framework.Graphics;
// using Terraria.ModLoader;
// using Terraria.ID;
// using MyHeroMod.content;
// using MyHeroMod.content.System;
// using Terraria.Audio;
// using System.Collections.Generic;
// using MyHeroMod.content.Quirks.HellFlames.Buffs;
// using MyHeroMod.content.Debuffs;
// using MyHeroMod.content.Items.Support;

// namespace MyHeroMod.content.Quirks.HellFlames
// {
//     public partial class HellFlamesPlayer : ModPlayer
//     {
//         public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

//         public int MaxHeat = 100;
//         public int CurrentHeat = 0;
//         public int temperatureTimer = 0;

//         public bool IsCombatVestAlphaOn = false;
//         public bool IsCombatVestBetaOn = false;

        

//         public bool IsFlashFireFistActive = false;

//         public override void OnRespawn()
//         {
//             CurrentHeat = 0;
//             SkillCooldowns.Clear();
//         }
//         public override void PostUpdateEquips()
//         {
//             var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

//             if (mainPlayer.SelectedQuirk != QuirkType.HellFlames)  
//                 return;

//             if (IsFlashFireFistActive)
//             {
//                 Player.AddBuff(ModContent.BuffType<Buffs.FlashFireFistBuff>(), 2);
//             }
                
//             if (CurrentHeat >= MaxHeat)
//             {
//                 Player.AddBuff(ModContent.BuffType<Heatstroke>(), 2);
//             }
                
           
//             if (CurrentHeat > 0)
//             {
//                 Player.AddBuff(ModContent.BuffType<Heat>(), 2);
//             }

            

//             // Verifica se a individualidade atual é Hell Flames
//             if (mainPlayer.CurrentStage >= QuirkStage.Adequation && mainPlayer.SelectedQuirk == QuirkType.HellFlames)
//             {
//                 // 1. Define o tempo de voo (100 = voo curto/médio)
//                 Player.wingTimeMax = 50;

//                 // 2. Se o jogador NÃO tiver asas equipadas, simula uma
//                 if (Player.wingsLogic == 0)
//                 {
//                     Player.wingsLogic = 29; // Física das Solar Wings
//                     Player.wings = -1; // Esconde o sprite da asa
//                 }

//                 // 3. Anula dano de queda
//                 Player.noFallDmg = true;
//             }
//         }

//         public override void PreUpdate()
//         {
//             List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
//             foreach (var skill in keys)
//             {
//                 if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
//             }
//         }
        

//         public override void PostUpdate()
//         {
//             var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

//             if (mainPlayer.SelectedQuirk == QuirkType.HellFlames && mainPlayer.CurrentStage >= QuirkStage.Adequation)
//             {
//                 // CORREÇÃO AQUI: Substituí !Player.onFloor por Player.velocity.Y != 0
//                 bool isFlying = (Player.velocity.Y != 0) && (Player.wingTime > 0 || Player.rocketDelay > 0) && !Player.mount.Active;

//                 // Se estiver apenas caindo ou voando (sem estar montado)
//                 if (Player.velocity.Y != 0 && !Player.mount.Active)
//                 {
//                     // Lado Esquerdo (Fogo)
//                     if (Main.rand.NextBool(2)) 
//                     {
//                         int dustFire = Dust.NewDust(
//                             Player.position + new Vector2(-5, Player.height - 10), 
//                             Player.width / 2, 
//                             10, 
//                             DustID.Torch, 
//                             0, 2f, 100, default, 1.5f 
//                         );
//                         Main.dust[dustFire].noGravity = true;
//                         Main.dust[dustFire].velocity *= 0.5f; // Suaviza o movimento
//                     }

//                     // Lado Direito (Fogo tbm)
//                     if (Main.rand.NextBool(2))
//                     {
//                         int dustFire2 = Dust.NewDust(
//                             Player.position + new Vector2(Player.width / 2, Player.height - 10), 
//                             Player.width / 2, 
//                             10, 
//                             DustID.Torch, 
//                             0, 2f, 100, default, 1.5f
//                         );
//                         Main.dust[dustFire2].noGravity = true;
//                         Main.dust[dustFire2].velocity *= 0.5f;
//                     }
//                 }
//             }
//              if (CurrentHeat != 0)
// {
//     temperatureTimer++;

    
//     if (temperatureTimer >= 60)
//     {
//         temperatureTimer = 0;
        
        
//         int recoveryRate = 1;

        
//         if (IsCombatVestAlphaOn) recoveryRate += 1; 
//         if (IsCombatVestBetaOn)  recoveryRate += 5; 

        
//         if (CurrentHeat > 0)
//         {
//             CurrentHeat -= recoveryRate;
            
//             if (CurrentHeat < 0) CurrentHeat = 0;
//         }
//     }
// }
// else
// {
//     temperatureTimer = 0;
// }
//         }
//     }
// }