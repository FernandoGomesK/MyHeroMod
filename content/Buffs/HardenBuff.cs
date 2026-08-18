using Terraria.ModLoader;
using Terraria;
using MyHeroMod.content.Quirks.Hardening;

namespace MyHeroMod.content.Buffs 
{
    public class HardenBuff : ModBuff
    {
        
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<HardeningPlayer>().isHardeningOn = true; 
        }
    }
}