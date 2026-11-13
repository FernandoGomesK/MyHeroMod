using Terraria.ModLoader;
using Terraria;

namespace MyHeroMod.content.Buffs.ZeroGravity
{
    public class ZeroGravityBuff : ModBuff
    {
        
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<Quirks.ZeroGravity.ZeroGravityPlayer>().isZeroGravityActive = true; 
        }
    }
}