using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using MyHeroMod.content.System;
using Terraria.Graphics.CameraModifiers;
using System;

namespace MyHeroMod.content.Quirks.Explosion.Projectiles.StunGrenade
{
    public class StunGrenadeProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 10;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
            Projectile.light = 1.0f;
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeLeft)
        {
            var explodePlayer = Main.player[Projectile.owner].GetModPlayer<ExplosionPlayer>();
            var isCluster = explodePlayer.IsClusterActive;
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position); 
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
            ImpactFrameSystem.Trigger(Color.Orange, false, "MyHeroMod/Assets/Effects/BlankImpactImage", "MyHeroMod/Assets/Effects/BlankImpactImage");
            float shakeIntensity = isCluster ? 30f : 20f;
            PunchCameraModifier shake = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2CircularEdge(1f, 1f), shakeIntensity, 15f, 20, 2000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }   
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.5f);
            }
            if (Main.rand.NextBool(7))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f);
            }

        }

        
    }

    
}