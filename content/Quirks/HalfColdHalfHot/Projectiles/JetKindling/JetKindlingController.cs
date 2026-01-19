using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.JetKindling
{
    public class JetKindlingController : ModProjectile
    {
        public override void SetDefaults()
        {
            // Este projétil é invisível e intangível, serve apenas para gerenciar o ataque
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false; // Ele não dá dano, quem dá dano é o fogo que ele cospe
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120; // DURAÇÃO DO ATAQUE: 120 ticks = 2 Segundos
            Projectile.hide = true; // Invisível
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // 1. Manter vivo apenas se o jogador estiver vivo
            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            // 2. Grudar no Jogador e Mirar
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
            
            // Rotação do braço
            player.itemRotation = (Projectile.velocity * player.direction).ToRotation();

            // 3. DISPARAR O FOGO (A cada 5 frames)
            // Projectile.ai[0] é um contador interno automático
            Projectile.ai[0]++; 

            if (Projectile.ai[0] % 5 == 0) // Atira a cada 5 ticks (rápido)
            {
                // Toca o som (com pitch variado para ficar natural)
                SoundEngine.PlaySound(SoundID.Item34 with { PitchVariance = 0.2f }, player.position);

                if (Projectile.owner == Main.myPlayer)
                {
                    // Lança 2 projéteis por vez para espalhar bem
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 shootVel = Projectile.velocity;
                        
                        // Velocidade e Espalhamento (Cone)
                        shootVel *= Main.rand.NextFloat(8f, 13f);
                        shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(15)); 
    
                        
                        
                        // Offset para sair da mão (aprox)
                        Vector2 spawnPos = player.Center + Projectile.velocity * 30f;

                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            spawnPos,
                            shootVel,
                            ModContent.ProjectileType<JetKindlingProj>(), // Chama o foguinho que já criamos
                            25, // Dano
                            1f,
                            player.whoAmI
                        );
                    }
                }
            }
        }
    }
}