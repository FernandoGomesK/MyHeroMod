using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Flight
{
    
    public partial class FlightPlayer : ModPlayer, IQuirkResetter, IHeroFlightModifier
    {
        public bool isFlightOn = false;
        public bool isFlightShieldOn = false;
        
        public void FullReset()
        {
            isFlightOn = false;
            isFlightShieldOn = false;
            Player.ClearBuff(ModContent.BuffType<FlightBuff>());
            Player.ClearBuff(ModContent.BuffType<FlightShieldBuff>());
        }

        public override void PreUpdate()
        {
            isFlightOn = false;
            isFlightShieldOn = false;
        }

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            if (!mainPlayer.HasActiveQuirk(QuirkType.Flight))  
                return;

            if (Player.HasBuff<FlightBuff>())
            {
                Player.wingTimeMax =360000;

                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29; 
                    Player.wings = -1; 
                }

                Player.noFallDmg = true;
            }
            
        }

        public void ModifyFlight(ref float speed)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
           
            if (!transPlayer.HasActiveQuirk(QuirkType.Flight)) return; 
            
            

            float dashSpeed = transPlayer.CurrentStage switch 
            {
                QuirkStage.Initial => 8f, QuirkStage.Adequation => 12f,
                QuirkStage.Intermediate => 15f, QuirkStage.Advanced => 18f,
                QuirkStage.Final => 20f, _ => 8f
            };

            speed = dashSpeed ;
        }
            
        }
    }
