using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.FaJin; 
using Terraria.ID;
using MyHeroMod.content.Quirks.Erasure;
using MyHeroMod.content.Quirks.DarkShadow;

namespace MyHeroMod.content.Buffs
{
    public class DarkShadowBuff : ModBuff
    {
        

        public override void SetStaticDefaults()
        {
            
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var darkShadowPlayer = player.GetModPlayer<DarkShadowPlayer>();

            darkShadowPlayer.isDarkShadowOn = true;

            
            

            
            
        }

        
    }
}