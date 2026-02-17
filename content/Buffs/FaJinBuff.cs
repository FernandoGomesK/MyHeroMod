using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.FaJin; 
using Terraria.ID;

namespace MyHeroMod.content.Quirks.OFA9th.Buffs
{
    public class FaJinBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/FaJinBuff";

        public override void SetStaticDefaults()
        {
            // Nome e descrição que aparecem ao passar o mouse
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // 1. Acessa o player que guarda as cargas (FaJinPlayer)
            var faJinPlayer = player.GetModPlayer<FajinPlayer>();

            // 2. Se o jogador não tiver mais cargas, o buff deve sumir automaticamente
            if (faJinPlayer.FaJinCharges <= 0)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
                return;
            }

            // 3. Efeito Visual: Partículas de "energia estocada" (Fogo laranja/laranja avermelhado)
            if (Main.rand.NextBool(3)) // 33% de chance a cada frame
            {
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Flare, 0f, 0f, 100, default, 1.2f);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }

        
    }
}