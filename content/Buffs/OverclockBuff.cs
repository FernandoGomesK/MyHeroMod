using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.Gearshift;
using MyHeroMod.content.Quirks.Overclock;

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

            

            

           

           
        }
    }
}