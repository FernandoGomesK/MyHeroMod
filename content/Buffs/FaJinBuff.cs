using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.FaJin; 
using Terraria.ID;

namespace MyHeroMod.content.Buffs
{
    public class FaJinBuff : ModBuff
    {
        

        public override void SetStaticDefaults()
        {
            // Nome e descrição que aparecem ao passar o mouse
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var faJinPlayer = player.GetModPlayer<FajinPlayer>();

            
            if (faJinPlayer.FaJinCharges <= 0)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
                return;
            }

            
            if (Main.rand.NextBool(3)) 
            {
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.RedTorch, 0f, 0f, 100, default, 2.0f);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }

        
    }
}