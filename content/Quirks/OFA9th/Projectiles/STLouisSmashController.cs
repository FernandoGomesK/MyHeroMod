using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MyHeroMod.content.Dusts;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Buffs;

// 1. Simplifiquei o namespace para ficar fácil de achar
namespace MyHeroMod.content.Quirks.Explosion.Projectiles
{
    public class STLouisSmashController : ModProjectile
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

                Projectile.width = 5;
                Projectile.height = 5;
                player.velocity.X *= 0.9f; 
                player.velocity.Y = -15f; 
                
                // Animação de Giro
                
                
                // Partículas saindo do player enquanto sobe
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
                // Aqui ele descobre onde está o mouse para descer
                Vector2 dashDirection = Main.MouseWorld - player.Center;
                dashDirection.Normalize();
                
                // VELOCIDADE DO DASH
                float speed = 20f; 
                Projectile.velocity = dashDirection * speed;
                player.velocity = Projectile.velocity; // Aplica no player

                SoundEngine.PlaySound(SoundID.Item14, player.position); // Som de disparo
            }
            // --- FASE 3: O DASH/DESCIDA (Frame 16+) ---
            else
            {
                
                player.velocity = Projectile.velocity;
                
                
                // player.fullRotation = - player.velocity.ToRotation() + MathHelper.PiOver2;
                player.fullRotation = (player.velocity.ToRotation() + MathHelper.PiOver2) + MathHelper.Pi;
                player.fullRotationOrigin = player.Size / 2;

            

                // Rastro de fogo
                // for (int i = 0; i < 3; i++)
                // {
                //     int d = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0, 0, 100, default, 2f);
                //     Main.dust[d].noGravity = true;
                //     Main.dust[d].velocity = -player.velocity * 0.5f; 
                // }

                
                // int d2 =Dust.NewDust(player.position, player.width, player.height, DustID.Ash, 0, 0, 100, default, 6f);
                // Main.dust[d2].noGravity = true;
                // Main.dust[d2].velocity = player.velocity;



                // var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
        
                // if (ofaPlayer.IsClusterActive == true){

                // int d3 =Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<ClusterDust>(), 0, 0, 100, default, 6f);
                // Main.dust[d2].noGravity = true;
                // Main.dust[d2].velocity = player.velocity;
                // }
            }
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            
            // Para o player e reseta a rotação ao bater
            player.velocity = Vector2.Zero;
            player.fullRotation = 0f; 

            SoundEngine.PlaySound(SoundID.Item62, Projectile.position); 

            // Efeito Visual da Explosão
            for (int i = 0; i < 50; i++)
            {
                int fire = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, 0, 0, 100, default, 4f);
                Main.dust[fire].velocity *= 6f;
                Main.dust[fire].noGravity = true;

                int smoke = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 100, default, 3f);
                Main.dust[smoke].velocity *= 4f;
            }
            
            // Dica: Adicione dano em área aqui criando outro projétil de explosão se quiser
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