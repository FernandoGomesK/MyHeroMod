using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Buffs.Gearshift
{
    public class ChangedGearBuff : ModBuff
    {
        #region Tuning

        /// <summary>Color of the armor tint and of the light emitted by the player.</summary>
        public static readonly Color GearColor = Color.RoyalBlue;

        /// <summary>Intensity of the light emitted by the player (1 = full GearColor).</summary>
        public const float LightIntensity = 0.8f;

        #endregion

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.moveSpeed += 4.0f;
            player.jumpSpeedBoost += 2.0f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.4f;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.8f;
            player.noFallDmg = true;

            player.armorEffectDrawOutlines = true;

            player.GetModPlayer<AfterimagePlayer>().Request(AfterimagePalettes.Blue);

            Lighting.AddLight(player.Center, GearColor.ToVector3() * LightIntensity);

            if (Main.netMode != NetmodeID.Server && Main.rand.NextBool(2))
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

    /// <summary>
    /// Tints the armor while <see cref="ChangedGearBuff"/> is active.
    /// Draw colors can only be changed from a draw hook, not from the buff itself.
    /// </summary>
    public class ChangedGearDrawPlayer : ModPlayer
    {

        public const float TintStrength = 0.6f;

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (!Player.HasBuff<ChangedGearBuff>())
                return;
            Vector4 tint = Vector4.Lerp(Vector4.One, ChangedGearBuff.GearColor.ToVector4(), TintStrength);

            drawInfo.colorArmorHead = new Color(drawInfo.colorArmorHead.ToVector4() * tint);
            drawInfo.colorArmorBody = new Color(drawInfo.colorArmorBody.ToVector4() * tint);
            drawInfo.colorArmorLegs = new Color(drawInfo.colorArmorLegs.ToVector4() * tint);
        }
    }
}