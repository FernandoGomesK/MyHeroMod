using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Float;
using Terraria;

namespace MyHeroMod.content.Buffs // Ajuste o namespace se necessário
{
    public class FloatBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/FloatBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FloatPlayer>().isFloatActive = true;
        }
    }
}