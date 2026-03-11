using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.OFA8th
{
    public class StockPileBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/OneForAllStockPile";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            var ofaPlayer = player.GetModPlayer<OneForAll8thPlayer>();




            if (ofaPlayer.form == 1)
            {
                player.moveSpeed += 2f; 
                player.statDefense += 2;    
                player.jumpSpeedBoost += 3.0f;     
                player.noFallDmg = true;
            }
            else if (ofaPlayer.form == 2)
            {
                player.moveSpeed += 4f; 
                player.statDefense += 3;    
                player.jumpSpeedBoost += 4.5f;
                player.noFallDmg = true;      
            }
        }
    }
}