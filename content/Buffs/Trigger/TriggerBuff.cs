using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Dusts;

namespace MyHeroMod.content.Buffs.Trigger
{
    public class TriggerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

                player.moveSpeed += 1.0f;
                player.jumpSpeedBoost += 1f;
                player.GetDamage(DamageClass.Generic) += 0.10f;
                player.GetAttackSpeed(DamageClass.Generic) += 0.10f;
                player.noFallDmg = true;
        }
    }
}