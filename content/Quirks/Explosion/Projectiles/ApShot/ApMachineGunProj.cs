using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot
{
    public class ApMachineGunProj : ModProjectile
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
            
            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

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
            
            

            // Atira a cada 3 frames (MUITO RÁPIDO)
            if (Projectile.ai[0] % 10 == 0) 
            {
                if (Projectile.ai[0] % 10 == 0) // Som não toca todo frame pra não travar áudio
                    SoundEngine.PlaySound(SoundID.Item14, player.position);
                    // Som grave e alto

                if (Projectile.owner == Main.myPlayer)
                {
                    // Lança 3 projéteis gigantes por vez com espalhamento
                    for (int i = 0; i < 1; i++)
                    {
                        Vector2 shootVel = Projectile.velocity;
                        
                        // Velocidade Alta (Canhão)
                        shootVel *= Main.rand.NextFloat(18f, 24f); 
                        
                        // Cone de dispersão menor que o JetBurn (foco no dano)
                        shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(12)); 

                        Vector2 spawnPos = player.Center + Projectile.velocity * 40f;

                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            spawnPos,
                            shootVel,
                            ModContent.ProjectileType<ApShotProj>(), 
                            30, 
                            4f,  // KNOCKBACK ALTO
                            player.whoAmI
                        );
                    }
                }
            }
        }
    }
}