using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System.BaseIAFProjectiles.SimpleProjectiles.FireBall;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.FlashFreezeHeatWave
{
    public class HeatwaveFireBallProj : BaseFireBallProj
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";
        protected override float ExpansionRate => 4f; 

        public override void SetDefaults()
        {
            base.SetDefaults();
            
            Projectile.width = 120; 
            Projectile.height = 120;
            
            Projectile.localNPCHitCooldown = 20; 
        }
        

        public override void OnKill(int timeLeft)
        {
           
            CreateExplosionEffects(Projectile.Center, 1f);

            
            Vector2 posSecundaria1 = Projectile.Center + Main.rand.NextVector2CircularEdge(120f, 120f);
            Vector2 posSecundaria2 = Projectile.Center + Main.rand.NextVector2CircularEdge(120f, 120f);
            Vector2 posSecundaria3 = Projectile.Center + Main.rand.NextVector2CircularEdge(200f, 200f);

            CreateExplosionEffects(posSecundaria1, 0.7f);
            CreateExplosionEffects(posSecundaria2, 0.7f);
            CreateExplosionEffects(posSecundaria3, 1.0f);

            
            Projectile.position = Projectile.Center;
            Projectile.width = 250; 
            Projectile.height = 250;
            Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);
        }

        private void CreateExplosionEffects(Vector2 center, float size)
        {
            int baseDust = (int)(80 * size);
            
            
            for (int i = 0; i < (int)(40 * size); i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f) * (20f * size);
                int shockwave = Dust.NewDust(center, 0, 0, DustID.SolarFlare, speed.X, speed.Y, 100, default, 3.5f * size);
                Main.dust[shockwave].noGravity = true;
            }

           
            for (int g = 0; g < (int)(12 * size); g++)
            {
                Vector2 goreSpeed = Main.rand.NextVector2Circular(10f * size, 10f * size);
                Gore.NewGore(Projectile.GetSource_Death(), center, goreSpeed, Main.rand.Next(61, 64), 2.0f * size);
            }

            
            for (int i = 0; i < baseDust; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(22f * size, 22f * size); 
                
                
                int dustType = Main.rand.NextBool() ? DustID.Torch : DustID.FlameBurst;

                int idx = Dust.NewDust(center - new Vector2(20), 40, 40, dustType, speed.X, speed.Y, 100, default, 4.0f * size);
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
    }
}