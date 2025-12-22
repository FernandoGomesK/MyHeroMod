using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public class FullCowlingBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/OneForAllFullCowling5Percent";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<OneForAll9thPlayer>().isFullCowlingBuffActive = true;
        }
    }
}