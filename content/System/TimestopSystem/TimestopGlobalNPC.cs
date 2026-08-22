using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.System
{
    public class TimeStopGlobalNPC : GlobalNPC
    {
        
        public override bool PreAI(NPC npc)
        {
            
            if (TimeStopSystem.IsTimeStopped)
            {
                
                npc.velocity = Vector2.Zero;
                
                
                npc.frameCounter = 0; 
                
                
                return false; 
            }

            
            return base.PreAI(npc);
        }
    }
}