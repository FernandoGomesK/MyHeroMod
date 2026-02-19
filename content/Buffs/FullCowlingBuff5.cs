using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Buffs
{
    public class FullCowlingBuff5 : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/OneForAllFullCowling5Percent";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            // var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();

            // ofaPlayer.isFullCowlingBuffActive = true;

           
                
                player.moveSpeed += 2f; 
                player.statDefense += 3;  
                player.jumpSpeedBoost += 5f;
                player.noFallDmg = true;

 }
}
}