// using Terraria;
// using Terraria.ModLoader;
// using MyHeroMod.content.System;
// using MyHeroMod.content;
// using MyHeroMod.content.Quirks.DangerSense;
// using MyHeroMod.content.Buffs;
// using Terraria.ID;
// using Terraria.Audio;
// using Microsoft.Xna.Framework;
// using MyHeroMod.content.Quirks.Explosion;
// using MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot;
// using MyHeroMod.Buffs;
// using MyHeroMod.content.Quirks.SlideAndGlide.Projectiles.ShootyGo;

// public class ShootyGoBlamSkill : QuirkSkill
// {
//     public override string Name => "Shooty Go Blam";
//     public override string Description => "Shoot a concentrated penetrating Projectile";
//     public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";

//     public override int BaseCooldown => 30;

//     public override QuirkType RequiredQuirk => QuirkType.SlideAndGlide;
//     public override QuirkStage RequiredStage => QuirkStage.Intermediate;
//     public override bool IsDefaultSkill => false;
//     public override bool IsBaseQuirk => false;


//                     public override void OnUse(Player player)
//             {

//                 var transPlayer = player.GetModPlayer<TransformationPlayer>();

            

                 


//         float damageMultiplier = 1.0f;
//         int MaxDamage = 45;
         

//             switch(transPlayer.CurrentStage){
//                 case QuirkStage.Initial:
//                 MaxDamage = 45;
//                 break;
            
//                 case QuirkStage.Adequation:
//                 MaxDamage = 45;
//                 break;
          
//                 case QuirkStage.Intermediate:
//                 MaxDamage = 60;
//                 break;
            
//                 case QuirkStage.Advanced:
//                 MaxDamage = 90;
//                 break;
          
//                 case QuirkStage.Final:
//                 MaxDamage = 180;
//                 break;
        
//                 default:
//                 MaxDamage =45;
//                 break;
                    
//             }

           

//             var finalDamage = (int)(damageMultiplier * MaxDamage);

// CombatText.NewText(player.getRect(), Color.Orange, "AP-SHOT!");
//             Vector2 Velocity = Main.MouseWorld - player.Center;
//             Velocity.Normalize();
//             Velocity *= 15f;

//             Projectile.NewProjectile(
//                 player.GetSource_FromThis(),
//                 player.Center,
//                 Velocity,
//                 ModContent.ProjectileType<ShootyGoProj>(),
//                 finalDamage, 
//                 2f, 
//                 player.whoAmI
//             );
            
//         }}
        