using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.FaJin; 
using Terraria.ID;

namespace MyHeroMod.content.Buffs
{
    public class FaJinActiveBuff : ModBuff
    {
        

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var faJinPlayer = player.GetModPlayer<FajinPlayer>();

            faJinPlayer.isFaJinActive = true;

            if (Main.rand.NextBool(6)) 
            {
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.RedTorch, 0f, 0f, 100, default, 1.5f);
                dust.noGravity = true;
                dust.velocity *= 1f;
            }       
        }

        
    }
}