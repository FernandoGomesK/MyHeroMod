using Terraria.ModLoader;
using Terraria;

namespace MyHeroMod.content.Buffs 
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
            // O Buff agora só serve para avisar o Player que a Quirk está ligada!
            player.GetModPlayer<Quirks.ZeroGravity.ZeroGravityPlayer>().isZeroGravityActive = true; 
        }
    }
}