using KhacesCore.Content.System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Utilities;
using MyHeroMod.content.System.BaseProjectiles;

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
        
     
        protected virtual SoundStyle? ChannelSound => null; 
        
      
        private SlotId _loopSoundSlot;

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

            bool isHolding = CoreKeybinds.SkillSlot1.Current || CoreKeybinds.SkillSlot2.Current || 
                             CoreKeybinds.SkillSlot3.Current || CoreKeybinds.SkillSlot4.Current;

            if (player.dead || !player.active || !isHolding || !IsChannelingValid(player))
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

            
            if (Projectile.ai[0] == 0 && ChannelSound.HasValue)
            {
                SoundStyle loopedStyle = ChannelSound.Value;
                loopedStyle.IsLooped = true; 
                _loopSoundSlot = SoundEngine.PlaySound(loopedStyle, player.Center);
            }

            
            if (SoundEngine.TryGetActiveSound(_loopSoundSlot, out var activeSound))
            {
                activeSound.Position = player.Center;
            }
           
            Projectile.ai[0]++; 

            if (Projectile.ai[0] % FireRate == 0)
            {
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
        
        
        public override void OnKill(int timeLeft)
        {
            if (SoundEngine.TryGetActiveSound(_loopSoundSlot, out var activeSound))
            {
                activeSound.Stop();
            }
        }

        protected virtual bool IsChannelingValid(Player player) => true;

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueFlamethrower
{
    public class BlueFlamethrowerProj : BaseStreamController
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";

        protected override int ParticleType => ModContent.ProjectileType<BlueFlamethrowerHitboxProj>();
        protected override int FireRate => 5; 
        protected override int ParticlesPerShot => 2;
        protected override float BaseSpeed => 10f;
        protected override float SpeedVariance => 2.5f;
        protected override float SpreadAngle => 15f;
        protected override SoundStyle? ChannelSound => new SoundStyle("MyHeroMod/Assets/Sounds/CremationSound");
    }
}