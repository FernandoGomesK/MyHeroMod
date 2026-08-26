using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow;
using Terraria.ID;
using MyHeroMod.content.Quirks.BlackWhip;

namespace MyHeroMod.content.Buffs
{
    public class AutomaticWhipBuff : ModBuff
    {
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            var whipPlayer = player.GetModPlayer<BlackWhipPlayer>();

            
            whipPlayer.isAutomaticWhipActive = true;

           

               
            
}
    }}