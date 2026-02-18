using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.Gearshift;

namespace MyHeroMod.content.Buffs
{
    public class GearshiftBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/GearshiftBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            var gearshiftPlayer = player.GetModPlayer<GearshiftPlayer>();
            gearshiftPlayer.isGearshiftBuffActive = true;
            ofaPlayer.isGearshiftBuffActive = true;

            player.moveSpeed += 1.0f; // Increase movement speed by 10%
            player.accRunSpeed += 5.0f; // Increase run speed
            player.maxRunSpeed += 5.0f; // Increase max run speed
            player.jumpSpeedBoost += 2.0f; // Increase jump speed
            player.noFallDmg = true;

            

            

           

           
        }
    }
}