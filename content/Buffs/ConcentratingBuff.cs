using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;

namespace MyHeroMod.content.Buffs
{
    public class ConcentratingBuff : ModBuff
    {


        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

            var BluePlayer = player.GetModPlayer<BlueflamePlayer>();

            BluePlayer.isConcentrating = true;

        }
    }
}