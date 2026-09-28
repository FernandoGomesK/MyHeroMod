using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles
{
    public abstract class BaseFlameHitbox : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames;

        protected abstract int FlameDustType { get; }
        protected abstract int SparkDustType { get; }
        protected abstract int FlameDebuff { get; }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.penetrate = 3;
            Projectile.timeLeft = 45; 
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;

            Projectile.alpha = 255; 
        }

        public override void AI()
        {
            
            Projectile.velocity *= 0.95f;
            Projectile.scale += 0.02f;

            Projectile.width = (int)(12 * Projectile.scale);
            Projectile.height = (int)(12 * Projectile.scale);


            if (Main.rand.NextBool(2))
            {
                int core = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, FlameDustType, Projectile.velocity.X * 0.3f, Projectile.velocity.Y * 0.3f, 100, default, Projectile.scale * 1.5f);
                Main.dust[core].noGravity = true;
            }

 
            if (Main.rand.NextBool(3))
            {
                Vector2 outerPos = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width, Projectile.height);
                Dust outer = Dust.NewDustPerfect(outerPos, FlameDustType, Projectile.velocity * 0.1f, 100, default, Projectile.scale * 1.2f);
                outer.noGravity = true;
                outer.velocity += Main.rand.NextVector2Circular(1.5f, 1.5f);
            }

          
            if (Main.rand.NextBool(4))
            {
                Vector2 sparkPos = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width / 2f, Projectile.height / 2f);
                Dust spark = Dust.NewDustPerfect(sparkPos, SparkDustType, Vector2.Zero, 0, default, 1f);
                spark.noGravity = true;
                spark.velocity = Projectile.velocity.RotatedByRandom(0.2f) * Main.rand.NextFloat(1.5f, 3.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(FlameDebuff, 180);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.Knockback *= 0.1f;
        }
    }
}