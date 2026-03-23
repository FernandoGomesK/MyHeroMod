using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.JetBurn;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.JetBurn
{
    public class JetKindlingController : ModProjectile
    {
        public override void SetDefaults()
        {
            
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false; 
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120; 
            Projectile.hide = true; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

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

            
                
            int dustColor = DustID.Torch;
            if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames) && transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                dustColor = DustID.BlueTorch;
            }

        
            for (int d = 0; d < 3; d++)
            {
                
                Vector2 dustVel = Projectile.velocity; 
                
                
                dustVel = dustVel.RotatedByRandom(MathHelper.ToRadians(20));
                
                
                dustVel *= Main.rand.NextFloat(6f, 12f);

                
                Vector2 spawnPos = player.Center + (Projectile.velocity * 20f);

                int dust = Dust.NewDust(spawnPos, 10, 10, dustColor, dustVel.X, dustVel.Y, 100, default, 2.5f);
                Main.dust[dust].noGravity = true; 
                Main.dust[dust].velocity = dustVel;
            }

            if (Projectile.ai[0] % 22 == 0)
            {
                
                SoundEngine.PlaySound(SoundID.Item34 with { PitchVariance = 0.2f }, player.position);

                if (Projectile.owner == Main.myPlayer)
                {
                    
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 shootVel = Projectile.velocity;
                        
                        // Velocidade e Espalhamento (Cone)
                        shootVel *= Main.rand.NextFloat(8f, 13f);
                        shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(15)); 
    
                        
                        
                        
                        Vector2 spawnPos = player.Center + Projectile.velocity * 30f;

                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            spawnPos,
                            shootVel,
                            ModContent.ProjectileType<JetKindlingProj>(), // Chama o foguinho que já criamos
                            Projectile.damage, // Dano
                            1f,
                            player.whoAmI
                        );
                    }
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}