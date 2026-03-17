// using Terraria;
// using Terraria.ModLoader;
// using Microsoft.Xna.Framework;
// using MyHeroMod.content.Quirks.HellFlames;

// namespace MyHeroMod.content.Quirks.Buffs
// {
//     public class HellFlashFireFistBuff : ModBuff
//     {
//         public override string Texture => "MyHeroMod/Assets/BuffImage/FlashFireFistBuff";
//         public override void SetStaticDefaults()
//         {
//             Main.buffNoSave[Type] = true;
//             Main.buffNoTimeDisplay[Type] = true;
//         }

//         public override void Update(Player player, ref int buffIndex)
//         {
            
//             var HellPlayer = player.GetModPlayer<HellFlamesPlayer>();

//             HellPlayer.IsFlashFireFistActive = true;

//             if (HellPlayer.IsFlashFireFistActive)
//             {
//                 // Apply Flash Fire Fist effects
//                 player.GetDamage(DamageClass.Melee) += 0.20f; // Increase melee damage by 20%
//                 player.moveSpeed += 2.0f; // Increase movement speed by 20%
//             }
            
            

           

            
//         }
//     }
// }