// using Terraria;
// using Terraria.ModLoader;
// using Microsoft.Xna.Framework;

// namespace MyHeroMod.content.Quirks.Blueflames.Buffs
// {
//     public class BlueRage : ModBuff
//     {
//         public override string Texture => "MyHeroMod/Assets/BuffImage/BlueRage";
        
//         public override void SetStaticDefaults()
//         {
//             Main.buffNoSave[Type] = true;
//             Main.buffNoTimeDisplay[Type] = true;
//         }

//         public override void Update(Player player, ref int buffIndex)
//         {
        
//             var BluePlayer = player.GetModPlayer<BlueFlamesPlayer>();

//             BluePlayer.IsRageActive = true;

//             if (BluePlayer.IsRageActive)
//             {
//                 // Apply Flash Fire Fist effects
//                 player.GetDamage(DamageClass.Melee) += 0.20f; // Increase melee damage by 20%
//                 player.moveSpeed += 1.5f; // Increase movement speed by 20%
//             }
            
            

           

            
//         }
//     }
// }