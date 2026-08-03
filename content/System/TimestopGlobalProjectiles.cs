using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using KhacesCore.Content.System.BaseProjectiles;

namespace MyHeroMod.content.System
{
    public class TimeStopGlobalProjectile : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {
            if (TimeStopSystem.IsTimeStopped)
            {
                

    
                Player owner = Main.player[projectile.owner];
                if (owner.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.Overclock))
                {
                    return base.PreAI(projectile);
                }
                

                
                projectile.velocity = Vector2.Zero;
                projectile.frameCounter = 0; 
                
            
                if (projectile.timeLeft > 0)
                {
                    projectile.timeLeft++;
                }
                
                return false; 
            }
            
            return base.PreAI(projectile);
        }
    }
}