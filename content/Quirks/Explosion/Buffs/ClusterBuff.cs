using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.Explosion.Buffs
{
    public class ClusterBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/ClusterBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var HellPlayer = player.GetModPlayer<ExplosionPlayer>();

            HellPlayer.IsClusterActive = true;

            if (HellPlayer.IsClusterActive)
            {
                // Apply Flash Fire Fist effects
                player.GetDamage(DamageClass.Melee) += 0.20f; // Increase melee damage by 20%
                player.moveSpeed += 2.0f; // Increase movement speed by 20%
            }
            
            

           

            
        }
    }
}