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
                // --- FLOOR BOUNCING ---
                // If the player is currently on the ground (Y velocity is 0),
                // AND they were falling reasonably fast in the previous frame (Y > 1f)
                if (Player.velocity.Y == 0f && previousVelocity.Y > 1f)
                {
                    // Invert the velocity to make them bounce up!
                    // Multiplying by 0.8f means they retain 80% of their falling speed (so they eventually settle)
                    Player.velocity.Y = -previousVelocity.Y * 0.95f; 
                }

                // --- WALL BOUNCING (Optional, fits the "Spring Limbs" theme) ---
                // If they hit a wall horizontally and were moving fast
                if (Player.velocity.X == 0f && Math.Abs(previousVelocity.X) > 4f)
                {
                    // Bounce off the wall
                    Player.velocity.X = -previousVelocity.X * 0.95f;
                }
            }

            
            previousVelocity = Player.velocity;
        }
            
            
        }

       
        
        }
    
