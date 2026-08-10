using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.CameraModifiers;

namespace MyHeroMod.content.System.BaseProjectiles
{
    public abstract class BaseLaserProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj";

        
        protected virtual float MaxRange => 1000f; 
        protected virtual float BeamWidth => 50f;
        protected virtual int DustType => 0; 
        protected virtual float DustScale => 2f;
        protected virtual int HitCooldown => 10; 

        protected virtual int Duration => 120;

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true; 
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.hide = true; 
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = HitCooldown; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            
            if (!IsChannelingValid(player))
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = player.Center;
            Projectile.timeLeft = 2; 

            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                Projectile.velocity = diff;
                
                Projectile.rotation = Projectile.velocity.ToRotation();
                player.ChangeDir(Main.MouseWorld.X > player.Center.X ? 1 : -1); 
                
                
                player.itemRotation = (Projectile.velocity * player.direction).ToRotation();
                Projectile.netUpdate = true;
            }

            if (Main.GameUpdateCount % 5 == 0)
            {
                PunchCameraModifier rumble = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 2f, 4f, 5, 1000f, "BeamRumble");
                Main.instance.CameraModifiers.Add(rumble);
            }

            SpawnBeamDust(player);
        }

        protected abstract bool IsChannelingValid(Player player);

        protected virtual void SpawnBeamDust(Player player)
        {
            Vector2 startPoint = player.Center;
            Vector2 perpendicular = new Vector2(-Projectile.velocity.Y, Projectile.velocity.X);

            
            for (int i = 0; i < 8; i++) 
            {
                float lengthOffset = Main.rand.NextFloat(0, MaxRange);
                float widthOffset = Main.rand.NextFloat(-BeamWidth / 2f, BeamWidth / 2f);
                
                Vector2 dustPos = startPoint + (Projectile.velocity * lengthOffset) + (perpendicular * widthOffset);

                Dust beamDust = Dust.NewDustPerfect(dustPos, DustType, Vector2.Zero);
                beamDust.noGravity = true;
                beamDust.scale = Main.rand.NextFloat(DustScale * 0.5f, DustScale); 
                beamDust.velocity = Projectile.velocity * Main.rand.NextFloat(2f, 6f); 
            }
        }

        
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 endPoint = player.Center + (Projectile.velocity * MaxRange);
            float collisionPoint = 0f;

        
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, endPoint, BeamWidth, ref collisionPoint))
            {
                return true;
            }
            return false;
        }
    }
}