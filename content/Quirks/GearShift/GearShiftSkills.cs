using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;

using MyHeroMod.content.System;
using Microsoft.Xna.Framework.Graphics;
using KhacesCore.Content.System.Interfaces;


namespace MyHeroMod.content.Quirks.Gearshift
{
    
    public partial class GearshiftPlayer : ModPlayer, IDashModifier, IQuirkResetter, IHeroPunchModifier
    {


        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash ,ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.Gearshift))
            return;
            
            if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>())) 
            {
                hideNormalDash = true;
                explosionColor = Color.Cyan; 
                dustType = DustID.BlueTorch;
            }
        }

        public void ModifyPunch(ref float projSpeed, ref int baseDamage, ref bool isSuperPunch, ref int numberOfPunches)
        {
            if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
    {
       projSpeed = 25;
            baseDamage = 20;
            isSuperPunch = false;
            numberOfPunches = 5;
    }

        }
    }
}
        
        //         Vector2 targetPos = Main.MouseWorld;
        //         Vector2 dir = targetPos - Player.Center;
        //         float distance = dir.Length();
                
        //         float maxDist = 600f;
        //         if (distance > maxDist)
        //         {
        //             dir.Normalize();
        //             dir *= maxDist;
        //             distance = maxDist;
        //         }

            
        //         Vector2 safePos = Player.Center;
        //         float stepSize = 16f; 
        //         bool hitWall = false;

        //         for (float i = 0; i < distance; i += stepSize)
        //         {
        //             Vector2 checkPos = Player.Center + Vector2.Normalize(dir) * i;
                    
                    
        //             if (Collision.SolidCollision(checkPos - new Vector2(Player.width/2, Player.height/2), Player.width, Player.height))
        //             {
        //                 hitWall = true;
        //                 break; 
        //             }
        //             safePos = checkPos; 
        //         }

        //         Vector2 startPos = Player.Center;
        //         int dustCount = (int)(Vector2.Distance(startPos, safePos) / 5); // 1 partícula a cada 5 pixels
        //         for (int i = 0; i < dustCount; i++)
        //         {
        //             Vector2 dustPos = Vector2.Lerp(startPos, safePos, (float)i / dustCount);
        //             int d = Dust.NewDust(dustPos, 0, 0, DustID.Electric, 0, 0, 100, Color.Cyan, 1.5f);
        //             Main.dust[d].noGravity = true;
        //             Main.dust[d].velocity *= 0.5f;
        //         }

                
        //         Player.Center = safePos;
        //         Player.velocity = Vector2.Zero; 
        //         if (hitWall) 
        //         {
        //             Player.velocity = -Vector2.Normalize(dir) * 2f; 
        //         }

        //         dashvfx(); 
        //         SetCooldown(skill, 40); 
        //     }
        //     break;
        //     }
        // }

       

        // private void dashvfx()
        // {
        //     SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.15f }, Player.position);
        //         for (int i = 0; i < 4; i++)
        //         {
        //             Vector2 dustPosition = Player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
        //             Dust.NewDust(dustPosition, 0, 0, DustID.Smoke, Player.velocity.X * -0.5f, Player.velocity.Y * -0.5f);
        //         }
        //         for (int i = 0; i < 15; i++)
        //         {
        //             Vector2 dustPosition = Player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
        //             Dust.NewDust(dustPosition, 0, 0, DustID.BlueTorch, Player.velocity.X * -1f, Player.velocity.Y * -1f, 0, default, 6f);
        //         }
        // }

      