// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using MyHeroMod.content.System; 

// namespace MyHeroMod.content.Projectiles.Base
// { 
//     public abstract class BaseChannelingProj : ModProjectile
//     {
//         public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj"; 

        
//         protected virtual int ChannelTime => 240;       

//         public override void SetDefaults()
//         {
//             Projectile.width = 32;
//             Projectile.height = 32;
//             Projectile.friendly = true;
//             Projectile.hostile = false;
//             Projectile.tileCollide = false; 
//             Projectile.penetrate = -1;
//             Projectile.alpha = 255; 
//         }

//         public override void AI()
//         {
//             Player player = Main.player[Projectile.owner];

            
//             if (player.dead || !player.active)
//             {
//                 Projectile.Kill();
//                 return;
//             }

            
//             Projectile.Center = player.Center;

        
//             bool isHolding = KeybindSystem.SkillSlot1.Current || KeybindSystem.SkillSlot2.Current || KeybindSystem.SkillSlot3.Current || KeybindSystem.SkillSlot4.Current;

            
//             if (!isHolding)
//             {
//                 OnChargeCancelled(player);
//                 Projectile.Kill();
//                 return;
//             }

            
//             SpawnChargingDust(player);

        
//             Projectile.ai[0]++;

            
//             if (Projectile.ai[0] >= ChannelTime)
//             {
//                 OnChargeComplete(player); 
//                 Projectile.Kill();        
//             }
//         }

        
//         public virtual void SpawnChargingDust(Player player) { }
//         public virtual void OnChargeCancelled(Player player) { } 
//         public abstract void OnChargeComplete(Player player);
//     }
// }