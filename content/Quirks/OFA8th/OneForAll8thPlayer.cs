// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.DataStructures;
// using Microsoft.Xna.Framework.Graphics;
// using Terraria.ModLoader;
// using Terraria.ID;
// using MyHeroMod.content;
// using MyHeroMod.content.Quirks;
// using MyHeroMod.content.Quirks.OFA8th.Projectiles;
// using Terraria.Audio;
// using MyHeroMod.content.System;
// using System.Collections.Generic;

// namespace MyHeroMod.content.Quirks.OFA8th
// {
//     public partial class OneForAll8thPlayer : ModPlayer
//     {
//         public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

//         public override void OnRespawn()
//         {
//             Player.GetModPlayer<TransformationPlayer>().ActiveForm = QuirkSkills.None;
//             SkillCooldowns.Clear();
//         }

//             public override void PostUpdateEquips()
//         {
//             var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            

//             if (mainPlayer.SelectedQuirk == QuirkType.OneForAll8th && mainPlayer.CurrentStage >= QuirkStage.Adequation)
//             {
//                 Player.moveSpeed += 1.5f;
//                 Player.jumpSpeedBoost += 1.5f;
//             }

//             // Só roda se for o All Might e estiver transformado
//             if (mainPlayer.SelectedQuirk == QuirkType.OneForAll8th && mainPlayer.ActiveForm != QuirkSkills.None)
//             {
//                 // 1. Aplica o Buff de Status (Defesa/Dano)
//                 Player.AddBuff(ModContent.BuffType<StockPileBuff>(), 2);

                
                
//             }
//         }
//     }
// }


        