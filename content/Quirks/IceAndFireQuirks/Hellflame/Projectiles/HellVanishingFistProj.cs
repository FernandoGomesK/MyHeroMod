using Microsoft.Xna.Framework;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Projectiles.GreyOnomatopoeias;
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
        
        private bool hasSpawnedText = false;
        private bool hasSpawnedHitText = false;

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
          
        
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            int textTypeToSpawn = ModContent.ProjectileType<GreyVanishingOnomatopoeia>();
                

            SpawnOnomatopoeia(ref hasSpawnedText, textTypeToSpawn);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

        
            
            
        
            if (Projectile.owner == Main.myPlayer && !hasSpawnedHitText) 
            {
                PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 12f, 10f, 15, 1000f, "VanishingFistImpact");
                Main.instance.CameraModifiers.Add(shake);
            }

            if (transPlayer.CurrentStage >= QuirkStage.Advanced)
            {
                player.velocity = Vector2.Zero;
                if (!hasSpawnedHitText && Projectile.owner == Main.myPlayer)
                {
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

               
                SpawnOnomatopoeia(ref hasSpawnedHitText, ModContent.ProjectileType<GreyJetBurnOnomatopoeia>());
            }
            else
            {
                
                SpawnOnomatopoeia(ref hasSpawnedHitText, ModContent.ProjectileType<GreyFistOnomatopoeia>());
            }

            target.AddBuff(BuffID.Frostburn, 180);
        }
    }
}