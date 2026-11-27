using Microsoft.Xna.Framework;
using MyHeroMod.content.Dusts;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.DetroitSmash;
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
                    if (player.active)
                        return player.GetModPlayer<OneForAll9thPlayer>().GetOfaDashSpeed();
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
      
            modifiers.FinalDamage *= 0f;
            modifiers.DisableCrit();
        }

        protected override void SpawnDashVisuals(Player player)
        {

            int dustType = ModContent.DustType<ElectricityDust>();

            int trailDust = Dust.NewDust(player.position, player.width, player.height, DustID.Ash, 0f, 0f, 100, default, 1.5f);
            Main.dust[trailDust].noGravity = true;
            Main.dust[trailDust].velocity *= 0.2f;


            if (Projectile.ai[0] == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, dustType, 0, 0, 100, default, 2f);
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

                
                player.velocity *= 0.5f;

                float damageScale = Projectile.ai[1] > 0f ? Projectile.ai[1] : 0.6f;

                ChargeDetroitProj.TriggerDetroitSmash(
                    player,
                    direction,
                    damageScale: damageScale,
                    singleTarget: true,
                    directTarget: target
                );
            }

            Projectile.Kill();
        }
    }
}