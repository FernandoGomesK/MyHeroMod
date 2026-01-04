using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.OFA9th.Buffs
{
    public class GearshiftBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/GearshiftBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            ofaPlayer.isGearshiftBuffActive = true;
            

           

            if (ofaPlayer.isGearshiftActive)
            {
                // Apply Gearshift effects
                player.moveSpeed += 1.0f; // Increase movement speed by 10%
                player.accRunSpeed += 3.0f; // Increase run speed
                player.maxRunSpeed += 3.0f; // Increase max run speed
                player.noFallDmg = true;
            }
        }
    }
}