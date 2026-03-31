using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Debuffs 
{
    public class DecayNPCBuff : GlobalNPC
    {
        public override bool InstancePerEntity => true; 
        
        public bool hasDecay;

        public override void ResetEffects(NPC npc)
        {
            
            hasDecay = false; 
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (hasDecay)
            {
                // Zera a regeneração natural do inimigo se ele tiver alguma
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                // A matemática do Terraria: a cada -2 de lifeRegen, o alvo toma 1 de dano por segundo.
                // Decay é forte! Vamos colocar -100 (Isso dá 50 de dano por segundo)
                npc.lifeRegen -= 100; 
                
                // O 'damage' é o numerozinho amarelo que pula da cabeça do inimigo pra indicar o debuff
                damage = 10; 
            }
        }
    }
}