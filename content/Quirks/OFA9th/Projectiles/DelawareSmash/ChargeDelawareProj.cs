using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs.Float;
using MyHeroMod.content.Buffs.Gearshift;
using MyHeroMod.content.Buffs.OFA9;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.AirRingExplosion;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.DelawareSmash;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.DetroitSmash;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{
    public enum DelawareVariant
    {
        Raw100,
        Normal,
        Precise,
        Charged,
        AirForceVolley,
        FullPower,
        FinalExplosion
    }

    public class ChargeDelawareProj : BaseChannelingProj
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";

        protected override int ChannelTime
        {
            get
            {
                if (Projectile.owner >= 0 && Projectile.owner < Main.maxPlayers)
                {
                    Player player = Main.player[Projectile.owner];
                    if (player.active && player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
                        return 20;
                }
                return 60; 
            }
        }

        protected override int TapThresholdTicks => 10;
        protected override float TapCooldownRefundPercent => 0.8f;

        public override void AI()
        {
            base.AI();
            Player player = Main.player[Projectile.owner];

            if (player.active && !player.dead)
            {
                player.velocity *= 0.6f;
            }

            if (Projectile.ai[0] == 1)
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
            }
        }

        public override void SpawnChargingDust(Player player)
        {
            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Electric, 0, 0, 100, Color.Green, 0.5f);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }
        }

        public override void OnTapRelease(Player player)
        {
            Vector2 direction = player.DirectionTo(Main.MouseWorld);
            TriggerDelawareSmash(player, direction, isCharged: false);
        }

        public override void OnChargeComplete(Player player)
        {
            Vector2 direction = player.DirectionTo(Main.MouseWorld);
            TriggerDelawareSmash(player, direction, isCharged: true);
        }

        private static DelawareVariant ResolveVariant(bool hasFullCowling, bool isAirForceOn, bool isCharged, bool usedFaJin)
        {
            if (!hasFullCowling)
                return DelawareVariant.Raw100;

            if (usedFaJin)
            {
                if (!isCharged)
                    return DelawareVariant.Charged;

                return isAirForceOn ? DelawareVariant.FinalExplosion : DelawareVariant.FullPower;
            }

            if (isCharged)
                return isAirForceOn ? DelawareVariant.AirForceVolley : DelawareVariant.Charged;

            return isAirForceOn ? DelawareVariant.Precise : DelawareVariant.Normal;
        }

        public static void TriggerDelawareSmash(Player player, Vector2 direction, bool isCharged)
        {
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();

            bool hasFullCowling = player.HasBuff(ModContent.BuffType<FullCowlingBuff>());
            bool hurtPlayer = !hasFullCowling;

            if (hurtPlayer)
            {
                if (ofaPlayer.currentFingers <= 0)
                {
                    CombatText.NewText(player.getRect(), Color.Red, "No fingers left!");
                    return;
                }
                ofaPlayer.currentFingers--;
            }

            int maxDamage = ofaPlayer.CalculateStageDamage(50, 150, 300, 550, 1200);
            float damageMultiplier = hasFullCowling ? ofaPlayer.GetAirForceDamageMultiplier() : 1f;
            damageMultiplier += ofaPlayer.ConsumeFaJin(out bool usedFaJin);

            DelawareVariant variant = ResolveVariant(hasFullCowling, ofaPlayer.isAirForceOn, isCharged, usedFaJin);

            float variantDamageScale = variant switch
            {
                DelawareVariant.Normal => 0.7f,
                DelawareVariant.Precise => 0.85f,
                DelawareVariant.Charged => 1.0f,
                DelawareVariant.AirForceVolley => 0.4f,
                DelawareVariant.FullPower => 1.4f,
                DelawareVariant.FinalExplosion => 1.6f,
                _ => 1.0f // Raw100
            };

            float rawFinalDamage = maxDamage * damageMultiplier * variantDamageScale;
            int finalDamage = (int)player.GetTotalDamage(DamageClass.Melee).ApplyTo(rawFinalDamage);

            // Combat text label
            string prefix = usedFaJin ? "Faux " : "";
            string baseLabel = hasFullCowling
                ? $"{(damageMultiplier * 100):0}% Delaware Smash"
                : "Delaware Smash";

            string suffix = variant switch
            {
                DelawareVariant.Precise => " (Air Force)",
                DelawareVariant.Charged => ": Charged",
                DelawareVariant.AirForceVolley => ": Volley",
                DelawareVariant.FullPower => ": Full Power!",
                DelawareVariant.FinalExplosion => ": Final Smash!",
                _ => "!"
            };

            CombatText.NewText(player.getRect(), Color.LimeGreen, prefix + baseLabel + suffix);

            // Base speed calculation

            float baseSpeed = hurtPlayer ? 30f : (20f + (ofaPlayer.isAirForceOn ? 10f : 0f));
            if (usedFaJin) baseSpeed += 12f;

            Vector2 velocity = direction * baseSpeed;

            // Float propulsion preserved from DelawareSmashSkill
            if (player.HasBuff(ModContent.BuffType<FloatBuff>()))
            {
                player.velocity = -velocity * 1.5f;
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Cloud, velocity.X * 2f, velocity.Y * 2f, 0, default, 1f);
                }
            }

            int delawareType = variant switch
            {
                DelawareVariant.Precise or DelawareVariant.AirForceVolley => ModContent.ProjectileType<PreciseDelawareSmashProj>(),
                DelawareVariant.Charged or DelawareVariant.FullPower or DelawareVariant.FinalExplosion => ModContent.ProjectileType<ChargedDelawareSmashProj>(),
                _ => ModContent.ProjectileType<DelawareSmashProj>()
            };
            int gearshiftBonus = player.HasBuff(ModContent.BuffType<GearshiftBuff>()) ? 2 : 0;

            switch (variant)
            {
                case DelawareVariant.Raw100:
                    {
                        Vector2 spawnPos = player.Center + (direction * 90f);
                        Projectile.NewProjectile(player.GetSource_FromThis(), spawnPos, velocity, ModContent.ProjectileType<DetroitSmashAnimProj>(), finalDamage, 2f, player.whoAmI);
                        Projectile.NewProjectile(player.GetSource_FromThis(), spawnPos, direction * 1f, ModContent.ProjectileType<AirRingShotProj>(), 0, 0f, player.whoAmI);
                        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
                        break;
                    }

                case DelawareVariant.AirForceVolley:
                    {
                        Vector2 spawnPos = player.Center + (direction * 80f);
                        int bulletCount = 4 + gearshiftBonus;
                        float totalSpread = MathHelper.ToRadians(18f);
                        for (int i = 0; i < bulletCount; i++)
                        {
                            float progress = bulletCount > 1 ? (i / (float)(bulletCount - 1)) - 0.5f : 0f;
                            Vector2 spreadDir = direction.RotatedBy(progress * totalSpread);
                            Vector2 spawnOffset = player.Center + (spreadDir * (20f + i * 10f));

                            Projectile.NewProjectile(player.GetSource_FromThis(), spawnOffset, spreadDir * (baseSpeed * 1.15f), delawareType, finalDamage, 2f, player.whoAmI);
                            Projectile.NewProjectile(player.GetSource_FromThis(), spawnPos, direction * 1.5f, ModContent.ProjectileType<AirRingShotProj>(), 0, 0f, player.whoAmI, ai0: 5.0f);
                        }
                        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, player.position);
                        break;
                    }

                case DelawareVariant.FinalExplosion:
                    {
                        Vector2 spawnPos = player.Center + (direction * 80f);
                        int blastIdx = Projectile.NewProjectile(player.GetSource_FromThis(), spawnPos, velocity * 1.2f, delawareType, finalDamage, 4f, player.whoAmI);
                        if (blastIdx >= 0 && blastIdx < Main.maxProjectiles)
                            Main.projectile[blastIdx].scale *= 1.75f;

                        // Open-palm shockwave visuals
                        Projectile.NewProjectile(player.GetSource_FromThis(), spawnPos, velocity, ModContent.ProjectileType<DetroitSmashAnimProj>(), 0, 0f, player.whoAmI);
                        Projectile.NewProjectile(player.GetSource_FromThis(), spawnPos, direction * 1.5f, ModContent.ProjectileType<AirRingShotProj>(), 0, 0f, player.whoAmI, ai0: 5.0f);
                        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.7f }, player.position);
                        break;
                    }

                default: // Normal, Precise, Charged, FullPower
                    {
                        float speedMult = variant switch
                        {
                            DelawareVariant.Precise => 1.25f,
                            DelawareVariant.FullPower => 1.3f,
                            _ => 1.0f
                        };

                        float projScale = variant switch
                        {
                            DelawareVariant.FullPower => 3f,
                            DelawareVariant.Charged => 2.5f,
                            _ => 1.0f
                        };

                        int shots = 1 + (gearshiftBonus > 0 ? 2 : 0);
                        for (int i = 0; i < shots; i++)
                        {
                            Vector2 spawnPos = player.Center + (direction * (20f * i));
                            int idx = Projectile.NewProjectile(player.GetSource_FromThis(), spawnPos, velocity * speedMult, delawareType, finalDamage, 2f, player.whoAmI);
                            if (idx >= 0 && idx < Main.maxProjectiles)
                                Main.projectile[idx].scale *= projScale;
                        }

                        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, player.position);
                        break;
                    }
            }

            int onomatopoeiaType = player.HasBuff(ModContent.BuffType<GearshiftBuff>())
                ? ModContent.ProjectileType<GearDekuDetroitSmashOnomatopoeia>()
                : ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>();

            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center + new Vector2(0, -30f), Vector2.Zero, onomatopoeiaType, 0, 0f, player.whoAmI);

            if (hurtPlayer)
            {
                ofaPlayer.ApplyRecoilDamage(0.05f);
            }

            float shakeStrength = variant == DelawareVariant.FinalExplosion ? 12f : (isCharged ? 9f : 5f);
            Main.instance.CameraModifiers.Add(new PunchCameraModifier(
                player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), shakeStrength, 15f, 20, 1000f, "DelawareSmashShake"));
        }
    }
}