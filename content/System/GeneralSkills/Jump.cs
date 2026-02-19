// using Terraria;
// using Microsoft.Xna.Framework;
// using Terraria.Audio;
// using Terraria.ID;
// using MyHeroMod.content.Quirks.FaJin;
// using MyHeroMod.content.Quirks.Gearshift;
// using MyHeroMod.content.Quirks.OFA9th;

// namespace MyHeroMod.content.System
// {
//     public class JumpSkill : QuirkSkill
//     {
//         public override string Name => "Jump";
//         public override string Description => "Do a Jump Hicher than a normal human.";
//         public override string IconPath => "MyHeroMod/Assets/Skills/Jump";
//         public override int BaseCooldown => 120;
       
//         public override QuirkType RequiredQuirk => QuirkType.Quirkless;
//         public override QuirkStage RequiredStage => QuirkStage.Initial;
        
//         public override bool IsDefaultSkill => true;
//         public override bool IsBaseQuirk => false;
        

//         public override bool CheckUnlock(TransformationPlayer player)
//         {
//             return true; 
//         }   
    

//         public override void OnUse(Player player)
//         {
//             var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
//             var fajinPlayer = player.GetModPlayer<FajinPlayer>();
//             var gearshiftPlayer = player.GetModPlayer<GearshiftPlayer>();
//             var transPlayer = player.GetModPlayer<TransformationPlayer>();
//             float speed = 14f;
//             bool isEnhanced = false;

//             if (gearshiftPlayer.isGearshiftBuffActive)
//             {
//                 ApplyDashMovement(player);
//                 return;
//             }
//             else if (fajinPlayer.FaJinStored)
//             {
//                 speed = 25f;
//                 isEnhanced = true;
//                 fajinPlayer.FaJinCharges = 0; 
//                 SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1"), player.position);

//                 executeJump(player, speed, isEnhanced);
//             }          
//             else if (transPlayer.SelectedQuirk == QuirkType.FaJin)
//                 {
//                     fajinPlayer.ChargeFajin(); 
//                     SoundEngine.PlaySound(SoundID.Item14, player.position);
//                     executeJump(player, speed, isEnhanced);
//                 }
//             else 
//             {
                
//                 SoundEngine.PlaySound(SoundID.Item14, player.position);
//                 executeJump(player, speed, isEnhanced);
//             }

             
//         }

//         private void executeJump(Player player, float speed, bool isEnhanced)
//         {
//             Vector2 dashDirection = Main.MouseWorld - player.Center;
//             if (dashDirection != Vector2.Zero)
//             {
//                 dashDirection.Normalize();
//                 player.velocity = dashDirection * speed;
//             }
//             // 3.Efeitos Visuais (VFX)
//             ApplyFajinVfx(player, isEnhanced);
            
//             player.SetImmuneTimeForAllTypes(10);
//         }

//         private void ApplyDashMovement(Player player)
//         {
//             Vector2 targetPos = Main.MouseWorld;
//                 Vector2 dir = targetPos - player.Center;
//                 float distance = dir.Length();
//                 SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.15f }, player.position);
//                 float maxDist = 600f;
//                 if (distance > maxDist)
//                 {
//                     dir.Normalize();
//                     dir *= maxDist;
//                     distance = maxDist;
//                 }

            
//                 Vector2 safePos = player.Center;
//                 float stepSize = 16f; 
//                 bool hitWall = false;

//                 for (float i = 0; i < distance; i += stepSize)
//                 {
//                     Vector2 checkPos = player.Center + Vector2.Normalize(dir) * i;
                    
                    
//                     if (Collision.SolidCollision(checkPos - new Vector2(player.width/2, player.height/2), player.width, player.height))
//                     {
//                         hitWall = true;
//                         break; 
//                     }
//                     safePos = checkPos; 
//                 }

//                 Vector2 startPos = player.Center;
//                 int dustCount = (int)(Vector2.Distance(startPos, safePos) / 5); // 1 partícula a cada 5 pixels
//                 for (int i = 0; i < dustCount; i++)
//                 {
//                     Vector2 dustPos = Vector2.Lerp(startPos, safePos, (float)i / dustCount);
//                     int d = Dust.NewDust(dustPos, 0, 0, DustID.Electric, 0, 0, 100, Color.Cyan, 1.5f);
//                     Main.dust[d].noGravity = true;
//                     Main.dust[d].velocity *= 0.5f;
//                 }

                
//                 player.Center = safePos;
//                 player.velocity = Vector2.Zero; 
//                 if (hitWall) 
//                 {
//                     player.velocity = -Vector2.Normalize(dir) * 2f; 
//                 }
//         }

//         private void dashvfx(Player player)
//         {
//             SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.15f }, player.position);
//                 for (int i = 0; i < 4; i++)
//                 {
//                     Vector2 dustPosition = player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
//                     Dust.NewDust(dustPosition, 0, 0, DustID.Smoke, player.velocity.X * -0.5f, player.velocity.Y * -0.5f);
//                 }
//                 for (int i = 0; i < 15; i++)
//                 {
//                     Vector2 dustPosition = player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
//                     Dust.NewDust(dustPosition, 0, 0, DustID.BlueTorch, player.velocity.X * -1f, player.velocity.Y * -1f, 0, default, 6f);
//                 }
//         }

//         private void ApplyFajinVfx(Player player, bool enhanced)
//         {
//             int dustCount = enhanced ? 20 : 10;
//             int type = enhanced ? DustID.RedTorch : DustID.Cloud;
//             float scale = enhanced ? 2f : 1.5f;

//             for (int i = 0; i < dustCount; i++)
//             {
//                 Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, type, 0f, 0f, 100, default, scale);
//                 dust.velocity *= 0.5f;
//                 if (enhanced) dust.noGravity = true;
//             }
//         }
//     }
// }