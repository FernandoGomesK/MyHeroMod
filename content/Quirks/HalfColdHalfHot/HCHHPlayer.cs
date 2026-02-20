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

// using MyHeroMod.content.Quirks.HalfColdHalfHot.Buffs;
// using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles;

// namespace MyHeroMod.content.Quirks.HalfColdHalfHot
// {
//     public partial class HalfColdHalfHotPlayer : ModPlayer
//     {
//         public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();
//         public int temperature = 0;

//         public int maxTemperature = 100;
//         public int MinimumTemperature = -100;

//         public int temperatureTimer = 0;
//         public bool IsFlashFreezeActive = false;

//         public bool IsFlashFireFistActive = false;
//         public bool IsPhosphorActive = false;

//         // FlashFreeze

//         public int FlashFreezeTimer = 0; 

//         public int ProjectileTimer = 0;

//         public bool IsCombatVestAlphaOn = false;

//         public bool IsCombatVestBetaOn = false;
//         public bool IsSurgeArmGauntletsOn = false;


//         public override void OnRespawn()
//         {
//             temperature = 0;
//             IsFlashFireFistActive = false;
//             IsPhosphorActive = false;
//             SkillCooldowns.Clear();
//         }

//         public override void ResetEffects()
//         {
//             IsCombatVestAlphaOn = false;
//             IsCombatVestBetaOn = false;
//             IsSurgeArmGauntletsOn = false;
//         }

//         public override void PreUpdate()
// {
//     // Cria uma lista temporária das skills em cooldown para poder modificar o dicionário
//     List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
    
//     foreach (var skill in keys)
//     {
//         // Se o cooldown for maior que 0, diminui 1 a cada frame
//         if (SkillCooldowns[skill] > 0) 
//         {
//             SkillCooldowns[skill]--;
//         }
//     }
// }



//         public override void PostUpdateEquips()
//         {
//             var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

//             if (IsFlashFireFistActive)
//             {
//                 Player.AddBuff(ModContent.BuffType<Buffs.HCFireFistBuff>(), 2);
//             }

//             if (temperature > 0)
//             {
//             Player.AddBuff(ModContent.BuffType<Buffs.TemperatureBuff>(), 2);
//             }

//             if (temperature < 0)
//                 {
//                 Player.AddBuff(ModContent.BuffType<Buffs.TemperatureBuff>(), 2);
//                 }
//             if (temperature >= maxTemperature)
//                 {
//                     Player.AddBuff(ModContent.BuffType<Debuffs.Heatstroke>(), 2);
//                 }
//                 if (temperature <= MinimumTemperature)
//                 {
//                     Player.AddBuff(ModContent.BuffType<Debuffs.FrostBite>(), 2);
//                 }


//             if (mainPlayer.SelectedQuirk != QuirkType.HalfColdHalfHot)  
//                 return;

//             // Verifica se a individualidade atual é Half Cold Half Hot
//             if (mainPlayer.CurrentStage >= QuirkStage.Intermediate && mainPlayer.SelectedQuirk == QuirkType.HalfColdHalfHot)
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
        

//         public override void PostUpdate()
//         {
//             var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

//             if (IsFlashFreezeActive)
//     {
//         UpdateFlashFreeze();
//     }

//             if (mainPlayer.SelectedQuirk == QuirkType.HalfColdHalfHot)
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

//                     // Lado Direito (Gelo)
//                     if (Main.rand.NextBool(2))
//                     {
//                         int dustIce = Dust.NewDust(
//                             Player.position + new Vector2(Player.width / 2, Player.height - 10), 
//                             Player.width / 2, 
//                             10, 
//                             DustID.IceTorch, 
//                             0, 2f, 100, default, 1.5f
//                         );
//                         Main.dust[dustIce].noGravity = true;
//                         Main.dust[dustIce].velocity *= 0.5f;
//                     }
//                 }

//                 if (temperature != 0)
// {
//     temperatureTimer++;

    
//     if (temperatureTimer >= 60)
//     {
//         temperatureTimer = 0;
        
        
//         int recoveryRate = 1;

        
//         if (IsCombatVestAlphaOn) recoveryRate += 1; 
//         if (IsCombatVestBetaOn)  recoveryRate += 5; 

        
//         if (temperature < 0) 
//         {
//             temperature += recoveryRate;
           
//             if (temperature > 0) temperature = 0;
//         }
//         else if (temperature > 0)
//         {
//             temperature -= recoveryRate;
            
//             if (temperature < 0) temperature = 0;
//         }
//     }
// }
// else
// {
//     temperatureTimer = 0;
// }
                
//             }
//         }
        
//     }
// }