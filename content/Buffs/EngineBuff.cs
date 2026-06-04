using Terraria.ModLoader;
using Terraria;
using MyHeroMod.content.Quirks.SlideAndGlide;
using MyHeroMod.content.Quirks.Engine;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Buffs 
{
    public class EngineBuff : ModBuff
    {
        
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var enginePlayer = player.GetModPlayer<EnginePlayer>();
            var mainPlayer = player.GetModPlayer<TransformationPlayer>();

            enginePlayer.isEngineOn = true;
            
            if (!mainPlayer.HasActiveQuirk(QuirkType.Engine))  
                return;
            
            if (enginePlayer.isEngineOn)
            {
                player.noFallDmg = true;
                
                // Os seus status base
                float baseAcceleration = mainPlayer.CurrentStage switch
                {
                    QuirkStage.Initial => 1f, 
                    QuirkStage.Adequation => 2.5f, 
                    QuirkStage.Intermediate => 3.0f, 
                    QuirkStage.Advanced => 3.5f, 
                    QuirkStage.Final => 5.50f, 
                    _ => 2.0f
                };

                float baseMaxSpeed = mainPlayer.CurrentStage switch
                {
                    QuirkStage.Initial => 5f, 
                    QuirkStage.Adequation => 6.5f, 
                    QuirkStage.Intermediate => 8.5f, 
                    QuirkStage.Advanced => 12.5f, 
                    QuirkStage.Final => 15.0f, 
                    _ => 4.0f
                };

                float baseJumpBoost = mainPlayer.CurrentStage switch
                {
                    QuirkStage.Initial => 5f, 
                    QuirkStage.Adequation => 6.5f, 
                    QuirkStage.Intermediate => 8.5f, 
                    QuirkStage.Advanced => 10.5f, 
                    QuirkStage.Final => 12.0f, 
                    _ => 4.0f
                };

                // --- APLICAÇÃO DAS MARCHAS ---
                // Cada marcha aumenta o poder base em 25% (Gear 4 = +100%, Gear 5 = +125%!)
                float gearMultiplier = 1f + (0.25f * enginePlayer.currentGear);

                float reciproSpeedBoost = 0f;
                float reciproAccelBoost = 0f;
                float reciproJumpBoost = 0f;

            
                if (player.HasBuff(ModContent.BuffType<ReciproBuff>()))
                {
                    reciproSpeedBoost = mainPlayer.CurrentStage switch
                    {
                        QuirkStage.Initial => 2f,      
                        QuirkStage.Adequation => 4f,    
                        QuirkStage.Intermediate => 7f,  
                        QuirkStage.Advanced => 12f,     
                        QuirkStage.Final => 20f,        
                        _ => 0f
                    };

                    reciproAccelBoost = mainPlayer.CurrentStage switch
                    {
                        QuirkStage.Initial => 0.5f, 
                        QuirkStage.Adequation => 1.0f, 
                        QuirkStage.Intermediate => 2.0f, 
                        QuirkStage.Advanced => 3.0f, 
                        QuirkStage.Final => 5.0f, 
                        _ => 0f
                    };
                    
                    reciproJumpBoost = mainPlayer.CurrentStage switch
                    {
                        QuirkStage.Initial => 1.0f, 
                        QuirkStage.Adequation => 2.0f, 
                        QuirkStage.Intermediate => 3.0f, 
                        QuirkStage.Advanced => 5.0f, 
                        QuirkStage.Final => 8.0f, 
                        _ => 0f
                    };
                }

                // Aplica a fórmula final: (Base * Marcha) + Bónus do Recipro
                player.runAcceleration *= (baseAcceleration * gearMultiplier) + reciproAccelBoost;
                player.maxRunSpeed += (baseMaxSpeed * gearMultiplier) + reciproSpeedBoost;
                player.jumpSpeedBoost += (baseJumpBoost) + reciproJumpBoost; 
            }
        }
    }
}