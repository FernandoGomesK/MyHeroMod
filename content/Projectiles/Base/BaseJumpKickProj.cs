// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using Terraria.Audio;

// namespace MyHeroMod.content.Projectiles.Base
// { 
    
//     public abstract class BaseJumpKickProj : ModProjectile
//     {
//         public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj"; 

        
//         protected virtual float DashSpeed => 25f;       
//         protected virtual float JumpPower => -15f;     
//         protected virtual int HoverFrames => 15;        
//         protected virtual int DustType => DustID.Smoke; 

//         public override void SetDefaults()
//         {
//             Projectile.width = 80; 
//             Projectile.height = 80;
//             Projectile.friendly = true;
//             Projectile.hostile = false;
//             Projectile.tileCollide = true; 
//             Projectile.penetrate = 1; 
//             Projectile.timeLeft = 120; 
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
//             player.heldProj = Projectile.whoAmI;
            
//             // Jump 

//             if (Projectile.ai[0] < HoverFrames)
//             {
//                 Projectile.ai[0]++;

//                 player.velocity.Y = JumpPower; 
//                 player.velocity.X *= 0.9f; 
//                 Projectile.width = 5;
//                 Projectile.height = 5;
                
                
                
//                 SpawnHoverDust(player); 
//             }

//             // Aiming portion of the code

//             else if (Projectile.ai[0] == HoverFrames)
//             {
//                 Projectile.ai[0]++;
//                 Projectile.width = 80;
//                 Projectile.height = 80;
                
//                 Vector2 dashDirection = Main.MouseWorld - player.Center;
//                 dashDirection.Normalize();
                
//                 Projectile.velocity = dashDirection * DashSpeed;
//                 player.velocity = Projectile.velocity;

//                 SoundEngine.PlaySound(SoundID.Item14, player.position); 
//             }

//             // Reset
            
//             else
//             {
//                 player.velocity = Projectile.velocity;
//                 player.fullRotation = (player.velocity.ToRotation() + MathHelper.PiOver2) + MathHelper.Pi;
//                 player.fullRotationOrigin = player.Size / 2;

//                 SpawnDashDust(player); 
//             }
//         }

//         public override void OnKill(int timeLeft)
//         {
//             Player player = Main.player[Projectile.owner];
//             player.velocity = Vector2.Zero;
//             player.fullRotation = 0f; 
//             SoundEngine.PlaySound(SoundID.Item62, Projectile.position); 

//             SpawnExplosionDust(Projectile.Center);
//         }

//         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => Projectile.Kill();
//         public override bool OnTileCollide(Vector2 oldVelocity) => true;

        
//         public virtual void SpawnHoverDust(Player player) { }
//         public virtual void SpawnDashDust(Player player) { }
//         public virtual void SpawnExplosionDust(Vector2 position) { }
//     }
// }