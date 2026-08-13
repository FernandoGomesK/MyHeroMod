// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.ModLoader;
// using Terraria.DataStructures;
// using Terraria.ID;
// using Terraria.Audio;

// namespace MyHeroMod.content.Quirks.HellFlames
// {
//     public partial class HellFlamesPlayer : ModPlayer
//     {
        
//          private void UpdateFlyingDust()
//         {
//             bool isFlying = (Player.velocity.Y != 0) && (Player.wingTime > 0 || Player.rocketDelay > 0) && !Player.mount.Active;
            
//             if (isFlying)
//             {
//                 if (Main.rand.NextBool(2)) 
//                 {
//                     int dustFire = Dust.NewDust(Player.position + new Vector2(-5, Player.height - 10), Player.width / 2, 10, DustID.Torch, 0, 2f, 100, default, 1.5f);
//                     Main.dust[dustFire].noGravity = true;
//                     Main.dust[dustFire].velocity *= 0.5f; 
//                 }
                
//                 if (Main.rand.NextBool(2))
//                 {
//                     int dustIce = Dust.NewDust(Player.position + new Vector2(Player.width / 2, Player.height - 10), Player.width / 2, 10, DustID.Torch, 0, 2f, 100, default, 1.5f);
//                     Main.dust[dustIce].noGravity = true;
//                     Main.dust[dustIce].velocity *= 0.5f;
//                 }
//             }
//         }

//     }
//     }
