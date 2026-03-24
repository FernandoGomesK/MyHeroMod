using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.Overclock
{
    public partial class OverclockPlayer : ModPlayer, IHeroDashModifier, IQuirkResetter, IHeroPunchModifier
    {
        public int form = 0;
        public bool isOverclockBuffActive = false;

        public int maxBreath = 0;
        // Inicia com 90 como padrão para não começar zerado
        public int currentBreath = 90; 
        
        private int ElectricSoundTimer = 0;

        public void FullReset()
        {
            isOverclockBuffActive = false;
        }

        public override void OnRespawn()
        {
            
            currentBreath = maxBreath;
            isOverclockBuffActive = false;
        }

        public override void ResetEffects()
        {
            isOverclockBuffActive = false; 
        }

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
                TimeStopSystem.IsTimeStopped = true; 

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
    }
}