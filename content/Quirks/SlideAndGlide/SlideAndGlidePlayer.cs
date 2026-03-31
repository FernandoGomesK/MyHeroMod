using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.SlideAndGlide
{
    public partial class SlideAndGlidePlayer : ModPlayer, IQuirkResetter, IHeroFlightModifier
    {
        public bool isSlideOn = false;
        public int greenLegsTimer = 0;

        public void FullReset()
        {
            isSlideOn = false;
        }

        public override void PreUpdate()
        {
            isSlideOn = false;
        }

        public override void PostUpdate()
        {
            
            if (greenLegsTimer > 0)
            {
                greenLegsTimer--;
            }
        }

            public void ModifyFlight(ref float speed)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
           
            if (!transPlayer.HasActiveQuirk(QuirkType.SlideAndGlide)) return; 
            
            

            float dashSpeed = transPlayer.CurrentStage switch 
            {
                QuirkStage.Initial => 2f, QuirkStage.Adequation => 5f,
                QuirkStage.Intermediate => 10f, QuirkStage.Advanced => 8f,
                QuirkStage.Final => 15f, _ => 40f
            };

            speed = dashSpeed ;
        }

        
        }
    }
