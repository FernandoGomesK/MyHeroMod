using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using Terraria.Audio;
using System.Collections.Generic;
using MyHeroMod.content.System;
using KhacesCore.Content.System.Interfaces;
using KhacesCore.Content.System;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhip;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackChain;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhipStun;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.PinpointFocus;
using MyHeroMod.content.System.Interfaces;




namespace MyHeroMod.content.Quirks.OFA9th
{
    // ========================================= Main ===============================================================================
    public partial class OneForAll9thPlayer : ModPlayer, IQuirkResetter, IDashModifier, IStrainSource
    {

        // ========================================= isQuirkless =======================================================================

        public bool isQuirkless = false;

        public int becomeQuirklessTimer = 1200;


        // ============================ Strain ==================================

         public int StrainPenaltyPerSecond { get; set; }

       public void AddStrain(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

           
            if (isQuirkless)
            {
                becomeQuirklessTimer -= amount;

                if (becomeQuirklessTimer <= 0)
                {
                    

                    
                    if (transPlayer.ActiveQuirks.Contains(QuirkType.OneForAll9th))
                    {
                        
                        transPlayer.ActiveQuirks.Remove(QuirkType.OneForAll9th);
                        
                       
                        Player.ClearBuff(ModContent.BuffType<FullCowlingBuff>());
                        FullReset();

                    }

                    becomeQuirklessTimer = 1200;
                    isQuirkless = false;
                }
                
            
                return;
            }

            
            transPlayer.currentStrain += amount;

            if (transPlayer.currentStrain <= 0)
            {
                transPlayer.currentStrain = 0;
            }
            else if (transPlayer.currentStrain >= transPlayer.maxStrain)
            {
                transPlayer.currentStrain = transPlayer.maxStrain;
                Player.ClearBuff(ModContent.BuffType<FullCowlingBuff>()); 
            }
        }

        // ======================= Support Items ===========================================================

        
        public bool isAirForceOn = false;

        public bool isIronSolesOn = false;
        public bool isMidGauntletsOn = false;

        // Full Cowling
        public bool isFullCowlingBuffActive = false;


        // ================================== Fingers =====================================================

        public int currentFingers = 10;
        public int MaxFingers = 10;
        public int fingerRegen = 0;
        public int fingerTimer = 800;

        // ============================================ Parallel Processing ======================================================
        public int ParallelProcessing = 0;
        public int MaxParallelProcessing = 0;

        // Activations
    
        private int ElectricSoundTimer = 0;

        
        public int percentage = 0;

        public bool hasTakenDashDamage = false;
        
        // ========================================== reset ========================================================================
        
        public void FullReset()
        {
            currentFingers = 10;
            ParallelProcessing = 0;
            ElectricSoundTimer = 0;
            percentage = 0;
            isFullCowlingBuffActive = false;
            Player.ClearBuff(ModContent.BuffType<FullCowlingBuff>());
            StrainPenaltyPerSecond = 0;

        }

        public override void OnRespawn()
        {
            currentFingers = 10;
            ElectricSoundTimer = 0;
            percentage = 0;
            isFullCowlingBuffActive = false;
            StrainPenaltyPerSecond = 0;
        }
        
        // ==================================================================================================================
        public override void PreUpdate()
        {

            if (!Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.OneForAll9th))
            {
                return; 
            } 

            if (currentFingers < MaxFingers)
            {
                fingerRegen++;
                if (fingerRegen >= fingerTimer)
                {
                    currentFingers++;
                    Main.NewText("Finger Regenerated", Color.White);
                    fingerRegen = 0; 
                }
            }
            else
            {
                fingerRegen = 0;
            }
        }
                
        public List<QuirkType> InternalQuirks = new List<QuirkType>();
        

        public bool HasInternalQuirk(QuirkType type)
        {
            return InternalQuirks.Contains(type);
        }
                

        public override void ResetEffects()
        {
            
            isFullCowlingBuffActive = false;
            ParallelProcessing = 0;
            isMidGauntletsOn = false;
            isAirForceOn = false;  
            isIronSolesOn = false;


            var dashPlayer = Player.GetModPlayer<DashPlayer>();
            
            if (!dashPlayer.IsDashing) 
            {
                hasTakenDashDamage = false;
            }

            
            if (!Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.OneForAll9th))
            {
                return; 
            }
            
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var ofaPlayer = Player.GetModPlayer<OneForAll9thPlayer>();

