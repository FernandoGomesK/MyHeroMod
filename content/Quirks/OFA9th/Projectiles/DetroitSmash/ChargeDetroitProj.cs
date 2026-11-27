using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs.Fajin;
using MyHeroMod.content.Buffs.Gearshift;
using MyHeroMod.content.Buffs.OFA9;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.FaJin;
using MyHeroMod.content.Quirks.Hardening.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.AirRingExplosion;
using MyHeroMod.content.Quirks.OFA8th;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Quirks.OFA9th.Projectiles.DetroitSmash
{ 
    public class ChargeDetroitProj : BaseChannelingProj
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
                return 80;
            }
        }

        protected override int TapThresholdTicks => 10;
        protected override float TapCooldownRefundPercent => 0.4f;

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
            float dashSpeed = player.GetModPlayer<OneForAll9thPlayer>().GetOfaDashSpeed();

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                direction * dashSpeed,
                ModContent.ProjectileType<DetroitSmashMeleeProj>(),
                1,
                0f,
                player.whoAmI
            );
        }


        public override void OnChargeCancelled(Player player)
        {
            // CombatText.NewText(player.getRect(), Color.Red, "Smash Cancelled!");
        }
        public static void TriggerDetroitSmash(Player player, Vector2 direction, float damageScale = 1f, bool singleTarget = false, NPC directTarget = null)
        {
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();

            bool hurtPlayer = false;
            int maxDamage = ofaPlayer.CalculateStageDamage(150, 400, 600, 1100, 2500);

            float damageMultiplier = 1f;
            if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()))
            {
                damageMultiplier = ofaPlayer.GetFullCowlingMultiplier();
            }
            else
            {
                hurtPlayer = true;
            }

            damageMultiplier += ofaPlayer.ConsumeFaJin(out bool usedFaJin);

            float rawFinalDamage = maxDamage * damageMultiplier * damageScale;
            int finalDamage = (int)player.GetTotalDamage(DamageClass.Melee).ApplyTo(rawFinalDamage);

            string attackName = usedFaJin ? "Faux " : "";
            if (usedFaJin || !hurtPlayer)
                attackName += (damageMultiplier * 100).ToString("0") + "% Detroit Smash";
            else
                attackName += "Detroit Smash";

            if (singleTarget)
                attackName += " (Rush)";

            attackName += player.HasBuff(ModContent.BuffType<GearshiftBuff>()) ? ": Quintuple" : "!";
            CombatText.NewText(player.getRect(), Color.LimeGreen, attackName);

            float projSpeedModifier = ofaPlayer.percentage switch
            {
                5 => 20f,
                10 => 25f,
                20 => 35f,
                45 => 50f,
                100 => 65f,
                _ => 65f
            };

            float finalProjSpeed = projSpeedModifier + (usedFaJin ? 20f : 0f);
            Vector2 velocity = direction * finalProjSpeed;
            Vector2 effectVelocity = direction * 1f;

            Vector2 baseSpawnLocation = singleTarget && directTarget != null
                ? directTarget.Center
                : player.Center + (direction * 90f);

            int numberOfPunches = player.HasBuff(ModContent.BuffType<GearshiftBuff>()) ? 5 : 1;

            for (int i = 0; i < numberOfPunches; i++)
            {
                Vector2 spacing = direction * (25f * i);
                Vector2 currentSpawn = baseSpawnLocation - spacing;

                int projIndex = Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    currentSpawn,
                    velocity,
                    ModContent.ProjectileType<DetroitSmashAnimProj>(),
                    finalDamage,
                    2f,
                    player.whoAmI
                );

                if (singleTarget && projIndex >= 0 && projIndex < Main.maxProjectiles)
                {
                    Main.projectile[projIndex].penetrate = 1;
                }

                Projectile.NewProjectile(player.GetSource_FromThis(), baseSpawnLocation, velocity, ModContent.ProjectileType<PunchAttackProj>(), 0, 0f, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_FromThis(), baseSpawnLocation, effectVelocity, ModContent.ProjectileType<AirRingShotProj>(), 0, 0f, player.whoAmI);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
            }

            int onomatopoeiaType = player.HasBuff(ModContent.BuffType<GearshiftBuff>())
            ? ModContent.ProjectileType<GearDekuDetroitSmashOnomatopoeia>()
            : ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>();

            int textIdx = Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center + new Vector2(0, -80f),
                Vector2.Zero,
                onomatopoeiaType,
                0,
                0f,
                player.whoAmI
            );

            if (textIdx >= 0 && textIdx < Main.maxProjectiles)
            {
                Main.projectile[textIdx].scale = 2.5f; 
            }
            
            if (hurtPlayer)
            {
                ofaPlayer.ApplyRecoilDamage(0.25f);
            }

            float shakeIntensity = singleTarget ? 7f : 10f;
            Main.instance.CameraModifiers.Add(new PunchCameraModifier(
                player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), shakeIntensity, 15f, 20, 1000f, "FullCowlingShake"));
        }

        public override void OnChargeComplete(Player player)
        {
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            Vector2 direction = player.DirectionTo(Main.MouseWorld);

            if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage <= 20)
            {
                Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    player.Center,
                    direction * 15f,
                    ModContent.ProjectileType<DetroitSmashMeleeProj>(),
                    1,
                    0f,
                    player.whoAmI,
                    ai1: 1f 
                );
                return;
            }

            TriggerDetroitSmash(player, direction);
        }
    }
}
    
