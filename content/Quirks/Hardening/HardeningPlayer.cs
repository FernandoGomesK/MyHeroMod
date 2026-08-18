using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Hardening
{
     public partial class HardeningPlayer : ModPlayer, IQuirkResetter
    {

        public bool isHardeningOn = false;
        public bool isUnbreakableOn = false;

        public int hardeningMaxHealth = 100;
        public float hardeningdHealth = 0;
        public int timeSinceLastHit = 0;    
        public void FullReset()
        {
            isHardeningOn = false;
            isUnbreakableOn = false;

            
            hardeningdHealth = 0f; 
            Player.ClearBuff(ModContent.BuffType<HardenBuff>());
            Player.ClearBuff(ModContent.BuffType<UnbreakableBuff>());
        }

        public override void PreUpdate()
        {
            isHardeningOn = false;
            isUnbreakableOn = false;
        }
    }
    
}
