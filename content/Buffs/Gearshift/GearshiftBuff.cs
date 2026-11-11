using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Gearshift;

namespace MyHeroMod.content.Buffs.Gearshift
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

            player.GetModPlayer<GearshiftPlayer>().isGearshiftBuffActive = true;

            player.moveSpeed += 1.0f;
            player.jumpSpeedBoost += 2.0f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.4f;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.8f;
            player.GetDamage(DamageClass.Ranged) += 0.5f;
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
                    Color.Cyan, 
                    0.5f
                );

                Main.dust[dustIndex].velocity.X *= 0.1f;
                Main.dust[dustIndex].velocity.Y = -1.2f;
                Main.dust[dustIndex].noGravity = true;
            }
        }
    }
}