using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Hardening.Projectiles
{
    public class RedGauntletProj : BaseDashProj
    {

        protected override float DashSpeed => 40f;
        protected override int DashDuration => 20;
        protected override float HitboxThickness => 90f;

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

            //target.AddBuff(BuffID.Frostburn, 180);
        }
    }
}