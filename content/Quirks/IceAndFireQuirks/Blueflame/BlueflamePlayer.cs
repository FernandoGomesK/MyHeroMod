using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Debuffs;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame
{
    public partial class BlueflamePlayer : ModPlayer, IQuirkResetter, IHeroTemperature
    {
        public int Temperature { get; set; } = 0;
        public int MaxTemperature { get; } = 100;
        public int MinTemperature { get; } = -50; 
        public int HeatPerSecond { get; set; }
        public int StrainPenaltyPerSecond { get; set; }
        public int CurrentStrain
        {
            get => Player.GetModPlayer<TransformationPlayer>().currentStrain;
            set => Player.GetModPlayer<TransformationPlayer>().currentStrain = value;
        }

        public int MaxStrain
        {
            get => Player.GetModPlayer<TransformationPlayer>().maxStrain;
        }

    
        public bool IsFlashFireFistActive = false;
        public bool IsRageActive = false;
        public bool IsPhosphorActive = false;

        public void AddHeat(int amount)
        {
            Temperature += amount;
            if (Temperature > MaxTemperature) Temperature = MaxTemperature;
        
            if (Temperature < MinTemperature) Temperature = MinTemperature; 
        }

        public void ReduceHeat(int amount)
        {
            Temperature -= amount;
            if (Temperature < MinTemperature) Temperature = MinTemperature;
        }

        public void AddStrain(int amount)
        {
            CurrentStrain += amount;
            CombatText.NewText(Player.getRect(), Color.Cyan, $"{amount} Strain!", false, true);

            if (CurrentStrain >= MaxStrain)
            {
                Player.AddBuff(ModContent.BuffType<Heatstroke>(), 300);
                Player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>());
            }
        }

        public override void ResetEffects()
        {
            IsFlashFireFistActive = false;
            IsPhosphorActive = false;
        }

        public void FullReset()
        {
            Temperature = 0;
            CurrentStrain = 0;
            IsFlashFireFistActive = false;
            IsRageActive = false;
            IsPhosphorActive = false;
            Player.ClearBuff(ModContent.BuffType<PhosphorBuff>());
            Player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>());
        }

        public override void OnRespawn()
        {
            Temperature = 0;
            CurrentStrain = 0;
        }

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.Blueflame)) return;

            if (Temperature > 0)
            {
                Player.AddBuff(ModContent.BuffType<TemperatureBuff>(), 2);
            }

            if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                Player.wingTimeMax = 50;
                if (Player.wingsLogic == 0) Player.wingsLogic = 29; 
                Player.noFallDmg = true;
            }
        }

        public override void PostUpdate()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.Blueflame)) return;

            
            if (IsFlashFireFistActive)
            {
                HeatPerSecond = 20; 
            }
            else
            {
                
                if (Temperature > 0) HeatPerSecond = -1;      
                else if (Temperature < 0) HeatPerSecond = 1;  
                else HeatPerSecond = 0;                       
            }

           
            if (Temperature >= 50)
            {
                StrainPenaltyPerSecond = 20; 
            }
            else
            {
                StrainPenaltyPerSecond = 0;
            }

        
            UpdateFlyingDust();
        }
    }
}