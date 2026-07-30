using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.Gearshift;
using Terraria.ID;

namespace MyHeroMod.content.Buffs
{
    public class GearshiftBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var gearshiftPlayer = player.GetModPlayer<GearshiftPlayer>();
            gearshiftPlayer.isGearshiftBuffActive = true;

            player.moveSpeed += 1.0f;
            player.accRunSpeed += 5.0f; 
            player.maxRunSpeed += 5.0f; 
            player.jumpSpeedBoost += 2.0f; 
            player.noFallDmg = true;

            
            if (Main.rand.NextBool(2)) 
            {
                Vector2 offset = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                
                int dustIndex = Dust.NewDust(
                    player.position + offset, 
                    player.width, 
                    player.height, 
                    DustID.Electric, 
                    0f,
                    0f, 
                    0, 
                    Color.Green, 
                    0.5f 
                );

            
                Main.dust[dustIndex].velocity.X *= 0.1f; 
                Main.dust[dustIndex].velocity.Y = -1.2f; 
                
                Main.dust[dustIndex].noGravity = true;
            }
        }
    }
}