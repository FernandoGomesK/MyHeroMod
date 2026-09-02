using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Dusts;

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
            if (ofaPlayer.percentage == 20)
            {
                player.moveSpeed += 2.5f; 
                player.statDefense += 3;    
                player.jumpSpeedBoost += 4f; 
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

           float spawnChance = 0.2f + (ofaPlayer.percentage / 100f); 

            if (Main.rand.NextFloat() < spawnChance) 
            {
                int dustType = ModContent.DustType<ElectricityDust>();

                Vector2 spawnOffset = Main.rand.NextVector2CircularEdge(35f, 45f); 
                Vector2 dustSpawnPos = player.Center + spawnOffset;
                
                Vector2 dustVelocity = new Vector2(spawnOffset.X * 0.05f, -Main.rand.NextFloat(1f, 3f));

              
                Color cowlinkColor = new Color(0, 255, 162); 
            
                float dustScale = 1.0f + (ofaPlayer.percentage / 150f); 

                Dust.NewDustPerfect(dustSpawnPos, dustType, dustVelocity, 0, cowlinkColor, dustScale);
            }
}
    }}