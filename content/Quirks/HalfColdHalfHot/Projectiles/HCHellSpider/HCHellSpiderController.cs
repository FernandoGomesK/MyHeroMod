using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.HCHellSpider
{
    public class HCHellSpiderController : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false; 
            Projectile.tileCollide = false;
            Projectile.timeLeft = 120;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Apenas morre se o jogador morrer
            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            // Grudar no Jogador e Mirar
            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 diff = Main.MouseWorld - player.MountedCenter;
                diff.Normalize();
                Projectile.velocity = diff;
                player.ChangeDir(Main.MouseWorld.X > player.MountedCenter.X ? 1 : -1);
                Projectile.netUpdate = true;
            }
            
            Projectile.Center = player.MountedCenter;

            // DISPARAR O FOGO
            Projectile.ai[0]++; 
            if (Projectile.ai[0] % 5 == 0) 
            {
                SoundEngine.PlaySound(SoundID.Item34 with { PitchVariance = 0.2f }, player.position);

                if (Projectile.owner == Main.myPlayer)  
                {
                    int projectilecount = 5;
                    float totalangle = MathHelper.ToRadians(50);

                    
                    int damagePerSpider = Projectile.damage / projectilecount; 

                    for (int i = 0; i < projectilecount; i++)
                    {
                        float fraction = (float)i / (projectilecount - 1);
                        float angle = MathHelper.Lerp(-totalangle / 2, totalangle / 2, fraction);

                        Vector2 shootVel = Projectile.velocity.RotatedBy(angle) * 14f;

                        Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            shootVel,
                            ModContent.ProjectileType<HCHellSpiderProj>(),
                            damagePerSpider, 
                            0f,
                            Projectile.owner
                        );
                    }
                }
            }
        }
    }
}