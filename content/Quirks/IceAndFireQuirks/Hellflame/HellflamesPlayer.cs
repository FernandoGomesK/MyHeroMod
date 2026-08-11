using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.Interfaces;
using Terraria.ID;
using ReLogic.Utilities;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.HellFlames
{
    public partial class HellFlamesPlayer : ModPlayer, IQuirkResetter, IHeroTemperature
    {
        // Temperature control
        private SlotId _loopSoundSlot;
        public int Temperature { get; set; } = 0;
        public int MaxTemperature { get; } = 100;
        public int MinTemperature { get; } = 0;    

        public int temperatureTimer = 0;   
        public bool IsCombatVestAlphaOn = false;
        public bool IsCombatVestBetaOn = false;
        public bool IsFlashFireFistActive = false;

        public void AddHeat(int amount)
        {
            Temperature += amount;
            if (Temperature > MaxTemperature) Temperature = MaxTemperature;
        }

        public void ReduceHeat(int amount)
        {
            Temperature -= amount;
            if (Temperature < MinTemperature) Temperature = MinTemperature;
        } 

        public override void OnRespawn()
        {
            Player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>());
            IsFlashFireFistActive = false;
            Temperature = 0;
        }

        public void FullReset()
        {
            Temperature = 0;
            IsFlashFireFistActive = false;
            Player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>());
        }

        public override void PreUpdate()
        {
            if (SoundEngine.TryGetActiveSound(_loopSoundSlot, out var activeSound))
            {
                activeSound.Stop();
            }
            IsFlashFireFistActive = false;
        }

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            if (!mainPlayer.HasActiveQuirk(QuirkType.HellFlames))  
                return;

            if (IsFlashFireFistActive)
            {
                Player.AddBuff(ModContent.BuffType<FlashfireFistBuff>(), 2);
            }
                
            if (Temperature >= MaxTemperature)
            {
                Player.AddBuff(ModContent.BuffType<Heatstroke>(), 2);
            }
                
            if (Temperature > 0)
            {
                Player.AddBuff(ModContent.BuffType<TemperatureBuff>(), 2);
            }

            
            if (mainPlayer.CurrentStage >= QuirkStage.Adequation && !Player.HasBuff<Heatstroke>())
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
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            if (!mainPlayer.HasActiveQuirk(QuirkType.HellFlames))
                return;
            
            
            if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                bool isFlying = (Player.velocity.Y != 0) && (Player.wingTime > 0 || Player.rocketDelay > 0) && !Player.mount.Active;

                if (Player.velocity.Y != 0 && !Player.mount.Active)
                {
                    if (Main.rand.NextBool(2)) 
                    {
                        int dustFire = Dust.NewDust(Player.position + new Vector2(-5, Player.height - 10), Player.width / 2, 10, DustID.Torch, 0, 2f, 100, default, 1.5f);
                        Main.dust[dustFire].noGravity = true;
                        Main.dust[dustFire].velocity *= 0.5f; 
                    }

                    if (Main.rand.NextBool(2))
                    {
                        int dustFire2 = Dust.NewDust(Player.position + new Vector2(Player.width / 2, Player.height - 10), Player.width / 2, 10, DustID.Torch, 0, 2f, 100, default, 1.5f);
                        Main.dust[dustFire2].noGravity = true;
                        Main.dust[dustFire2].velocity *= 0.5f;
                    }
                }
            }

            
            if (Temperature != 0)
            {
                temperatureTimer++;

                if (temperatureTimer >= 60)
                {
                    temperatureTimer = 0;
                    int recoveryRate = 1;

                    if (IsCombatVestAlphaOn) recoveryRate += 1; 
                    if (IsCombatVestBetaOn)  recoveryRate += 5; 

                    if (Temperature > 0)
                    {
                        Temperature -= recoveryRate;
                        if (Temperature < 0) Temperature = 0;
                    }
                }
            }
            else
            {
                temperatureTimer = 0;
            }
        }
    }
}