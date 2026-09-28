using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.Graphics;
using MyHeroMod.content.Buffs;
using KhacesCore.Content.System.BaseProjectiles;
using MyHeroMod.content.Quirks.FierceWings;
using MyHeroMod.content.Quirks.FierceWings.Projectiles;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot
{
    public class FeatherBarrageProj : BaseChannelingProj
    {
 
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";

        protected override int ChannelTime => 120;

        public override void AI()
        {
            base.AI();

            if (Projectile.ai[0] >= 1)
            {
                Player player = Main.player[Projectile.owner];
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                var fiercePlayer = player.GetModPlayer<FierceWingsPlayer>();

                float damageMultiplier = 1.0f;
                int MaxDamage = 45;

                switch (transPlayer.CurrentStage)
                {
                    case QuirkStage.Initial:
                    case QuirkStage.Adequation:
                        MaxDamage = 45;
                        break;
                    case QuirkStage.Intermediate:
                        MaxDamage = 60;
                        break;
                    case QuirkStage.Advanced:
                        MaxDamage = 90;
                        break;
                    case QuirkStage.Final:
                        MaxDamage = 180;
                        break;
                    default:
                        MaxDamage = 45;
                        break;
                }

                var finalDamage = (int)(damageMultiplier * MaxDamage);

                if (!player.active || player.dead)
                {
                    Projectile.Kill();
                    return;
                }

                if (Projectile.owner == Main.myPlayer)
                {
                    Vector2 diff = Main.MouseWorld - player.MountedCenter;
                    diff.Normalize();
                    Projectile.velocity = diff;
                    player.ChangeDir(Main.MouseWorld.X > player.MountedCenter.X ? 1 : -1);
                    Projectile.netUpdate = true;
                }

                Projectile.Center = player.MountedCenter;
                player.heldProj = Projectile.whoAmI;
                player.itemTime = 2;
                player.itemAnimation = 2;

                player.velocity *= 0.1f;

                player.itemRotation = (Projectile.velocity * player.direction).ToRotation();

                int shootSpeed = (transPlayer.CurrentStage >= QuirkStage.Advanced) ? 5 : 10;

 
                if (Projectile.ai[0] % shootSpeed == 0)
                {
                    if (Projectile.owner == Main.myPlayer)
                    {
                        if (fiercePlayer.currentFeathers <= 0)
                        {
                            Projectile.Kill();
                            return;
                        }

                        for (int i = 0; i < 1; i++)
                        {
                            Vector2 shootVel = Projectile.velocity;
                            shootVel *= Main.rand.NextFloat(18f, 24f);
                            shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(12));

                            Vector2 spawnPos = player.Center + Projectile.velocity * 60f;

                            Projectile.NewProjectile(
                                player.GetSource_FromThis(),
                                spawnPos,
                                shootVel,
                                ModContent.ProjectileType<RedFeatherProj>(),
                                finalDamage,
                                4f,
                                player.whoAmI
                            );

                            
                            fiercePlayer.RemoveFeathers(5);
                        }
                        SoundEngine.PlaySound(SoundID.Item17, player.Center);
                    }
                }
            }
        }

        public override void SpawnChargingDust(Player player)
        {

        }

        public override void OnChargeCancelled(Player player)
        {

        }

        public override void OnChargeComplete(Player player)
        {
        }
    }
}