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

<<<<<<< HEAD
        public bool isAirForceOn = false;
=======

        public bool isIronSolesOn = false;
>>>>>>> Testbranch
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
        public int percentage = 0;
        public int pendingPercentage = 0;
        public bool Activating = false; 
        

        

        public override void OnRespawn()
        {
            // Fingers = 10;
            // ElectricSoundTimer = 0;
            // ActivationTimer = 0;
            // GearshiftTimer = 0;
            
        }
        

        
        public override void PreUpdate()
        {
            if (Activating)
            {
                ActivationTimer++;
                Player.velocity *= 0.6f; 

                if (Main.rand.NextBool(2))
                {
                    Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, Color.Green, 0.5f);
                    d.noGravity = true;
                    d.velocity *= 0.5f;   
                }

                if (ActivationTimer >= ActivationMaxTime)
                {   
                    ActivateFullCowling();
                    Activating = false;
                    ActivationTimer = 0;
                }

                
        }
        
        
                }
        private void ActivateFullCowling()
        {
            var transformPlayer = Player.GetModPlayer<TransformationPlayer>();

            percentage = pendingPercentage;
        
            Player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 3600000);
            Main.NewText("ONE FOR ALL Full Cowling", Color.Cyan);
            CombatText.NewText(Player.getRect(), Color.Cyan, "Full Cowling!");
            
            

            // Explosão de partículas
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, speed.X, speed.Y, 0, Color.Green, 2f);
            }
        }


        public List<QuirkType> InternalQuirks = new List<QuirkType>();
        
            

        public void UnlockQuirks(){
        var transPlayer = Player.GetModPlayer<TransformationPlayer>();


        if (transPlayer.SelectedQuirk == QuirkType.OneForAll9th)
    {
        // Evolução baseada no estágio (Stage)
        if (transPlayer.CurrentStage >= QuirkStage.Initial)
            InternalQuirks.Add(QuirkType.OneForAll9th); 

        if (transPlayer.CurrentStage >= QuirkStage.Adequation)
            InternalQuirks.Add(QuirkType.BlackWhip);

        if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
            InternalQuirks.Add(QuirkType.DangerSense);

        if (transPlayer.CurrentStage >= QuirkStage.Advanced)
        {
            InternalQuirks.Add(QuirkType.Float);
            InternalQuirks.Add(QuirkType.SmokeScreen);
        }

        if (transPlayer.CurrentStage >= QuirkStage.Final)
        {
            InternalQuirks.Add(QuirkType.FaJin);
            InternalQuirks.Add(QuirkType.Gearshift);
        }
    }

    
        }

        public bool HasInternalQuirk(QuirkType type)
{
    return InternalQuirks.Contains(type);
}
        

        public override void ResetEffects()
        {

            UnlockQuirks();

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
        
       
        //     if (ParallelProcessing > 0)
        //     {
        //         Player.AddBuff(ModContent.BuffType<ParallelProcessingBuff>(), 2);
        //     }

        //    
            bool hasAnyFullCowling = Player.HasBuff(ModContent.BuffType<FullCowlingBuff>());

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

