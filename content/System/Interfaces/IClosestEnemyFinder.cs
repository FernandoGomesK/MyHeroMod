using Microsoft.Xna.Framework;
using Terraria;

namespace MyHeroMod.content.System
{
    public interface IClosestEnemyFinder
    {
        
        NPC FindClosestEnemy(Player player, float maxRange, bool canAttackCitizens);
    }

    public class TargetFinder : IClosestEnemyFinder
    {
        public NPC FindClosestEnemy(Player player, float maxRange, bool canAttackCitizens)
        {
            NPC closestNPC = null;
            float minDistance = maxRange;

            foreach (NPC npc in Main.npc)
            {
            
                if (npc.active && npc.lifeMax > 5 && !npc.dontTakeDamage)
                {
                    
                    if (npc.friendly && !canAttackCitizens) continue;

                    float distanceToNpc = player.Distance(npc.Center);
                    
                    if (distanceToNpc < minDistance)
                    {
                        // Line of sight check to prevent targeting through walls
                        if (Collision.CanHitLine(player.Center, 1, 1, npc.Center, 1, 1))
                        {
                            minDistance = distanceToNpc;
                            closestNPC = npc;
                        }
                    }
                }
            }

            return closestNPC;
        }
    }
}