using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using KhacesCore.Content.System.Interfaces;
using MyHeroMod.content.System.Interfaces;

namespace MyHeroMod.content.Quirks.Overclock
{
    public partial class OverclockPlayer : ModPlayer, IDashModifier, IQuirkResetter, IHeroBreath
    {
        public int form = 0;
        public bool isOverclockBuffActive = false;

        public int maxBreath = 0;
        
        public int currentBreath = 90; 
        
        private int ElectricSoundTimer = 0;

        public int BreathChangePerSecond { get; set; }

        public void AddBreath(int amount)
        {
            currentBreath += amount;


            if (currentBreath > maxBreath) currentBreath = maxBreath;

            
            if (currentBreath <= 0)
            {
                currentBreath = 0; 
                isOverclockBuffActive = false;
                Player.ClearBuff(ModContent.BuffType<OverclockBuff>());
                CombatText.NewText(Player.getRect(), Color.Red, "BREATH!");
            }
        }

        public void FullReset()
        {
            isOverclockBuffActive = false;
        }

        public override void OnRespawn()
        {
            
            currentBreath = maxBreath;
            isOverclockBuffActive = false;
        }

        // public override void ResetEffects()
        // {
        //     isOverclockBuffActive = false; 
        // }

        public override void PreUpdate()
        { 
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            maxBreath = transPlayer.CurrentStage switch {
                QuirkStage.Initial => 80, 
                QuirkStage.Adequation => 100, 
                QuirkStage.Intermediate => 150,
                QuirkStage.Advanced => 240, 
                QuirkStage.Final => 300, 
                _ => 90
            };
            
            
            if (currentBreath > maxBreath)
            {
                currentBreath = maxBreath;
            }
        }

        

        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            if (!transPlayer.HasActiveQuirk(QuirkType.Overclock)) return;

            if (Player.HasBuff(ModContent.BuffType<OverclockBuff>()))
            {
                // TimeStopSystem.IsTimeStopped = true; 
 
                currentBreath--;
                
                if (currentBreath <= 0)
                {
                    currentBreath = 0; 
                    isOverclockBuffActive = false;
                    Player.ClearBuff(ModContent.BuffType<OverclockBuff>());
                    CombatText.NewText(Player.getRect(), Color.Red, "BREATH!");
                }
            }
            else 
            {
                if (currentBreath < maxBreath)
                {
                    currentBreath++; 
                }
            }
        }

        public override void ResetEffects()
        {
            isOverclockBuffActive = Player.HasBuff(ModContent.BuffType<OverclockBuff>());
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

        
            if (isOverclockBuffActive)
            {
                
                int drainAmount = -60; 

            
                if (transPlayer.Nature == NatureType.HigherBrainPower)
                {
                    drainAmount = -40; 
                }

                BreathChangePerSecond = drainAmount;
            }
            else 
            {
                // Recovers 60 breath per second when not active
                BreathChangePerSecond = 60; 
                
                // Stop the global system from trying to add breath if we are already full
                if (currentBreath >= maxBreath) BreathChangePerSecond = 0;
            }
        }
    }
}
    
