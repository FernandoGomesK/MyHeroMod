using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace MyHeroMod.content.Debuffs 
{
    public class DecayBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = false; 
            Main.debuff[Type] = true; 
        }

        
        public override void Update(Player player, ref int buffIndex)
        {
            // Ativa a flag no ModPlayer
            player.GetModPlayer<DecayPlayerBuff>().hasDecay = true;
            
            
            int d1 = Dust.NewDust(player.position, 1, 1, DustID.Wraith, 0f, 0f, 100, default, 2.5f);
                Main.dust[d1].noGravity = true;
                Main.dust[d1].velocity.Y = -Main.rand.NextFloat(2f, 5f); // Shoot UP
                Main.dust[d1].velocity.X *= 0.2f; // Don't move sideways much

                // Purple Dust
                if (Main.rand.NextBool(2)) // 50% chance for purple
                {
                    int d2 = Dust.NewDust(player.position, 1, 1, DustID.PurpleTorch, 0f, 0f, 100, default, 2.0f);
                    Main.dust[d2].noGravity = true;
                    Main.dust[d2].velocity.Y = -Main.rand.NextFloat(3f, 6f); 
                    Main.dust[d2].velocity.X *= 0.2f;
                }
        }

    
        public override void Update(NPC npc, ref int buffIndex)
        {
            // Ativa a flag no GlobalNPC
            npc.GetGlobalNPC<DecayNPCBuff>().hasDecay = true;
            
           int d1 = Dust.NewDust(npc.position, 1, 1, DustID.Wraith, 0f, 0f, 100, default, 2.5f);
                Main.dust[d1].noGravity = true;
                Main.dust[d1].velocity.Y = -Main.rand.NextFloat(2f, 5f); // Shoot UP
                Main.dust[d1].velocity.X *= 0.2f; // Don't move sideways much

                // Purple Dust
                if (Main.rand.NextBool(2)) // 50% chance for purple
                {
                    int d2 = Dust.NewDust(npc.position, 1, 1, DustID.PurpleTorch, 0f, 0f, 100, default, 2.0f);
                    Main.dust[d2].noGravity = true;
                    Main.dust[d2].velocity.Y = -Main.rand.NextFloat(3f, 6f); 
                    Main.dust[d2].velocity.X *= 0.2f;
                }
        }
    }
}