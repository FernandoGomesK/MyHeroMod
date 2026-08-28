using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Engine
{
    public partial class EnginePlayer : ModPlayer, IQuirkResetter, IDashModifier
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

       
        public override void ResetEffects()
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
                    if (momentumTimer > 650) momentumTimer = 650;
                }
                else
                {
                    if (Main.GameUpdateCount % 12 == 0)
                {
                    momentumTimer -= 5;
                }
                    if (momentumTimer < 0) momentumTimer = 0;
                }

            
                if (momentumTimer > 600) currentGear = 5;      
                else if (momentumTimer > 420) currentGear = 4; 
                else if (momentumTimer > 240) currentGear = 3; 
                else if (momentumTimer > 120) currentGear = 2; 
                else if (momentumTimer > 30) currentGear = 1; 
                else currentGear = 0;

                SpawnEngineDust();


            }
            else
            {
                
                momentumTimer = 0;
                currentGear = 0;
            }
        }

        
        public void SpawnEngineDust(float scaleMultiplier = 1f)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            Vector2 exhaustOffset = transPlayer.CurrentVariant == QuirkVariant.Variant1 ? new Vector2(0f, -24f) : Vector2.Zero;

        
            if (currentGear == 2 && Main.rand.NextBool(5))
                Dust.NewDust(Player.BottomLeft + exhaustOffset, Player.width, 10, DustID.Smoke, 0f, 0f, 100, default, 1.2f * scaleMultiplier);
            
            if (currentGear == 3 && Main.rand.NextBool(4))
                Dust.NewDust(Player.BottomLeft + exhaustOffset, Player.width, 10, DustID.Torch, 0f, 0f, 100, default, 1.5f * scaleMultiplier);

            if (currentGear >= 4)
            {
                if (transPlayer.CurrentStage >= QuirkStage.Advanced)
                {
                    if (currentGear == 4 && Main.rand.NextBool(3))
                        Dust.NewDust(Player.BottomLeft + exhaustOffset, Player.width, 10, DustID.BlueTorch, 0f, 0f, 100, default, 1.8f * scaleMultiplier);

                    if (currentGear == 5)
                    {
                        if (transPlayer.CurrentStage == QuirkStage.Final && Main.rand.NextBool(2))
                        {
                            int d = Dust.NewDust(Player.BottomLeft + exhaustOffset, Player.width, 10, DustID.Clentaminator_Cyan, 0f, 0f, 100, default, 2f * scaleMultiplier);
                            Main.dust[d].velocity *= 1.5f; 
                        }
                        else if (transPlayer.CurrentStage == QuirkStage.Advanced && Main.rand.NextBool(2))
                        {
                            Dust.NewDust(Player.BottomLeft + exhaustOffset, Player.width, 10, DustID.BlueTorch, 0f, 0f, 100, default, 2.2f * scaleMultiplier);
                        }
                    }
                }
                else 
                {
                    if (Main.rand.NextBool(3))
                        Dust.NewDust(Player.BottomLeft + exhaustOffset, Player.width, 10, DustID.Torch, 0f, 0f, 100, default, 1.8f * scaleMultiplier);
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

                float boostScale = (boostDust == DustID.Clentaminator_Cyan ? 2.2f : 1.8f) * scaleMultiplier;

                int d1 = Dust.NewDust(Player.BottomLeft + exhaustOffset, 2, 10, boostDust, 0f, 0f, 100, default, boostScale);
                Main.dust[d1].velocity *= 1.8f; 
                Main.dust[d1].noGravity = true; 

                int d2 = Dust.NewDust(Player.BottomRight + exhaustOffset - new Vector2(2, 0), 2, 10, boostDust, 0f, 0f, 100, default, boostScale);
                Main.dust[d2].velocity *= 1.8f;
                Main.dust[d2].noGravity = true;
            }
        }

        public override void PostUpdate()
        {
           
            }
            public override void FrameEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

         
            if (transPlayer.HasActiveQuirk(QuirkType.Engine) && transPlayer.CurrentVariant == QuirkVariant.Variant1)
            {
            
                Player.handon = EquipLoader.GetEquipSlot(Mod, "TenseiExhaustArms", EquipType.HandsOn);
                Player.handoff = EquipLoader.GetEquipSlot(Mod, "TenseiExhaustArms", EquipType.HandsOff);
            }
        }

       
        }
    }
