// using Terraria;
// using Terraria.ModLoader;
// using Microsoft.Xna.Framework;

// namespace MyHeroMod.content.Quirks.OFA8th
// {
//     public class StockPileBuff : ModBuff
//     {
//         public override string Texture => "MyHeroMod/Assets/BuffImage/OneForAllStockPile";
//         public override void SetStaticDefaults()
//         {
//             Main.buffNoSave[Type] = true;
//             Main.buffNoTimeDisplay[Type] = true;
//         }

//         public override void Update(Player player, ref int buffIndex)
//         {
//             var transformPlayer = player.GetModPlayer<TransformationPlayer>();
//             var ofaPlayer = player.GetModPlayer<OneForAll8thPlayer>();


//             if (ofaPlayer.percentage == 45)
//             {
//                 player.moveSpeed += 3f; 
//                 player.statDefense += 3;  
//                 player.jumpSpeedBoost += 5f;
//                 player.noFallDmg = true;
                
//             }
//             if (ofaPlayer.percentage == 10)
//             {
//                 player.moveSpeed += 2f; 
//                 player.statDefense += 3;   
//                 player.jumpSpeedBoost += 3f; 
//                 player.noFallDmg = true;
//             }
//             if (ofaPlayer.percentage == 5)
//             {
//                 player.moveSpeed += 1.5f; 
//                 player.statDefense += 2;    
//                 player.jumpSpeedBoost += 2.0f;
//                 player.noFallDmg = true;
//             }

            

//             if (ofaPlayer.p)
//             {
//                 player.moveSpeed += 0.50f; 
//                 player.statDefense += 2;    
//                 player.jumpSpeedBoost += 3.0f;     
//             }
//             else if (transformPlayer.ActiveForm == QuirkSkills.StockPileMaximum)
//             {
//                 player.moveSpeed += 0.80f; 
//                 player.statDefense += 3;    
//                 player.jumpSpeedBoost += 4.5f;      
//             }
//         }
//     }
// }