using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow;
using Terraria.ID;

namespace MyHeroMod.content.Buffs
{
    public class AutomaticDarkShadowBuff : ModBuff
    {
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            var darkShadow = player.GetModPlayer<DarkShadowPlayer>();

            // Color shadowColor = new Color(24, 0, 33);


            darkShadow.isDarkShadowAutomatic = true;
            
            //     player.moveSpeed += 1.5f; 
            //     player.statDefense += 2;    
            //     player.jumpSpeedBoost += 2.0f;
            //     player.noFallDmg = true;
            //     for (int i = 0; i < 3; i++) 
            //         {
            //             int dustIndex = Dust.NewDust(player.position, player.width, player.height, DustID.Shadowflame, 0f, 0f, 100, shadowColor, 1.5f);
            //             if (dustIndex >= 0)
            //             {
            //                 Dust dust = Main.dust[dustIndex];
            //                 dust.noGravity = true;
            //                 dust.velocity *= 0.3f; 
            //             }
            //         }
            
}
    }}