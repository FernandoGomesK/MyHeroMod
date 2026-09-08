using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Debuffs;
using ReLogic.Utilities;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass
{
    public abstract class BaseIceAndFirePlayer : ModPlayer, IQuirkResetter, IHeroTemperature
    {
        protected SlotId _loopSoundSlot;

        public void PlayLoopSound(SoundStyle style, Vector2 position)
        {
            if (!SoundEngine.TryGetActiveSound(_loopSoundSlot, out var activeSound))
            {
                _loopSoundSlot = SoundEngine.PlaySound(style, position);
            }
            else
            {
                activeSound.Position = position;
            }
        }

        public void StopLoopSound()
        {
            if (SoundEngine.TryGetActiveSound(_loopSoundSlot, out var activeSound))
            {
                activeSound.Stop();
            }
        }

    
        public int Temperature { get; set; } = 0;
        public int HeatPerSecond { get; set; }
        public int StrainPenaltyPerSecond { get; set; }

        
        public int CurrentStrain
        {
            get => Player.GetModPlayer<TransformationPlayer>().currentStrain;
            set => Player.GetModPlayer<TransformationPlayer>().currentStrain = value;
        }
        public int MaxStrain => Player.GetModPlayer<TransformationPlayer>().maxStrain;

        public abstract int MaxTemperature { get; }
        public abstract int MinTemperature { get; }
        public abstract int FlashfireHeatRate { get; }
        
        
        public virtual int StrainPenaltyThreshold => (int)(MaxTemperature * 0.75f);
        
        
        public virtual QuirkStage FlightUnlockStage => QuirkStage.Adequation;

        
        public bool IsFlashFireFistActive = false;
        public bool IsPhosphorActive = false;
        public bool isCombatVestAlphaOn = false;
        public bool isCombatVestBetaOn = false;
        public bool isSurgeArmGauntletsOn = false;

        public virtual int PhosphorCoolingRate => 0; 
        public virtual bool PhosphorFreezesTemperature => false;
        public virtual bool PhosphorTurnsOff => false;

        public void AddHeat(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            if (amount > 0 && (transPlayer.Nature == NatureType.HeatResistance || transPlayer.Nature == NatureType.ThermalResistance))
            {
                amount = (int)(amount * 0.5f);
            }
           
            else if (amount < 0 && (transPlayer.Nature == NatureType.ColdResistance || transPlayer.Nature == NatureType.ThermalResistance))
            {
                amount = (int)(amount * 0.5f);
            }

            Temperature += amount;
            
            if (Temperature > MaxTemperature) Temperature = MaxTemperature;
            if (Temperature < MinTemperature) Temperature = MinTemperature; 
        }

        public void ReduceHeat(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (amount < 0 && (transPlayer.Nature == NatureType.ColdResistance || transPlayer.Nature == NatureType.ThermalResistance))
            {
                amount = (int)(amount * 0.5f);
            }
            Temperature -= amount;
            if (Temperature < MinTemperature) Temperature = MinTemperature;
        }

        public void AddStrain(int amount)
        {
            CurrentStrain += amount;
            
            
            if (CurrentStrain < 0) CurrentStrain = 0; 

           
            if (amount > 0)
            {
                CombatText.NewText(Player.getRect(), Color.Cyan, $"{amount} Strain!", false, true);
            }

            if (CurrentStrain >= MaxStrain)
            {
                ApplyMaxStrainPenalty();
            }
        }

        protected virtual void ApplyMaxStrainPenalty()
        {
            Player.AddBuff(ModContent.BuffType<Heatstroke>(), 300);
            Player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>());
        }

        private int CalculateEnvironmentalHeat()
        {
            int envHeat = 0;

            
            if (Player.onFire || Player.onFire2 || Player.onFire3) envHeat += 10;
            if (Player.lavaWet) envHeat += 15;
            
            if (Player.ZoneDesert || Player.ZoneUnderworldHeight) envHeat += 2;
            if (Player.ZoneJungle) envHeat += 1;

            
            if (Player.wet && !Player.lavaWet && !Player.honeyWet) envHeat -= 8;
            
         
            if (Player.HasBuff(BuffID.Frostburn) || Player.HasBuff(BuffID.Frostburn2)) envHeat -= 10;
            if (Player.HasBuff(BuffID.Chilled) || Player.HasBuff(BuffID.Frozen)) envHeat -= 5;
            
            
            if (Player.ZoneSnow) envHeat -= 3;
            if (Player.ZoneSkyHeight) envHeat -= 1; 

            return envHeat;
        }

        

        

        public override void ResetEffects()
        {
            IsFlashFireFistActive = false;
            IsPhosphorActive = false;
            isCombatVestAlphaOn = false;
            isCombatVestBetaOn = false;
            isSurgeArmGauntletsOn = false;
        }

        public virtual void FullReset()
        {
            Temperature = 0;
            
            IsFlashFireFistActive = false;
            IsPhosphorActive = false;
            Player.ClearBuff(ModContent.BuffType<PhosphorBuff>());
            Player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>());
            Player.ClearBuff(ModContent.BuffType<Heatstroke>());
            Player.ClearBuff(ModContent.BuffType<FrostBite>());
            Player.ClearBuff(ModContent.BuffType<TemperatureBuff>());
            HeatPerSecond = 0;
            StrainPenaltyPerSecond = 0;
            IsPhosphorActive = false;
        }

        public override void OnRespawn()
        {
            Temperature = 0;
            CurrentStrain = 0;
            IsFlashFireFistActive = false;
            Player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>());
        }

       public override void PreUpdate()
        {
            if (IsPhosphorActive && Player.statLife <= 0.75 * Player.statLifeMax2 && PhosphorTurnsOff)
            {
                Player.ClearBuff(ModContent.BuffType<PhosphorBuff>());
                IsPhosphorActive = false;
            }

            
            if (Player.HasBuff<PhosphorBuff>() && PhosphorFreezesTemperature)
            {
                Temperature = 0;
                HeatPerSecond = 0;
            }
        }

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            if (IsFlashFireFistActive) Player.AddBuff(ModContent.BuffType<FlashfireFistBuff>(), 2);
            if (Temperature > 0 || Temperature < 0) Player.AddBuff(ModContent.BuffType<TemperatureBuff>(), 2);
            if (Temperature >= MaxTemperature) Player.AddBuff(ModContent.BuffType<Heatstroke>(), 2);
            if (Temperature <= MinTemperature && MinTemperature < 0) Player.AddBuff(ModContent.BuffType<FrostBite>(), 2);
            
            if (mainPlayer.CurrentStage >= FlightUnlockStage && !Player.HasBuff<Heatstroke>())
            {
                Player.wingTimeMax = 50;
                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29; 
                    Player.wings = -1;
                }
                Player.noFallDmg = true;
            }
        }

       public override void PostUpdate()
        {


            if (!IsFlashFireFistActive)
            {
                StopLoopSound();
            }
        
            if (!IsPhosphorActive || !PhosphorFreezesTemperature)
            {
                if (IsFlashFireFistActive)
                {
                    HeatPerSecond = FlashfireHeatRate; 
                }
                else
                {
                    int recoveryRate = 1;
                    if (isCombatVestAlphaOn) recoveryRate += 1; 
                    if (isCombatVestBetaOn)  recoveryRate += 5; 

                    if (Temperature > 0) HeatPerSecond = -recoveryRate;      
                    else if (Temperature < 0) HeatPerSecond = recoveryRate;  
                    else HeatPerSecond = 0;                       
                }

                HeatPerSecond += CalculateEnvironmentalHeat();

                if (IsPhosphorActive)
                {
                    HeatPerSecond -= PhosphorCoolingRate;
                    if (Temperature <= -50 && HeatPerSecond < 0)
                    {
                        Temperature = -50; 
                        HeatPerSecond = 0; 
                    }
                }
            }
            else
            {
                
                HeatPerSecond = 0;
            }
            
        var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
        bool isOverheating = Temperature >= (MaxTemperature * 0.75f);
        bool isFreezing = MinTemperature < 0 && Temperature <= (MinTemperature * 0.75f);

        bool isSafeHot = Temperature >= 0 && Temperature <= (MaxTemperature * 0.25f);
        bool isSafeCold = Temperature < 0 && Temperature >= (MinTemperature * 0.25f);

        if (isOverheating || isFreezing)
        {
            int penalty = 20;

            
            if (isOverheating && (transPlayer.Nature == NatureType.HeatResistance || transPlayer.Nature == NatureType.ThermalResistance))
            {
                penalty = 10; 
            }
            if (isFreezing && (transPlayer.Nature == NatureType.ColdResistance || transPlayer.Nature == NatureType.ThermalResistance))
            {
                penalty = 10; 
            }

            StrainPenaltyPerSecond = penalty; 
        }
        else if (isSafeHot || isSafeCold)
        {
            StrainPenaltyPerSecond = -5; 
        }
        else 
        {
            StrainPenaltyPerSecond = 0;  
        }

        UpdateFlyingDust();
        }
        protected virtual void UpdateFlyingDust()
        {
            bool isFlying = (Player.velocity.Y != 0) && (Player.wingTime > 0 || Player.rocketDelay > 0) && !Player.mount.Active;
            if (isFlying)
            {
                if (Main.rand.NextBool(2)) 
                {
                    int dustFire = Dust.NewDust(Player.position + new Vector2(-5, Player.height - 10), Player.width / 2, 10, DustID.Torch, 0, 2f, 100, default, 1.5f);
                    Main.dust[dustFire].noGravity = true;
                    Main.dust[dustFire].velocity *= 0.5f; 
                }
                
                if (MinTemperature < 0 && Main.rand.NextBool(2))
                {
                    int dustIce = Dust.NewDust(Player.position + new Vector2(Player.width / 2, Player.height - 10), Player.width / 2, 10, DustID.IceTorch, 0, 2f, 100, default, 1.5f);
                    Main.dust[dustIce].noGravity = true;
                    Main.dust[dustIce].velocity *= 0.5f;
                }
            }
        }
    }
}