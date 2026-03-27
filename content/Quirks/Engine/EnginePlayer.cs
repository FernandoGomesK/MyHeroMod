using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Engine
{
    public partial class EnginePlayer : ModPlayer, IQuirkResetter
    {
        public bool isEngineOn = false;

        public void FullReset()
        {
            isEngineOn = false;
        }

        public override void PreUpdate()
        {
            isEngineOn = false;
        }

        public override void PostUpdate()
        {
           
            }

        //     public void ModifyFlight(ref float speed)
        // {
        //     var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
           
        //     if (!transPlayer.HasActiveQuirk(QuirkType.SlideAndGlide)) return; 
            
            

        //     float dashSpeed = transPlayer.CurrentStage switch 
        //     {
        //         QuirkStage.Initial => 2f, QuirkStage.Adequation => 5f,
        //         QuirkStage.Intermediate => 10f, QuirkStage.Advanced => 8f,
        //         QuirkStage.Final => 15f, _ => 40f
        //     };

        //     speed = dashSpeed ;
        // }
        }
    }
