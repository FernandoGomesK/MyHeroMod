using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Hardening.Projectiles
{
    public class DetroitSmashMeleeProj : BaseDashProj
    {

        protected override float DashSpeed 
        {
            get
            {
                if (Projectile.owner >= 0 && Projectile.owner < Main.maxPlayers)
                {
                    Player player = Main.player[Projectile.owner];
                    var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
                    var projSpeedModifier = ofaPlayer.percentage switch
                    {
                        5 => 20f,
                        10 => 25f,
                        20 => 35,
                        45 => 50f,
                        100 => 65f,
                        _ => 65f
                    };
                    return projSpeedModifier;
                }
                return 40f;
            }
        }


       

        protected override int DashDuration => 20;
        protected override float HitboxThickness => 90f;
        private bool hasTriggeredSmash;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.NewProjectile(
                source,
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<DashRingProj>(),
                0, 
                0f,
                player.whoAmI
            );
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Nullify the dash's own contact damage so only the spawned smash deals damage
            modifiers.FinalDamage *= 0f;
            modifiers.DisableCrit();
        }

        protected override void SpawnDashVisuals(Player player)
        {

            int trailDust = Dust.NewDust(player.position, player.width, player.height, DustID.Ash, 0f, 0f, 100, default, 1.5f);
            Main.dust[trailDust].noGravity = true;
            Main.dust[trailDust].velocity *= 0.2f;


            if (Projectile.ai[0] == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, DustID.Stone, 0, 0, 100, default, 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 3f;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!hasTriggeredSmash)
            {
                hasTriggeredSmash = true;
                Player player = Main.player[Projectile.owner];
                Vector2 direction = Projectile.velocity.SafeNormalize(Vector2.UnitX * player.direction);

                // Stop the player's dash momentum on impact so you don't overshoot through the boss
                player.velocity *= 0.25f;

                ChargeDetroitProj.TriggerDetroitSmash(
                    player,
                    direction,
                    damageScale: 0.6f,
                    singleTarget: true,
                    directTarget: target
                );
            }

            Projectile.Kill();
        }
    }
}