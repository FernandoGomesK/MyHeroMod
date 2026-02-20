// using Terraria;
// using Terraria.ModLoader;
// using MyHeroMod.content.System;
// using MyHeroMod.content;
// using MyHeroMod.content.Quirks.DangerSense;
// using MyHeroMod.content.Buffs;
// using Terraria.ID;
// using Terraria.Audio;
// using Microsoft.Xna.Framework;

// public class DangerSenseSkill : QuirkSkill
// {
//     public override string Name => "DangerSense";
//     public override string Description => "Activates DangerSense";
//     public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";

//     public override int BaseCooldown => 30;

//     public override QuirkType RequiredQuirk => QuirkType.DangerSense;
//     public override QuirkStage RequiredStage => QuirkStage.Advanced;
//     public override bool IsDefaultSkill => false;
//     public override bool IsBaseQuirk => true;

//     public override void OnUse(Player player)
//     {
//         var dsPlayer = player.GetModPlayer<DangerSensePlayer>();    

        

//             if (dsPlayer.CurrentStage >= QuirkStage.Adequation)
//             {
//                 player.AddBuff(ModContent.BuffType<OvertimeBuff>(), 300);
//                 dsPlayer.IsOvertimeActive = true;
//                 dsPlayer.IsDangerSenseActive = true; 
//                 CombatText.NewText(player.getRect(), Color.Yellow, "Overtime!");
//             }
//             else
//             {
                
//                 ToggleDangerSense(player, dsPlayer);
//             }
//         }

    
//         private void ToggleDangerSense(Player player, DangerSensePlayer dsPlayer)
//         {
//             dsPlayer.IsDangerSenseActive = !dsPlayer.IsDangerSenseActive;

//             if (dsPlayer.IsDangerSenseActive)
//             {
//                 CombatText.NewText(player.getRect(), Color.Orange, "Danger Sense: ON");
//                 SoundEngine.PlaySound(SoundID.Item4, player.position);
//             }
//             else
//             {
//                 CombatText.NewText(player.getRect(), Color.Gray, "Danger Sense: OFF");
//                 SoundEngine.PlaySound(SoundID.Item4, player.position);
//             }
//         }
//     }
