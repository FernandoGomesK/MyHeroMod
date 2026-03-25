using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Float;
using Terraria;
using MyHeroMod.content.Quirks.Flight;

namespace MyHeroMod.content.Buffs // Ajuste o namespace se necessário
{
    public class FlightShieldBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/FlightShieldBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FlightPlayer>().isFlightShieldOn = true;

            
        }
    }
}