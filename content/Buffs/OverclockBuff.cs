using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.Gearshift;
using MyHeroMod.content.Quirks.Overclock;
using MyHeroMod.content.Dusts;

namespace MyHeroMod.content.Buffs
{
    public class OverclockBuff : ModBuff
    {
       
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
           
            var overclockPlayer = player.GetModPlayer<OverclockPlayer>();
            
            overclockPlayer.isOverclockBuffActive = true;

            player.moveSpeed += 4.0f; 
            player.accRunSpeed += 8.0f; 
            player.maxRunSpeed += 8.0f; 
            player.jumpSpeedBoost += 4.0f; 
            player.noFallDmg = true;

            
            if (Main.rand.NextFloat() < 0.6f) 
            {
                int dustType = ModContent.DustType<ElectricityDust>();

                
                Vector2 spawnOffset = Main.rand.NextVector2CircularEdge(35f, 45f); 
                Vector2 dustSpawnPos = player.Center + spawnOffset;

                
                Vector2 dustVelocity = new Vector2(spawnOffset.X * 0.05f, -Main.rand.NextFloat(1f, 3f));

               
                Dust.NewDustPerfect(dustSpawnPos, dustType, dustVelocity, 0, Color.Yellow, 1.2f);
            }

           

           
        }
    }
}