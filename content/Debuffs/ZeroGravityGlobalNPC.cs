using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Debuffs 
{
    // O nome mudou para GlobalNPC para não confundir com o Buff real!
    public class ZeroGravityGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true; 
        
        public bool hasZeroGravity;
        
        
        private bool storedNoGravity;
        private bool gravityStored;

        public override void ResetEffects(NPC npc)
        {
            hasZeroGravity = false; 
        }

        public override void PostAI(NPC npc)
        {
            if (hasZeroGravity)
            {
                if (!gravityStored)
                {
                    storedNoGravity = npc.noGravity;
                    gravityStored = true;
                }

                npc.noGravity = true;
                
                if (npc.velocity.Y > -2f) 
                {
                    npc.velocity.Y -= 0.1f; 
                }
                
                if (Main.rand.NextBool(5))
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.PinkFairy);
                }
            }
            else
            {
                if (gravityStored)
                {
                    npc.noGravity = storedNoGravity;
                    gravityStored = false;
                }
            }
        }
    }
}