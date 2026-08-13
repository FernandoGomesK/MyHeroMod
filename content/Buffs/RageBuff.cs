// using Terraria;
// using Terraria.ModLoader;
// using Microsoft.Xna.Framework;
// using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;

// namespace MyHeroMod.content.Buffs
// {
//     public class BlueRage : ModBuff
//     {
        
        
//         public override void SetStaticDefaults()
//         {
//             Main.buffNoSave[Type] = true;
//             Main.buffNoTimeDisplay[Type] = true;
//         }

//         public override void Update(Player player, ref int buffIndex)
//         {
        
//             var BluePlayer = player.GetModPlayer<BlueflamePlayer>();

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