using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.FaJin; 
using Terraria.ID;
using MyHeroMod.content.Quirks.Erasure;

namespace MyHeroMod.content.Buffs
{
    public class ErasingBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/ErasingBuff";

        public override void SetStaticDefaults()
        {
            // Nome e descrição que aparecem ao passar o mouse
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var erasurePlayer = player.GetModPlayer<ErasurePlayer>();

            erasurePlayer.isErasureActive = true;

            
            

            
            
        }

        
    }
}