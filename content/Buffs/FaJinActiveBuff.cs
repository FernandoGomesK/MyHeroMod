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
        public override string Texture => "MyHeroMod/Assets/BuffImage/FaJinActiveBuff";

        public override void SetStaticDefaults()
        {
            // Nome e descrição que aparecem ao passar o mouse
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var faJinPlayer = player.GetModPlayer<FajinPlayer>();

            faJinPlayer.isFaJinActive = true;

            
            

            
            
        }

        
    }
}