            UnlockQuirks();

            if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
            {
                if (Player.HasBuff(ModContent.BuffType<FloatBuff>())) { ParallelProcessing++; }
                if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>())) { ParallelProcessing++; }
                if (Player.HasBuff(ModContent.BuffType<DangerSenseBuff>())) { ParallelProcessing++; }
                if (Player.HasBuff(ModContent.BuffType<SmokescreenBuff>())) { ParallelProcessing++; }
                if (Player.HasBuff(ModContent.BuffType<FaJinActiveBuff>())) { ParallelProcessing++; }
                if (Player.HasBuff(ModContent.BuffType<FullCowlingBuff>())) { ParallelProcessing++; }
                if (Player.HasBuff(ModContent.BuffType<OverlayBuff>())) {ParallelProcessing++;} 
                if (Player.HasBuff(ModContent.BuffType<OverlayBuff>()) && Player.HasBuff(ModContent.BuffType<GearshiftRecoil>())) {ParallelProcessing++;} 
                         
            }
            
            if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
            {
                if (transPlayer.CurrentStage == QuirkStage.Initial) MaxParallelProcessing = 0; 
                else if (transPlayer.CurrentStage == QuirkStage.Adequation) MaxParallelProcessing = 1; 
                else if (transPlayer.CurrentStage == QuirkStage.Intermediate) MaxParallelProcessing = 2; 
                else if (transPlayer.CurrentStage == QuirkStage.Advanced) MaxParallelProcessing = 4; 
                else if (transPlayer.CurrentStage >= QuirkStage.Final) MaxParallelProcessing = 6; 
            }
            else
            {
                MaxParallelProcessing = 0;
            }
        }

        public override void PostUpdateMiscEffects()
        {
            
            if (!Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.OneForAll9th))
            {
                StrainPenaltyPerSecond = 0;
                return; 
            }
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (Player.ownedProjectileCounts[ModContent.ProjectileType<BlackWhipProjectile>()] >= 1) { ParallelProcessing++; }
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<BlackChainProjectile>()] >= 1) { ParallelProcessing++; }
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<BlackWhipStunProj>()] >= 1) { ParallelProcessing++; }
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<PinpointFocusProj>()] >= 1) { ParallelProcessing++; }

            if (currentFingers < MaxFingers)
            {
        
                Player.AddBuff(ModContent.BuffType<FingersBuff>(), 2); 
            }

        
           int strainDrain = (transPlayer.CurrentStage, percentage) switch
            {
                (QuirkStage.Adequation, 5)  => 8,
                (QuirkStage.Adequation, 10) => 15,
                (QuirkStage.Adequation, 20) => 20,
                (QuirkStage.Adequation, 45) => 50,

                (QuirkStage.Intermediate, 5)  => 4,
                (QuirkStage.Intermediate, 10) => 10,
                (QuirkStage.Intermediate, 20) => 15,
                (QuirkStage.Intermediate, 45) => 40,

                (QuirkStage.Advanced, 5)  => 2,
                (QuirkStage.Advanced, 10) => 5,
                (QuirkStage.Advanced, 20) => 8,   
                (QuirkStage.Advanced, 45) => 30,

                (QuirkStage.Final, 5)  => 0,
                (QuirkStage.Final, 10) => 3,      
                (QuirkStage.Final, 20) => 4,
                (QuirkStage.Final, 45) => 15,

                _ => 0
            };

            if (ParallelProcessing > 0)
            {
                Player.AddBuff( ModContent.BuffType<ParallelProcessingBuff>(), 2);
                if (ParallelProcessing > 1)
                {
                    int multiQuirkTax = (ParallelProcessing - 1) * 3;
                    strainDrain += multiQuirkTax;
                }
            }

            if (isMidGauntletsOn)
            {
                strainDrain = (int)(strainDrain * 0.65f);
            }

            if (isFullCowlingBuffActive)
            {
                
                HandleFullCowlingEffects();
                Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
                ElectricSoundTimer++;
                StrainPenaltyPerSecond = strainDrain;
            }
            else
            {
                StrainPenaltyPerSecond = 0;
            }

            }
        
    }
}