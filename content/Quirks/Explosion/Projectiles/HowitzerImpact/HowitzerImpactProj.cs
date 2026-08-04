using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MyHeroMod.content.Dusts;
using System;
using Terraria.Graphics.CameraModifiers;

// 1. Simplifiquei o namespace para ficar fácil de achar
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
            Projectile.alpha = 255; // Invisível
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
                PunchCameraModifier shake = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
                Main.instance.CameraModifiers.Add(shake);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Explosion2Sound") { Volume = 2.5f, PitchVariance = 0.3f }, Projectile.Center);
                
            }
        
            else if (Projectile.ai[0] == 15)
            {
                PunchCameraModifier shake = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
                Main.instance.CameraModifiers.Add(shake);
                Projectile.ai[0]++;
                
                
                Vector2 dashDirection = Main.MouseWorld - player.Center;
                dashDirection.Normalize();
                
                
                float speed = 25f; 
                Projectile.velocity = dashDirection * speed;
                player.velocity = Projectile.velocity; 

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Explosion1Sound") { Volume = 2.5f, PitchVariance = 0.3f }, Projectile.Center);
                var quirkPlayer = player.GetModPlayer<ExplosionPlayer>();
        
                if (quirkPlayer.IsClusterActive == true){
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

                
                int d2 =Dust.NewDust(player.position, player.width, player.height, DustID.Ash, 0, 0, 100, default, 6f);
                Main.dust[d2].noGravity = true;
                Main.dust[d2].velocity = player.velocity;



                var quirkPlayer = player.GetModPlayer<ExplosionPlayer>();
        
                if (quirkPlayer.IsClusterActive == true){

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Crackle2") { Volume = 1.5f, PitchVariance = 0.3f }, Projectile.Center);
                int d3 =Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<ClusterDust>(), 0, 0, 100, default, 6f);
                Main.dust[d2].noGravity = true;
                Main.dust[d2].velocity = player.velocity;
                }
                PunchCameraModifier shake = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
                Main.instance.CameraModifiers.Add(shake);
            }
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            
        
            player.velocity = Vector2.Zero;
            player.fullRotation = 0f; 

            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Explosion1Sound") { Volume = 1.5f, PitchVariance = 0.3f }, Projectile.Center); 

            
            Projectile.position = Projectile.Center;
            Projectile.width = 250; 
            Projectile.height = 250;
            Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);

            
            for (int i = 0; i < 50; i++)
            {
                double angle = Main.rand.NextDouble() * 2.0 * Math.PI;
                float speed = 20f; 
                Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
                
                int shockwaveDust = Dust.NewDust(Projectile.Center, 0, 0, DustID.SolarFlare, velocity.X, velocity.Y, 100, default, 3.5f);
                Main.dust[shockwaveDust].noGravity = true;
                Main.dust[shockwaveDust].velocity = velocity;
            }

        
            for (int g = 0; g < 15; g++)
            {
                Vector2 goreSpeed = Main.rand.NextVector2Circular(10f, 10f);
                Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, goreSpeed, Main.rand.Next(61, 64), 2.5f);
            }

        
            for (int i = 0; i < 70; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(22f, 22f); 
                
            
                int dustType = Main.rand.NextBool() ? DustID.Torch : DustID.FlameBurst;
                
                int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, speed.X, speed.Y, 100, default, 4.5f);
                Main.dust[idx].noGravity = true;

        
                if (Main.rand.NextBool(2))
                {
                    Main.dust[idx].velocity *= 2.5f;
                    Main.dust[idx].scale *= 1.8f;
                }
            } 

            
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ash, speed.X, speed.Y, 100, default, 2.5f);
            }

            PunchCameraModifier shake = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
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