using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Engine
{
    public partial class EnginePlayer : ModPlayer, IQuirkResetter
    {
        public bool isEngineOn = false;
        public bool isBoosting = false;
        public int momentumTimer = 0;
        public int currentGear = 0;
        

        public void FullReset()
        {
            isEngineOn = false;
            momentumTimer = 0;
            currentGear = 0;
        }

        public override void PreUpdate()
        {
            isEngineOn = false;
            isBoosting = false;
        }

        public override void PostUpdateMiscEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (isEngineOn)
            {
                bool isRunning = (Player.controlLeft || Player.controlRight) && Math.Abs(Player.velocity.X) > 0.5f;

                
                if (isRunning)
                {
                    momentumTimer++;
                }
                else
                {
                    momentumTimer -= 5;
                    if (momentumTimer < 0) momentumTimer = 0;
                }

            
                if (momentumTimer > 600) currentGear = 5;      
                else if (momentumTimer > 420) currentGear = 4; 
                else if (momentumTimer > 240) currentGear = 3; 
                else if (momentumTimer > 120) currentGear = 2; 
                else if (momentumTimer > 30) currentGear = 1; 
                else currentGear = 0;

                
                if (currentGear == 2 && Main.rand.NextBool(5))
                    Dust.NewDust(Player.BottomLeft, Player.width, 10, DustID.Smoke, 0f, 0f, 100, default, 1.2f);
                
                
                if (currentGear == 3 && Main.rand.NextBool(4))
                    Dust.NewDust(Player.BottomLeft, Player.width, 10, DustID.Torch, 0f, 0f, 100, default, 1.5f);

                
                if (currentGear >= 4)
                {
                    
                    if (transPlayer.CurrentStage >= QuirkStage.Advanced)
                    {
                        if (currentGear == 4 && Main.rand.NextBool(3))
                        {
                            Dust.NewDust(Player.BottomLeft, Player.width, 10, DustID.BlueTorch, 0f, 0f, 100, default, 1.8f);
                        }

                        if (currentGear == 5)
                        {
                            
                            if (transPlayer.CurrentStage == QuirkStage.Final && Main.rand.NextBool(2))
                            {
                                int d = Dust.NewDust(Player.BottomLeft, Player.width, 10, DustID.Clentaminator_Cyan, 0f, 0f, 100, default, 2f);
                                Main.dust[d].velocity *= 1.5f; 
                            }
                           
                            else if (transPlayer.CurrentStage == QuirkStage.Advanced && Main.rand.NextBool(2))
                            {
                                Dust.NewDust(Player.BottomLeft, Player.width, 10, DustID.BlueTorch, 0f, 0f, 100, default, 2.2f);
                            }
                        }
                    }
                   
                    else 
                    {
                        if (Main.rand.NextBool(3))
                        {
                            Dust.NewDust(Player.BottomLeft, Player.width, 10, DustID.Torch, 0f, 0f, 100, default, 1.8f);
                        }
                    }
                }

                
                if (isBoosting && Main.rand.NextBool(1)) 
                {
                    
                    int boostDust = transPlayer.CurrentStage switch
                    {
                        QuirkStage.Final => DustID.Clentaminator_Cyan,
                        QuirkStage.Advanced => DustID.Clentaminator_Cyan,
                        QuirkStage.Intermediate => DustID.BlueTorch,
                        QuirkStage.Adequation => DustID.Torch,
                        _ => DustID.Smoke
                    };

                    float boostScale = 1.8f;
                    if (boostDust == DustID.Clentaminator_Cyan) boostScale = 2.2f; 

                    
                    int d1 = Dust.NewDust(Player.BottomLeft, 2, 10, boostDust, 0f, 0f, 100, default, boostScale);
                    Main.dust[d1].velocity *= 1.8f; 
                    Main.dust[d1].noGravity = true; 

                    int d2 = Dust.NewDust(Player.BottomRight - new Vector2(2, 0), 2, 10, boostDust, 0f, 0f, 100, default, boostScale);
                    Main.dust[d2].velocity *= 1.8f;
                    Main.dust[d2].noGravity = true;
                }

            }
            else
            {
                
                momentumTimer = 0;
                currentGear = 0;
            }
        }

        public override void PostUpdate()
        {
           
            }

       
        }
    }
