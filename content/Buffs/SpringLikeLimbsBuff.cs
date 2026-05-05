using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Smokescreen;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using MyHeroMod.content.Quirks.SpringLikeLimbs;

namespace MyHeroMod.content.Buffs 
{
    public class SpringLikeLimbsBuff : ModBuff
    {
        
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SpringLikeLimbsPlayer>().isSpringActive = true;
           
            
                player.jumpSpeedBoost += 5f;
                player.noFallDmg = true;
        }
    }
}