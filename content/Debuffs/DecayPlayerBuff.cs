using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Debuffs 
{
    public class DecayPlayerBuff : ModPlayer
    {
        public bool hasDecay;

        public override void ResetEffects()
        {
            hasDecay = false;
        }

        
        public override void UpdateBadLifeRegen()
        {
            if (hasDecay)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0; 
                
                Player.lifeRegen -= 100; 
            }
        }
    }
}