using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.HeavenPiercingWall;


namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.GreatGlacialAegir
{
    public class GreatGlacialAegirController : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 80; 
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false; 
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
            
            Projectile.ai[0]++;

            if (Projectile.ai[0] < 60)
            {
                
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, 0, 0, 100, default, 4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.5f;
            
                player.moveSpeed *= 0.2f;;
            }

            if (Projectile.ai[0] > 60)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, 0, 0, 100, default, 8f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.5f;
            }


            else if (Projectile.ai[0] == 60)
            {
                
            

                // Aqui é onde ele "PULA"
                Vector2 dashDirection = Main.MouseWorld - player.Center;
                dashDirection.Normalize();
                
                // VELOCIDADE DO DASH
                float speed = 25f; 
                Projectile.velocity = dashDirection * speed;
                player.velocity = Projectile.velocity;
                
                
               
            }

            else
            {
                player.velocity = Projectile.velocity;
                player.fallStart = (int)(player.position.Y / 16f);
            }


            // --- FASE 2: CÁLCULO DA MIRA (Frame 15) ---
            if (Projectile.ai[0] > 300)
            {
                player.velocity *= 0.1f;
                Projectile.Kill();
            }


           
             
                
                
                
                

                // Rastro de fogo
            
            }

             public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            player.velocity = Vector2.Zero;

            // Só cria os projéteis se já tiver passado da fase de carga (para não bugar se cancelar antes)
            if (Projectile.ai[0] >= 60)
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/TodorokiIce"), Projectile.position);
                 // Som de impacto de gelo

                // Velocidade base da onda (Horizontal)
                float waveSpeed = 12f;

                // 1. ONDA PARA A DIREITA
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    player.Center, // Sai do jogador
                    new Vector2(waveSpeed, 0), // Velocidade X positiva
                    ModContent.ProjectileType<IceWaveController>(),
                    Projectile.damage, // Usa o mesmo dano do dash
                    Projectile.knockBack,
                    player.whoAmI
                );

                // 2. ONDA PARA A ESQUERDA
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    player.Center, // Sai do jogador
                    new Vector2(-waveSpeed, 0), // Velocidade X negativa
                    ModContent.ProjectileType<IceWaveController>(),
                    Projectile.damage,
                    Projectile.knockBack,
                    player.whoAmI
                );
                
                // Explosão visual de impacto
                for (int i = 0; i < 30; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Ice, Main.rand.NextFloat(-5,5), Main.rand.NextFloat(-5,5), 100, default, 2f);
                }
            }
        }
        }
    }
