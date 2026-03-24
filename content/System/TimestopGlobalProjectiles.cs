using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.System
{
    public class TimeStopGlobaProjectile : GlobalProjectile
    {
        
        public override bool PreAI(Projectile projectile)
        {
            
            if (TimeStopSystem.IsTimeStopped)
            {
                
                projectile.velocity = Vector2.Zero;
                
                
                projectile.frameCounter = 0; 
                
                
                return false; 
            }

            
            return base.PreAI(projectile);
        }
    }
}