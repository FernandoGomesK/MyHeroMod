using Microsoft.Xna.Framework;
using MyHeroMod.content.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Projectiles.Blueflame
{
    public class BlueflameMassiveExplosion : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.None;

        public override void SetDefaults()
        {
            Projectile.width = 1000;  
            Projectile.height = 1000;
            Projectile.friendly = true;
            Projectile.hostile = true; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 3;   
            Projectile.tileCollide = false; 
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (Projectile.timeLeft == 3)
            {
                SoundEngine.PlaySound(SoundID.Item14, Projectile.Center); 

                Vector2 center = Projectile.Center;

                for (int i = 0; i < 150; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f) * 40f;
                    int shockwave = Dust.NewDust(center, 0, 0, DustID.SolarFlare, speed.X, speed.Y, 100, default, 5f);
                    Main.dust[shockwave].noGravity = true;
                }

                for (int g = 0; g < 40; g++)
                {
                    Vector2 goreSpeed = Main.rand.NextVector2Circular(20f, 20f);
                    Gore.NewGore(Projectile.GetSource_FromThis(), center, goreSpeed, Main.rand.Next(61, 64), 3.0f);
                }

                for (int i = 0; i < 300; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(50f, 50f);

                    int dustType = Main.rand.NextBool() ? DustID.BlueTorch : DustID.BlueFairy;

                    int idx = Dust.NewDust(center - new Vector2(20), 40, 40, dustType, speed.X, speed.Y, 100, default, 6.0f);
                    Main.dust[idx].noGravity = true;

                    if (Main.rand.NextBool(2))
                    {
                        Main.dust[idx].velocity *= 1.5f;
                        Main.dust[idx].scale *= 1.5f;
                    }
                }
            }
        }
    }
}