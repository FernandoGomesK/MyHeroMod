using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Debuffs 
{
    public class ZeroGravityEnemyBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/content/Buffs/ZeroGravityBuff"; 
        
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; 
            Main.pvpBuff[Type] = true; 
            Main.buffNoSave[Type] = true; 
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            // Ativa a gravidade zero NA CLASSE GlobalNPC DO INIMIGO
            npc.GetGlobalNPC<ZeroGravityGlobalNPC>().hasZeroGravity = true; 
        }
    }
}