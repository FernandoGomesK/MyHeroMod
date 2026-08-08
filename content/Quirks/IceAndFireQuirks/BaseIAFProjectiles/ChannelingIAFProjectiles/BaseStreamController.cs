using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace MyHeroMod.content.System.BaseProjectiles
{
    public abstract class BaseStreamController : ModProjectile
    {
    
        protected abstract int ParticleType { get; }
        protected virtual int FireRate => 5; 
        protected virtual int ParticlesPerShot => 2;
        protected virtual float BaseSpeed => 10f;
        protected virtual float SpeedVariance => 2.5f;
        protected virtual float SpreadAngle => 15f;
        protected virtual SoundStyle? ShootSound => null;

        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj"; 

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false; 
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.hide = true; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            
            if (player.dead || !player.active || !player.channel || !IsChannelingValid(player))
            {
                Projectile.Kill();
                return;
            }

            
            Projectile.timeLeft = 2; 

            
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

            
            Projectile.ai[0]++; 

            if (Projectile.ai[0] % FireRate == 0)
            {
                if (ShootSound.HasValue)
                {
                    SoundEngine.PlaySound(ShootSound.Value, player.position);
                }

                if (Projectile.owner == Main.myPlayer)
                {
                    for (int i = 0; i < ParticlesPerShot; i++)
                    {
                        Vector2 shootVel = Projectile.velocity;
                        
                        
                        float speed = BaseSpeed + Main.rand.NextFloat(-SpeedVariance, SpeedVariance);
                        shootVel *= speed;
                        
                        
                        shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(SpreadAngle)); 
                        
                        
                        Vector2 spawnPos = player.Center + (Projectile.velocity * 30f);

                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            spawnPos,
                            shootVel,
                            ParticleType, 
                            Projectile.damage, 
                            Projectile.knockBack,
                            player.whoAmI
                        );
                    }
                }
            }
        }

        
        protected virtual bool IsChannelingValid(Player player) => true;

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}