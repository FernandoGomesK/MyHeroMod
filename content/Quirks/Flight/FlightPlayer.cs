using System;
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

        public int flightShieldMaxHealth = 0;
        public float flightShieldHealth = 0;
        public int timeSinceLastHit = 0;
        
        public void FullReset()
        {
            isFlightOn = false;
            isFlightShieldOn = false;

            
            flightShieldHealth = 0f; 
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

            if (Player.HasBuff<FlightShieldBuff>())
            {
                isFlightShieldOn = true;
                var transPlayer = Player.GetModPlayer<TransformationPlayer>();

                
                flightShieldMaxHealth = transPlayer.CurrentStage switch 
                {
                    QuirkStage.Initial => 20, QuirkStage.Adequation => 50,
                    QuirkStage.Intermediate => 60, QuirkStage.Advanced => 80,
                    QuirkStage.Final => 120, _ => 20
                };
            }
            else
            {
                
                flightShieldMaxHealth = 0;
                flightShieldHealth = 0f;
            }
        }

        public override void PostUpdate()
        {
            timeSinceLastHit++;
            if (timeSinceLastHit > 350) 
            {
                timeSinceLastHit = 350; 
            }

            if (isFlightShieldOn && flightShieldHealth < flightShieldMaxHealth)
            {
                
                if (timeSinceLastHit > 300) 
                {
                    
                    flightShieldHealth += 0.5f; 
                    
                    if (flightShieldHealth > flightShieldMaxHealth)
                    {
                        flightShieldHealth = flightShieldMaxHealth;
                    }
                }
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (isFlightShieldOn && flightShieldHealth > 0)
            {
                modifiers.ModifyHurtInfo += (ref Player.HurtInfo info) =>
                {
                    int damageToAbsorb = Math.Min((int)flightShieldHealth, info.Damage);
                    info.Damage -= damageToAbsorb;
                    flightShieldHealth -= damageToAbsorb;

                    
                    timeSinceLastHit = 0; 

                    if (info.Damage <= 0)
                    {
                        info.Damage = 0;
                    }
                };
            }
        }
            

        public override void OnHurt(Player.HurtInfo info)
        {
            
            timeSinceLastHit = 0; 
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
