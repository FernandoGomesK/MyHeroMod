using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th;

namespace MyHeroMod.content.Buffs
{
    public class FullCowlingBuff : ModBuff
    {
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();


            if (ofaPlayer.percentage == 45)
            {
                player.moveSpeed += 3f; 
                player.statDefense += 3;  
                player.jumpSpeedBoost += 5f;
                player.noFallDmg = true;
                ofaPlayer.isFullCowlingBuffActive = true;
                
            }
            if (ofaPlayer.percentage == 10)
            {
                player.moveSpeed += 2f; 
                player.statDefense += 3;    
                player.jumpSpeedBoost += 3f; 
                player.noFallDmg = true;
                ofaPlayer.isFullCowlingBuffActive = true;
            }
            if (ofaPlayer.percentage == 5)
            {
                player.moveSpeed += 1.5f; 
                player.statDefense += 2;    
                player.jumpSpeedBoost += 2.0f;
                player.noFallDmg = true;
                ofaPlayer.isFullCowlingBuffActive = true;
            }
}
    }}