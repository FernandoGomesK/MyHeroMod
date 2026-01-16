using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.OFA8th
{
    public class StockPileBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/OneForAllStockPile";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

            

            if (transformPlayer.ActiveForm == QuirkSkills.StockPile)
            {
                // Apply Full Cowling 5% effects
                player.moveSpeed += 0.50f; // Increase movement speed by 5%
                player.statDefense += 2;    // Increase defense by 2
                player.jumpSpeedBoost += 3.0f; // Increase jump height
                
            }
            else if (transformPlayer.ActiveForm == QuirkSkills.StockPileMaximum)
            {
                // Apply Full Cowling 8% effects
                player.moveSpeed += 0.80f; // Increase movement speed by 8%
                player.statDefense += 3;    // Increase defense by 3
                player.jumpSpeedBoost += 4.5f; // Increase jump height
                
            }
        }
    }
}