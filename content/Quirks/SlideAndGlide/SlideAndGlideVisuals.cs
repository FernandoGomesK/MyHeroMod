using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using Terraria.DataStructures;


namespace MyHeroMod.content.Quirks.SlideAndGlide
{
    public partial class SlideAndGlidePlayer : ModPlayer
    {
        
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
       
        if (Player.HasBuff(ModContent.BuffType<SlideAndGlideBuff>()))
        {
        
        
        
        
        
        Player.armorEffectDrawShadow = true; 

        bool isMoving = Player.velocity.X != 0f;

        bool isGrounded = Player.velocity.Y == 0f;

        bool goingRight = Player.velocity.X > 0f;
        bool goingLeft = Player.velocity.X < 0f;


                if (isGrounded && isMoving)
                {

                    if (goingRight)
                    {
                        if (Main.rand.NextBool(2))
                    {
                        int circle2 = Dust.NewDust(Player.position + new Vector2(-8, Player.height - 6), Player.width / 2, 10, DustID.IceTorch, 0, 2f, 100, default, 0.5f);
                        Main.dust[circle2].noGravity = true;
                        Main.dust[circle2].velocity *= 0.5f;
                    }

                    if (Main.rand.NextBool(2))
                                {
                                    int dust2 = Dust.NewDust(Player.position + new Vector2(-8 , Player.height - 6), Player.width / 2, 10, DustID.SteampunkSteam, 0, 2f, 100, default, 1.5f);
                                    Main.dust[dust2].noGravity = true;
                                    Main.dust[dust2].velocity *= 1f;
                                }
                    }
                    else
                    {
                        if (Main.rand.NextBool(2))
                    {
                        int circle = Dust.NewDust(Player.position + new Vector2((Player.width / 2) + 6, Player.height - 6), Player.width / 2, 10, DustID.IceTorch, 0, 2f, 100, default, 0.5f);
                        Main.dust[circle].noGravity = true;
                        Main.dust[circle].velocity *= 0.5f;
                    }

        if (Main.rand.NextBool(2))
                    {
                        int dust = Dust.NewDust(Player.position + new Vector2((Player.width / 2) + 6, Player.height - 6), Player.width / 2, 10, DustID.SteampunkSteam, 0, 2f, 100, default, 1.5f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity *= 1f;
                    }
                    }

                   

        
        
        
        }

        

        

    
        
}}}}