// using Terraria;
// using Microsoft.Xna.Framework;
// using Terraria.Audio;
// using Terraria.ID;
// using Terraria.ModLoader;
// using MyHeroMod.content.System.BasePlayer;
// using MyHeroMod.content.Quirks.OFA9th.Projectiles;
// using MyHeroMod.content.Buffs;
// using MyHeroMod.content.System.GeneralSkills;


// namespace MyHeroMod.content.System
// {
//     public class CruiseFlightSkill : QuirkSkill
//     {
//         public override string Name => "Cruise Flight";
//         public override string Description => "Fly at a constant speed to yout cursor direction!";
//         public override string IconPath => "MyHeroMod/Assets/Skills/Dash";
//         public override int BaseCooldown => 120;

        
       
//         public override QuirkType RequiredQuirk => QuirkType.Quirkless;
//         public override QuirkStage RequiredStage => QuirkStage.Initial;
        
//         public override bool IsDefaultSkill => false;
//         public override bool IsBaseQuirk => false;
        

//         public override bool CheckUnlock(TransformationPlayer player)
//         {

//             if (player.HasActiveQuirk(QuirkType.Flight))
//         {
//             return player.CurrentStage >= QuirkStage.Initial; 
//         }

//         else if(player.HasActiveQuirk(QuirkType.SlideAndGlide))
//         {
//             return player.CurrentStage >= QuirkStage.Adequation; 
//         }
//                 return false;
//          }
        

//         public override void OnUse(Player player)
//         {
        
//             if (player.HasBuff(ModContent.BuffType<CruisingBuff>()))
//             {
//                 player.ClearBuff(ModContent.BuffType<CruisingBuff>());
//             }
//             else
//             {
                
//                 if (player.wingTimeMax > 0 && player.wingTime > 0)
//                 {
                    
//                     player.AddBuff(ModContent.BuffType<CruisingBuff>(), 36000); 

//                     Projectile.NewProjectile(
//                         player.GetSource_FromThis(),
//                         player.Center,
//                         Vector2.Zero, 
//                         ModContent.ProjectileType<FlightProj>(),
//                         0, 
//                         0f, 
//                         player.whoAmI
//                     );
//                 }
//             }
//         }
//     }
// }