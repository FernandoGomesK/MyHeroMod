// using Terraria;
// using Terraria.ModLoader;
// using MyHeroMod.content.System;
// using MyHeroMod.content;
// using MyHeroMod.content.Buffs;
// using Terraria.ID;
// using Terraria.Audio;
// using Microsoft.Xna.Framework;
// using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
// using MyHeroMod.content.Quirks.HellFlames;
// using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
// using MyHeroMod.content.Quirks.AllForOne;
// using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.IceShot;
// using MyHeroMod.content.System.Interfaces;
// using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.HellSpider;



// public class HellSpiderSkill: QuirkBaseSkill
// {
    
//     public override string Name => "Hell Spider";
    

//     public override string GetDisplayName(Player player)
//     {
//         var transPlayer = player.GetModPlayer<TransformationPlayer>();
       

//             if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
//             {
//                 return "Ice Spike / Hell Spider";
//             }
//             return "Flashfire Fist: Hell Spider";
           
//     }

   
//     public override string Description => "Shoot burning Lines of fire";
//     public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
//     public override string Category => "Fire";
//     public override int BaseCooldown => 120;

//     public override QuirkType RequiredQuirk => QuirkType.HellFlames;
//     public override QuirkStage RequiredStage => QuirkStage.Adequation;
//     public override bool IsDefaultSkill => false;
    

//     public override bool CheckUnlock(TransformationPlayer player)
//     {
//         if (player.HasActiveQuirk(QuirkType.HellFlames))
//         {
//             return player.CurrentStage >= QuirkStage.Adequation; 
//         }

//         else if(player.HasActiveQuirk(QuirkType.HalfColdHalfHot))
//         {
//             return player.CurrentStage >= QuirkStage.Intermediate; 
//         } 
            
//         else if(player.HasActiveQuirk(QuirkType.Blueflame))
//         {
//             return player.CurrentStage >= QuirkStage.Advanced; 
//         }
//         return false;
//     }



//     public override void OnUse(Player player)
//     {
//         var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
//         var bluePlayer = player.GetModPlayer<BlueflamePlayer>();
//         var transPlayer = player.GetModPlayer<TransformationPlayer>();
//         var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

//         Vector2 direction = Main.MouseWorld - player.Center;
//         direction.Normalize();

//         float multiplier = 1.0f;
        
        
//         if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot) && hchhPlayer.IsSurgeArmGauntletsOn) 
//         {
//             multiplier += 0.5f;
//         }

        
//         int fireDamage = 110; 
        
//         if (transPlayer.HasActiveQuirk(QuirkType.Blueflame))
//         {
//             fireDamage = transPlayer.CurrentStage switch {
//                 QuirkStage.Adequation => 180, QuirkStage.Intermediate => 280,
//                 QuirkStage.Advanced => 480, QuirkStage.Final => 950, _ => 180
//             };
//         }
//         else 
//         {
//             fireDamage = transPlayer.CurrentStage switch {
//                 QuirkStage.Adequation => 110, QuirkStage.Intermediate => 180,
//                 QuirkStage.Advanced => 360, QuirkStage.Final => 760, _ => 110
//             };
//         }

        
//         if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
//         {
//             if (hchhPlayer.IsFlashFireFistActive)
//             {
//                 if (player.ownedProjectileCounts[ModContent.ProjectileType<HellSpiderController>()] > 0) return;
                
//                 Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<HellSpiderController>(), (int)(fireDamage * multiplier), 0f, player.whoAmI);
                
//                 foreach (var modPlayer in player.ModPlayers)
//                 {
//                     if (modPlayer is IHeroTemperature heatUser) heatUser.AddHeat(25);
//                 }
                
//                 return;
                
                
//             }
//             else
//             {
                
//                 int iceDamage = transPlayer.CurrentStage switch {
//                     QuirkStage.Initial => 25, QuirkStage.Adequation => 55, QuirkStage.Intermediate => 90,
//                     QuirkStage.Advanced => 180, QuirkStage.Final => 380, _ => 25
//                 };
                
//                 Vector2 velocity = direction * 15f;
//                 Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, velocity, ModContent.ProjectileType<IceShotProj>(), (int)(iceDamage * multiplier), 2f, player.whoAmI);

//                 foreach (var modPlayer in player.ModPlayers)
//                 {
//                     if (modPlayer is IHeroTemperature heatUser) heatUser.ReduceHeat(25);
//                 }
                
//                 return; 
//             }
//         }
        
        
//         else if (transPlayer.HasActiveQuirk(QuirkType.HellFlames))
//         {
//             if (player.ownedProjectileCounts[ModContent.ProjectileType<HellSpiderController>()] > 0) return;
            
//             Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<HellSpiderController>(), (int)(fireDamage * multiplier), 0f, player.whoAmI);
            
//             foreach (var modPlayer in player.ModPlayers)
//             {
//                 if (modPlayer is IHeroTemperature heatUser) 
//                 {
//                     heatUser.AddHeat(25);
//                 }
//             }
            
//             return;
//         }
        
       
//         else if (transPlayer.HasActiveQuirk(QuirkType.Blueflame))
//         {
//             if (player.ownedProjectileCounts[ModContent.ProjectileType<HellSpiderController>()] > 0) return;
            
//             Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<HellSpiderController>(), (int)(fireDamage * multiplier), 0f, player.whoAmI);
            
//             foreach (var modPlayer in player.ModPlayers)
//             {
//                 if (modPlayer is IHeroTemperature heatUser) 
//                 {
//                     heatUser.AddHeat(25);
//                 }
//             } 
//             return;
//         }

        
//     }
// }