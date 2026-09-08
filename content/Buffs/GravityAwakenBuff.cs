using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow;
using Terraria.ID;
using MyHeroMod.content.Quirks.BlackWhip;

namespace MyHeroMod.content.Buffs
{
    public class GravityAwakenBuff : ModBuff
    {
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            // var whipPlayer = player.GetModPlayer<BlackWhipPlayer>();

            


            // whipPlayer.isOverlayActive = true;
            
                player.moveSpeed += 1.5f; 
                player.statDefense += 2;    
                player.jumpSpeedBoost += 2.0f;
                player.noFallDmg = true;

                for (int i = 0; i < 5; i++) 
                    {
                        Vector2 spawnOffset = Main.rand.NextVector2CircularEdge(35f, 45f); 
                        Vector2 dustSpawnPos = player.Center + spawnOffset;
                        

                        int dustIndex = Dust.NewDust(dustSpawnPos, player.width, player.height, DustID.PinkFairy, 0f, 0f, 100, default, 1.0f);
                        if (dustIndex >= 0)
                        {
                            Dust dust = Main.dust[dustIndex];
                            dust.noGravity = true;
                            dust.velocity *= 0.3f; 
                        }
                    }
            
}
    }}