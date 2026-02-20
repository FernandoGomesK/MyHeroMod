using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th.Buffs;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using Terraria.Audio;
using System.Collections.Generic;
using MyHeroMod.content.System.BasePlayer;




namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer : ModPlayer
    {


        public int FaJinCharges = 0;
        public int MaxFaJinCharges = 5;
        public bool FaJinStored = false;

        // Gearshift
        public bool isGearshiftActive = false;
        public bool isGearshiftBuffActive = false;
        public int GearshiftTimer = 0;
        public int GearshiftMaxTime = 6000;
        // Gearshift Buff   
        public bool GearActivation = false;

        // Full Cowling
        public bool isFullCowlingBuffActive = false;

        // Danger Sense
        public bool isDangerSenseActive = false;
        // Smoke Screen
        public bool isSmokeScreenActive = false;

        // Float
        public bool isFloatActive = false;

        // Fingers

        public int Fingers = 10;

        // Parallel Processing
        public int ParallelProcessing = 0;
        public int MaxParallelProcessing = 0;
        


        private int ElectricSoundTimer = 0;

        public int ActivationTimer = 0;
        public int ActivationMaxTime = 40;
        

        

        public override void OnRespawn()
        {
            // Fingers = 10;
            // ElectricSoundTimer = 0;
            // ActivationTimer = 0;
            // GearshiftTimer = 0;
            
        }
        public override void PreUpdate()
        {
        }
            

        
            // if (ActivationTimer > 0)
            // {
            //     ActivationTimer++;
            //     Player.velocity *= 0.6f; 

                

                
                // if (ActivationTimer >= ActivationMaxTime)
                // {
                    
                
                
                    // if (GearActivation)
                    // {
                    //     isGearshiftActive = true;
                    //     GearActivation = false;
                    //     GearshiftTimer = 0;

                    //     Main.NewText("ONE FOR ALL 2ND - GEARSHIFT: TRANSMISSION !", Color.Cyan);
                    //     CombatText.NewText(Player.getRect(), Color.Cyan, "SECOND GEAR");
                    //     SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/GearShiftSound"), Player.position);

                    //     // Explosão de partículas
                    //     for (int i = 0; i < 20; i++)
                    //     {
                    //         Vector2 speed = Main.rand.NextVector2Circular(5f, 5f);
                    //         Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, speed.X, speed.Y, 0, Color.Cyan, 2f);
                    //     }
                    // }
                    // ActivationTimer = 0;

                    
        //         }
        //     }
        // }

        


        public override void ResetEffects()
        {
            
        //     var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            
        //     ParallelProcessing = 0;

            
        //     if (isFloatActive) ParallelProcessing++;
        //     if (isDangerSenseActive) ParallelProcessing++;
        //     if (isGearshiftActive) ParallelProcessing++;
        //     if (isSmokeScreenActive) ParallelProcessing++;

            
        //     if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th)
        //     {
        //         if (mainPlayer.CurrentStage == QuirkStage.Initial) 
        //             MaxParallelProcessing = 0; 
        //         else if (mainPlayer.CurrentStage == QuirkStage.Adequation) 
        //             MaxParallelProcessing = 1; 
        //         else if (mainPlayer.CurrentStage == QuirkStage.Intermediate) 
        //             MaxParallelProcessing = 2; 
        //         else if (mainPlayer.CurrentStage == QuirkStage.Advanced) 
        //             MaxParallelProcessing = 4; 
        //         else if (mainPlayer.CurrentStage >= QuirkStage.Final) 
        //             MaxParallelProcessing = 6; 
        //     }
        //     else
        //     {
        //         MaxParallelProcessing = 0;
        //     }
        // }
        
        // public override void PostUpdateEquips()
        // {
        //     var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
        //     var player = Player.GetModPlayer<OneForAll9thPlayer>();

        //     if (FaJinCharges >= MaxFaJinCharges)
        //     {
        //         FaJinStored = true;
        //         Player.AddBuff(ModContent.BuffType<FaJinBuff>(), 2);
        //     }
        //     else
        //     {
        //         FaJinStored = false;
        //     }

        //     if (ParallelProcessing > 0)
        //     {
        //         Player.AddBuff(ModContent.BuffType<ParallelProcessingBuff>(), 2);
        //     }

        //     if (isDangerSenseActive) Player.AddBuff(ModContent.BuffType<DangerSenseBuff>(), 2);

        //     if (isSmokeScreenActive)
        //     {
        //         Dust.NewDust(Player.position, Player.width, Player.height, DustID.Smoke, 0f, 0f, 100, Color.MediumPurple, 6.0f);
        //     }
            

            bool hasAnyFullCowling = Player.HasBuff(ModContent.BuffType<FullCowlingBuff5>()) || 
                            Player.HasBuff(ModContent.BuffType<FullCowlingBuff10>()) || 
                            Player.HasBuff(ModContent.BuffType<FullCowlingBuff45>());

            if (hasAnyFullCowling)
            {
                isFullCowlingBuffActive = true;
                HandleFullCowlingEffects();
                Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
                ElectricSoundTimer++;
            }
            else
            {
                isFullCowlingBuffActive = false;
            }



            
        }
        

    }
}

