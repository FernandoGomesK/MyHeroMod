using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;

namespace MyHeroMod.content.Buffs
{
    public class PhosphorBuff : ModBuff
    {
        
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            
            
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            var bluePlayer = player.GetModPlayer<BlueflamePlayer>();
            hchhPlayer.IsPhosphorActive = true;


    }
    }
}