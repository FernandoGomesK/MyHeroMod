
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MyHeroMod.content.Projectiles.HellSpider
{
    public class HellSpiderController : ModProjectile
    {
        public override void SetDefaults()
        {
            
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false; 
            Projectile.tileCollide = false;
            Projectile.timeLeft = 120; // Duration
            Projectile.hide = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            
            if (player.dead || !player.active)
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
            
        
            player.itemRotation = (Projectile.velocity * player.direction).ToRotation();

            // shoot flame each 5 frames
            
            Projectile.ai[0]++; 

            if (Projectile.ai[0] % 5 == 0) // 
            {
                // plays the sound weith varied pitch
                SoundEngine.PlaySound(SoundID.Item34 with { PitchVariance = 0.2f }, player.position);

                if (Projectile.owner == Main.myPlayer)
                {
                    int projectilecount = 5;
                    float totalangle = MathHelper.ToRadians(50); // Ângulo total de dispersão

                    for (int i = 0; i < projectilecount; i++)
                    {
                        // Calcula o ângulo para cada projétil
                        float fraction = (float)i / (projectilecount - 1);
                        float angle = MathHelper.Lerp(-totalangle / 2, totalangle / 2, fraction);

                        Vector2 shootVel = Projectile.velocity.RotatedBy(angle);
                        shootVel *= 14f; // Velocidade do projétil

                        Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            shootVel,
                            ModContent.ProjectileType<HellSpiderProj>(),
                            15, // Dano do fogo
                            0f,
                            Projectile.owner
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


