using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MyHeroMod.content.Dusts;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Buffs;
using Microsoft.Build.Evaluation;

// 1. Simplifiquei o namespace para ficar fácil de achar
namespace MyHeroMod.content.Quirks.Explosion.Projectiles
{
    public class ManchesterSmashController : ModProjectile
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
            
            // --- FASE 1: SUBIDA (O Pulo) ---
            // Dura 15 frames (0.25 segundos)
            if (Projectile.ai[0] < 15)
            {
                Projectile.ai[0]++;

                
                player.velocity.Y = -15f; 
                player.velocity.X *= 0.9f; 
                Projectile.width = 5;
                Projectile.height = 5;
                
                player.fullRotation += 0.4f * player.direction;
                player.fullRotationOrigin = player.Size / 2;
                
                
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0, 0, 100, default, 1f);
                }
            }
            // --- FASE 2: CÁLCULO DA MIRA (Frame 15) ---
            else if (Projectile.ai[0] == 15)
            {
                Projectile.ai[0]++;
                
                Projectile.width = 80;
                Projectile.height = 80;
                Vector2 dashDirection = Main.MouseWorld - player.Center;
                dashDirection.Normalize();
                
                // VELOCIDADE DO DASH
                
                float speed = 25f;
                if (Projectile.ai[1] == 1f)     
                {
                    speed = 40f;
                }


                Projectile.velocity = dashDirection * speed;
                player.velocity = Projectile.velocity;

                SoundEngine.PlaySound(SoundID.Item14, player.position); 
            }
        
            else
            {
                
                player.velocity = Projectile.velocity;
                
                
                player.fullRotation = (player.velocity.ToRotation() + MathHelper.PiOver2) + MathHelper.Pi;
                player.fullRotationOrigin = player.Size / 2;

                if (Projectile.ai[1] == 1f)
                {
                
                    int d = Dust.NewDust(player.position, player.width, player.height, DustID.RedTorch, 0, 0, 100, Color.Red, 4f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.5f;
                }

            

            }
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            
            
            player.velocity = Vector2.Zero;
            player.fullRotation = 0f; 

            SoundEngine.PlaySound(SoundID.Item62, Projectile.position); 

            if (Projectile.ai[1] == 1f)
                {
                    int smoke = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch, 0, 0, 100, default, 3f);
                    Main.dust[smoke].velocity *= 4f;
                }


            for (int i = 0; i < 50; i++)
            {
                int fire = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0, 0, 100, Color.Green, 4f);
                Main.dust[fire].velocity *= 6f;
                Main.dust[fire].noGravity = true;

                int smoke = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 100, default, 3f);
                Main.dust[smoke].velocity *= 4f;
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill(); // Garante que exploda ao tocar inimigos
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true; // Garante que exploda ao tocar chão/parede
        }
    }
}