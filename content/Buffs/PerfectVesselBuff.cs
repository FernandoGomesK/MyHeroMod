using Terraria.ModLoader;
using Terraria;

namespace MyHeroMod.content.Buffs 
{
    public class PerfectVesselBuff : ModBuff
    {
        
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}