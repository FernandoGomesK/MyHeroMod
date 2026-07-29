using System;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.SpringLikeLimbs
{
    public partial class SpringLikeLimbsPlayer : ModPlayer, IQuirkResetter
    
    {
        public bool isSpringActive = false;
        private Vector2 previousVelocity;
        // public int springTimer = 0;

        public void FullReset()
        {
            isSpringActive = false;
        }

        public override void PreUpdate()
        {
            isSpringActive = false;
        }

        public override void PostUpdate()
        {
            if (isSpringActive)
            {
                
                if (Player.velocity.Y == 0f && previousVelocity.Y > 1f)
                {
                    
                    Player.velocity.Y = -previousVelocity.Y * 0.95f; 
                }

                
                if (Player.velocity.X == 0f && Math.Abs(previousVelocity.X) > 4f)
                {
                    
                    Player.velocity.X = -previousVelocity.X * 0.95f;
                }
            }

            
            previousVelocity = Player.velocity;
        }

        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            
            if (!transPlayer.HasActiveQuirk(QuirkType.SpringLikeLimbs)) return;

            if (isSpringActive) 
            {
                speed = 25f;             
            }
        
        }
            
            
        }

       
        
        }
    
