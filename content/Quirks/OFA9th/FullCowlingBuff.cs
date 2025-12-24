using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public class FullCowlingBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/OneForAllFullCowling5Percent";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();

            ofaPlayer.isFullCowlingBuffActive = true;

            if (transformPlayer.ActiveForm == OfaSkills.OneForAllFullCowling5)
            {
                // Apply Full Cowling 5% effects
                player.moveSpeed += 0.50f; // Increase movement speed by 5%
                player.statDefense += 2;    // Increase defense by 2
                player.jumpSpeedBoost += 1.0f; // Increase jump height
            }
            else if (transformPlayer.ActiveForm == OfaSkills.OneForAllFullCowling8)
            {
                // Apply Full Cowling 8% effects
                player.moveSpeed += 0.80f; // Increase movement speed by 8%
                player.statDefense += 3;    // Increase defense by 3
                player.jumpSpeedBoost += 1.5f; // Increase jump height
            }
        }
    }
}