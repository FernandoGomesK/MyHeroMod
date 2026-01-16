using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.OFA9th.Buffs
{
    public class DangerSenseBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/DangerSenseBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            player.detectCreature = true;
            player.dangerSense = true;
        }
    }
}