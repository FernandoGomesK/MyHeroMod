using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace MyHeroMod.content.Quirks.SlideAndGlide
{
    public partial class SlideAndGlidePlayer : ModPlayer, IQuirkResetter, IFlightModifier, IDashModifier
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

        public bool IsOnGround()
        {
            
            return Player.velocity.Y == 0f;
        }

        public bool isMoving()
        {
            return Math.Abs(Player.velocity.X) > 0.1f || Math.Abs(Player.velocity.Y) > 0.1f;
        }

        public override void PostUpdateMiscEffects()
        {
        
            if ((isSlideOn || Player.HasBuff(ModContent.BuffType<SlideAndGlideBuff>())) && IsOnGround() && isMoving())
            {
                Player.fullRotation = Player.direction == 1 ? MathHelper.PiOver2 : -MathHelper.PiOver2;

            
                Player.fullRotationOrigin = new Vector2(Player.width / 2, Player.height / 2);

                Player.legFrame.Y = Player.legFrame.Height * 5; 
                Player.bodyFrame.Y = Player.bodyFrame.Height * 5;
            }
            else
            {
                
                Player.fullRotation = 0f;
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
                QuirkStage.Final => 15f, _ => 15f
            };

            speed = dashSpeed;
        }

        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.SlideAndGlide))
                return; 
                
            if (Player.HasBuff(ModContent.BuffType<SlideAndGlideBuff>()) && transPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                speed = transPlayer.CurrentStage switch 
                {
                    QuirkStage.Initial => 20f, 
                    QuirkStage.Adequation => 25f,
                    QuirkStage.Intermediate => 35f, 
                    QuirkStage.Advanced => 40f,
                    QuirkStage.Final => 60f, 
                    _ => 80f
                };

                isEnhanced = true;
                explosionColor = Color.SkyBlue; 
                dustType = DustID.BlueTorch;


            }

            
        }

        public bool CanCruiseFlight()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            return transPlayer.HasActiveQuirk(QuirkType.SlideAndGlide) && transPlayer.CurrentStage >= QuirkStage.Advanced;
        }
    }
}
