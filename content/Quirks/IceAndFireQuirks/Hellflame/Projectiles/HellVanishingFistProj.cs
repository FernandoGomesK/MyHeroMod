using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles;
using Terraria;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles
{
   public class HellVanishingFistProj : BaseDashProj
    {
     
        protected override float DashSpeed => 35f; 
        protected override int DashDuration => 15; 
        protected override float HitboxThickness => 90f;

        protected override void SpawnDashVisuals(Player player)
        {
            
            int trailDust = Dust.NewDust(player.position, player.width, player.height, DustID.FireworkFountain_Red, 0f, 0f, 100, default, 1.5f);
            Main.dust[trailDust].noGravity = true;
            Main.dust[trailDust].velocity *= 0.2f;

            
            if (Projectile.ai[0] == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0, 0, 100, default, 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 3f;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            

            if (transPlayer.CurrentStage >= QuirkStage.Advanced)
            {
                player.velocity = Vector2.Zero;

                
                PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 12f, 10f, 15, 1000f, "VanishingFistImpact");
                Main.instance.CameraModifiers.Add(shake);


                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<HellJetBurnController>(),
                Projectile.damage, 
                2f, 
                player.whoAmI,
                60f
            );
            }
            

            target.AddBuff(BuffID.Frostburn, 180);
        }
    }
}