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
        }

        public override void PostUpdateMiscEffects()
        {
            if (isEngineOn)
            {
                bool isRunning = (Player.controlLeft || Player.controlRight) && Math.Abs(Player.velocity.X) > 0.5f;

                if (isRunning)
                {
                    momentumTimer++;
                }
                else
                {
                    momentumTimer -= 15;
                    if (momentumTimer < 0)
                        momentumTimer = 0;
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
                
                if (currentGear == 4 && Main.rand.NextBool(3))
                    Dust.NewDust(Player.BottomLeft, Player.width, 10, DustID.BlueTorch, 0f, 0f, 100, default, 1.8f);
                
                if (currentGear == 5 && Main.rand.NextBool(2))
                {
                    int d = Dust.NewDust(Player.BottomLeft, Player.width, 10, DustID.Clentaminator_Cyan, 0f, 0f, 100, default, 2f);
                    Main.dust[d].velocity *= 1.5f; 
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
