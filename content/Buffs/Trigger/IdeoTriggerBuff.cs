using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Dusts;

namespace MyHeroMod.content.Buffs.Trigger
{
    public class IdeoTriggerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

            player.moveSpeed += 2.0f;
            player.jumpSpeedBoost += 2f;
            player.statDefense -= 30;
            player.GetDamage(DamageClass.Generic) += 0.40f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.0f;
            player.noFallDmg = true;
        }
    }
}