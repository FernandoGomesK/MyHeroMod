using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.HellFlames.Projectiles.ProminenceBurn
{
    public class ProminenceBurnController : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            
            // DURAÇÃO DO ATAQUE: 300 ticks = 5 Segundos
            Projectile.timeLeft = 300; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            // 1. Grudar e Mirar
            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 diff = Main.MouseWorld - player.MountedCenter;
                diff.Normalize();
                Projectile.velocity = diff;
                player.ChangeDir(Main.MouseWorld.X > player.MountedCenter.X ? 1 : -1);
                Projectile.netUpdate = true;
            }
            
            Projectile.Center = player.MountedCenter;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            
            // Jogador fica quase parado fazendo força
            player.velocity *= 0.1f; 
            
            // Rotação do braço
            player.itemRotation = (Projectile.velocity * player.direction).ToRotation();

            // 2. DISPARO CONTÍNUO (Gatling Gun de Fogo)
            Projectile.ai[0]++;
            
            // Treme a tela (Screen Shake) para dar impacto
            if (Projectile.ai[0] % 5 == 0)
            {
                 // Main.instance.CameraModifiers.Add(...) // (Opcional: Lógica de shake)
            }

            // Atira a cada 3 frames (MUITO RÁPIDO)
            if (Projectile.ai[0] % 2 == 0) 
            {
                if (Projectile.ai[0] % 20 == 0) // Som não toca todo frame pra não travar áudio
                    SoundEngine.PlaySound(SoundID.Item34 with { Pitch = -0.5f, Volume = 1.5f }, player.position); // Som grave e alto

                if (Projectile.owner == Main.myPlayer)
                {
                    // Lança 3 projéteis gigantes por vez com espalhamento
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 shootVel = Projectile.velocity;
                        
                        // Velocidade Alta (Canhão)
                        shootVel *= Main.rand.NextFloat(15f, 25f); 
                        
                        // Cone de dispersão menor que o JetBurn (foco no dano)
                        shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(2)); 

                        Vector2 spawnPos = player.Center + Projectile.velocity * 40f;

                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            spawnPos,
                            shootVel,
                            ModContent.ProjectileType<ProminenceBurnFire>(), 
                            30, // DANO ALTO
                            4f,  // KNOCKBACK ALTO
                            player.whoAmI
                        );
                    }
                }
            }
        }
    }
}