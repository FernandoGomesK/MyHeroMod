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
using rail;
using MyHeroMod.content.System;




namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer : ModPlayer, IQuirkResetter, IHeroDashModifier
    {

        public bool isAirForceOn = false;

        public bool isIronSolesOn = false;

        // Full Cowling
        public bool isFullCowlingBuffActive = false;


        // Fingers

        public int currentFingers = 10;
        public int MaxFingers = 10;
        public int fingerRegen = 0;
        public int fingerTimer = 450;

        // Parallel Processing
        public int ParallelProcessing = 0;
        public int MaxParallelProcessing = 0;

        // Activations
    
        private int ElectricSoundTimer = 0;

        public int ActivationTimer = 0;
        public int ActivationMaxTime = 40;
        public int percentage = 0;
        public int pendingPercentage = 0;
        public bool Activating = false; 
        

        public void FullReset()
        {
            currentFingers = 10;
            ParallelProcessing = 0;
            ElectricSoundTimer = 0;
            ActivationTimer = 0;
            ActivationMaxTime = 40;
            percentage = 0;
            pendingPercentage = 0;
            Activating = false; 
            isFullCowlingBuffActive = false;

        }

        public override void OnRespawn()
        {
            currentFingers = 10;
            ElectricSoundTimer = 0;
            ActivationTimer = 0;
            ActivationMaxTime = 40;
            percentage = 0;
            pendingPercentage = 0;
            Activating = false; 
            isFullCowlingBuffActive = false;
        }
        

        
        public override void PreUpdate()
        {

            if (!Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.OneForAll9th))
            {
                return; 
            } 
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

            if (currentFingers < MaxFingers)
            {
                fingerRegen++;
            }
            if (fingerRegen >= fingerTimer)
            {
                currentFingers++;
                Main.NewText("Finger Regenerated", Color.White);
                fingerRegen = 0;
            }
            else
            {
                fingerRegen = 0;
            }
        
        
                }
        private void ActivateFullCowling()
        {

            percentage = pendingPercentage;
        
            Player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 3600000);
            Main.NewText("ONE FOR ALL Full Cowling", Color.Cyan);
            CombatText.NewText(Player.getRect(), Color.Cyan, "Full Cowling!");
            
            
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
            if (!Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.OneForAll9th))
            {
                return; 
            }
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var ofaPlayer = Player.GetModPlayer<OneForAll9thPlayer>();

            UnlockQuirks();

            ParallelProcessing = 0;

            if (transPlayer.SelectedQuirk == QuirkType.OneForAll9th)
            {
                
            
            if (Player.HasBuff(ModContent.BuffType<FloatBuff>()))
            {
                ParallelProcessing++;
            }
            if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
            {
                ParallelProcessing++;
            }
            if (Player.HasBuff(ModContent.BuffType<DangerSenseBuff>()))
            {
                ParallelProcessing++;
            }
            }


            
            
            if (transPlayer.SelectedQuirk == QuirkType.OneForAll9th)
            {
                if (transPlayer.CurrentStage == QuirkStage.Initial) 
                    MaxParallelProcessing = 0; 
                else if (transPlayer.CurrentStage == QuirkStage.Adequation) 
                    MaxParallelProcessing = 1; 
                else if (transPlayer.CurrentStage == QuirkStage.Intermediate) 
                    MaxParallelProcessing = 2; 
                else if (transPlayer.CurrentStage == QuirkStage.Advanced) 
                    MaxParallelProcessing = 4; 
                else if (transPlayer.CurrentStage >= QuirkStage.Final) 
                    MaxParallelProcessing = 6; 
            }
            else
            {
                MaxParallelProcessing = 0;
            }
        

            if (ParallelProcessing > 0)
            {
                Player.AddBuff(ModContent.BuffType<ParallelProcessingBuff>(), 2);
            }

       
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

            if (currentFingers < MaxFingers)
            {
                Player.AddBuff(ModContent.BuffType<FingersBuff>(), 2);
            }



            
        }
        

    }
}

