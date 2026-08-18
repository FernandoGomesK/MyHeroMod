using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using MyHeroMod.content.Dusts;
using System;
using Terraria.Graphics.CameraModifiers;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.Explosion.Projectiles
{
    public class HowitzerImpactProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";
        
        public override void SetDefaults()
        {
            Projectile.width = 80; 
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true; 
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 120; 
            Projectile.alpha = 255; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = player.Center;
            player.heldProj = Projectile.whoAmI;
            
            
            if (Projectile.ai[0] < 15)
            {
                Projectile.ai[0]++;

                player.velocity.X *= 0.9f; 
                player.velocity.Y = -15f;  
                
                player.fullRotation += 0.4f * player.direction;
                player.fullRotationOrigin = player.Size / 2;
                
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0, 0, 100, default, 1f);
                }

                
                if (Projectile.ai[0] == 1)
                {
                    PunchCameraModifier shake = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
                    Main.instance.CameraModifiers.Add(shake);
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Explosion2Sound") { Volume = 2.5f, PitchVariance = 0.3f }, Projectile.Center);
                }
            }
            
            else if (Projectile.ai[0] == 15)
            {
                Projectile.ai[0]++;
                
                PunchCameraModifier shake = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
                Main.instance.CameraModifiers.Add(shake);
                
                Vector2 dashDirection = Main.MouseWorld - player.Center;
                dashDirection.Normalize();
                
                float speed = 25f; 
                Projectile.velocity = dashDirection * speed;
                player.velocity = Projectile.velocity; 

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Explosion1Sound") { Volume = 2.5f, PitchVariance = 0.3f }, Projectile.Center);
                
                var quirkPlayer = player.GetModPlayer<ExplosionPlayer>();
                if (quirkPlayer.IsClusterActive)
                {
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Crackle2") { Volume = 1.5f, PitchVariance = 0.3f }, Projectile.Center);
                }
            }
            
            else
            {
                player.velocity = Projectile.velocity;
                player.fullRotation = player.velocity.ToRotation() + MathHelper.PiOver2;
                player.fullRotationOrigin = player.Size / 2;

                
                for (int i = 0; i < 3; i++)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0, 0, 100, default, 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity = -player.velocity * 0.5f; 
                }

                
                int d2 = Dust.NewDust(player.position, player.width, player.height, DustID.Ash, 0, 0, 100, default, 6f);
                Main.dust[d2].noGravity = true;
                Main.dust[d2].velocity = player.velocity;

                var quirkPlayer = player.GetModPlayer<ExplosionPlayer>();
                if (quirkPlayer.IsClusterActive)
                {
                    int d3 = Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<ClusterDust>(), 0, 0, 100, default, 6f);
                    Main.dust[d3].noGravity = true;
                    Main.dust[d3].velocity = player.velocity;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            var quirkPlayer = player.GetModPlayer<ExplosionPlayer>();
            bool isCluster = quirkPlayer.IsClusterActive;
            
        
            player.velocity = Vector2.Zero;
            player.fullRotation = 0f; 

            
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Explosion1Sound") { Volume = 1.5f, PitchVariance = 0.3f }, Projectile.Center); 
            if (isCluster)
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Crackle2") { Volume = 1.5f, PitchVariance = 0.3f }, Projectile.Center);
            }

            
            float shakeIntensity = isCluster ? 30f : 20f;
            PunchCameraModifier shake = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2CircularEdge(1f, 1f), shakeIntensity, 15f, 20, 2000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        
            ImpactFrameSystem.Trigger(Color.White, false, "MyHeroMod/Assets/Effects/BlankImpactImage", "MyHeroMod/Assets/Effects/HowitzerCluster/HowitzerCluster1",
             "MyHeroMod/Assets/Effects/HowitzerCluster/HowitzerCluster2", "MyHeroMod/Assets/Effects/HowitzerCluster/HowitzerCluster3",
             "MyHeroMod/Assets/Effects/HowitzerCluster/HowitzerCluster4","MyHeroMod/Assets/Effects/HowitzerCluster/HowitzerCluster5"   );
            

            
            float scaleMulti = isCluster ? 1.5f : 1.0f;

            
            CreateExplosionEffects(Projectile.Center, scaleMulti, isCluster);

            
            Vector2 posSecundaria1 = Projectile.Center + Main.rand.NextVector2CircularEdge(120f, 120f);
            Vector2 posSecundaria2 = Projectile.Center + Main.rand.NextVector2CircularEdge(120f, 120f);
            Vector2 posSecundaria3 = Projectile.Center + Main.rand.NextVector2CircularEdge(200f, 200f);

            
            
            CreateExplosionEffects(posSecundaria1, scaleMulti * 0.7f, isCluster);
            CreateExplosionEffects(posSecundaria2, scaleMulti * 0.7f, isCluster);
            CreateExplosionEffects(posSecundaria3, scaleMulti * 1.0f, isCluster);

            
            Projectile.position = Projectile.Center;
            Projectile.width = (int)(250 * scaleMulti); 
            Projectile.height = (int)(250 * scaleMulti);
            Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);
        }

        
        private void CreateExplosionEffects(Vector2 center, float sizeMulti, bool isCluster)
        {
            int baseDust = (int)(80 * sizeMulti);
            
            
            for (int i = 0; i < (int)(40 * sizeMulti); i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f) * (20f * sizeMulti);
                int shockwave = Dust.NewDust(center, 0, 0, DustID.SolarFlare, speed.X, speed.Y, 100, default, 3.5f * sizeMulti);
                Main.dust[shockwave].noGravity = true;
            }

            
            for (int g = 0; g < (int)(12 * sizeMulti); g++)
            {
                Vector2 goreSpeed = Main.rand.NextVector2Circular(10f * sizeMulti, 10f * sizeMulti);
                Gore.NewGore(Projectile.GetSource_Death(), center, goreSpeed, Main.rand.Next(61, 64), 2.0f * sizeMulti);
            }

        
            for (int i = 0; i < baseDust; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(22f * sizeMulti, 22f * sizeMulti); 
                
                int dustType = Main.rand.NextBool() ? DustID.Torch : DustID.FlameBurst;
                
                
                if (isCluster && Main.rand.NextBool(3)) 
                {
                    dustType = ModContent.DustType<ClusterDust>();
                }

                int idx = Dust.NewDust(center - new Vector2(20), 40, 40, dustType, speed.X, speed.Y, 100, default, 4.0f * sizeMulti);
                Main.dust[idx].noGravity = true;

                if (Main.rand.NextBool(2))
                {
                    Main.dust[idx].velocity *= 2.0f;
                    Main.dust[idx].scale *= 1.5f;
                }
            } 
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill(); 
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true; 
        }
    }
}