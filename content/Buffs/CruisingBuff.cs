using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Explosion;

namespace MyHeroMod.Buffs
{
    public class CruisingBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/ClusterBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            // var explodePlayer = player.GetModPlayer<ExplosionPlayer>();

            // explodePlayer.IsClusterActive = true;

            // if (explodePlayer.IsClusterActive)
            // {
            //     // Apply Flash Fire Fist effects
            //     player.GetDamage(DamageClass.Melee) += 0.20f; // Increase melee damage by 20%
            //     player.moveSpeed += 3.0f; // Increase movement speed by 20%
            // }
            
            

           

            
        }
    }
}