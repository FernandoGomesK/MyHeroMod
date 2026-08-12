using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles.ContinuousBlast.HellSpider
{
    public class HellSpiderController : ModProjectile
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";
        
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

          
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            int dustColor = DustID.Torch; 
            if (transPlayer.HasActiveQuirk(QuirkType.Blueflame))
            {
                dustColor = DustID.BlueTorch; 
            }

        
            Vector2 handPosition = player.MountedCenter + (Projectile.velocity * 30f);
            if (Main.rand.NextBool(2))
            {
                Dust handDust = Dust.NewDustPerfect(
                    handPosition + Main.rand.NextVector2Circular(8f, 8f), 
                    dustColor, 
                    Projectile.velocity * Main.rand.NextFloat(1f, 3f), 
                    100, 
                    default, 
                    Main.rand.NextFloat(1.5f, 2.5f)
                );
                handDust.noGravity = true;
            }

            Projectile.ai[0]++; 

            if (Projectile.ai[0] % 5 == 0) 
            {
                SoundEngine.PlaySound(SoundID.Item34 with { PitchVariance = 0.2f }, player.position);

                if (Projectile.owner == Main.myPlayer)
                {
                    int projectilecount = 5;
                    float totalangle = MathHelper.ToRadians(50);

                    for (int i = 0; i < projectilecount; i++)
                    {
                        float fraction = (float)i / (projectilecount - 1);
                        float angle = MathHelper.Lerp(-totalangle / 2, totalangle / 2, fraction);

                        Vector2 shootVel = Projectile.velocity.RotatedBy(angle);
                        shootVel *= 14f;

                        Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            handPosition, 
                            shootVel,
                            ModContent.ProjectileType<HellSpiderProj>(),
                            Projectile.damage, 
                            0f,
                            Projectile.owner,
                            0f, 
                            dustColor 
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