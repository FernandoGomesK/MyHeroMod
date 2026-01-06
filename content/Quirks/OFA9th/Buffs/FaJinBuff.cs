using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.OFA9th.Buffs
{
    public class FaJinBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/FaJinBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            
            

           

            
        }
    }
}