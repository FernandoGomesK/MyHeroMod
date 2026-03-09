using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Quirks.HalfColdHalfHot;

namespace MyHeroMod.content.Buffs
{
    public class FlashFireFistBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/HCFireFistBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            // var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
            // var bluePlayer = player.GetModPlayer<BlueFlamesPlayer>();
            hchhPlayer.IsFlashFireFistActive = true;
    }
}
}