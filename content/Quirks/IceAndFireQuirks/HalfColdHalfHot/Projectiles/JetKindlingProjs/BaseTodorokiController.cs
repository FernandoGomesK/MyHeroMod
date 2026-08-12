using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System.BaseProjectiles;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.JetKindlingProjs
{
    public abstract class BaseTodorokiController : BaseLaserProj
    {
        protected override float MaxRange => 400f;
        protected override float BeamWidth => 100f;
        protected override int HitCooldown => 15;
    
        protected abstract int OuterDustType { get; }
        protected abstract int CoreDustType { get; }
        protected abstract int SparkDustType { get; }
        protected abstract Vector3 LightColor { get; }
        protected abstract int DebuffType { get; }
        protected abstract int ParticleType { get; }

        protected virtual float BaseSpeed => 30f;
        protected virtual float SpeedVariance => 5f;
        protected virtual float SpreadAngle => 25f; 
        protected virtual int ParticlesPerShot => 2;
        
        protected override int DustType => OuterDustType; 
        protected override float DustScale => 3.5f;

        protected override bool IsChannelingValid(Player player)
        {
            if (Projectile.ai[0] > 0)
            {
                Projectile.ai[0]--; 
                if (Projectile.ai[0] <= 0) return false;
                return player.active && !player.dead; 
            }
            return player.active && !player.dead && player.channel;
        }
    
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(DebuffType, 300);
        }

        protected override void SpawnBeamDust(Player player)
        {
            Vector2 startPoint = player.Center;
            Vector2 perpendicular = new Vector2(-Projectile.velocity.Y, Projectile.velocity.X);

            Lighting.AddLight(player.Center, LightColor); 

            // Hand origin dust
            for (int i = 0; i < 5; i++) 
            {
                Vector2 handOffset = Projectile.velocity * 30f;
                Dust origin = Dust.NewDustPerfect(startPoint + handOffset + Main.rand.NextVector2Circular(30f, 30f), OuterDustType, Projectile.velocity * 30f, 0, default, 3.5f);
                origin.noGravity = true;
            }

            // Core beam dust
            for (int i = 0; i < 20; i++) 
            {
                float lengthOffset = Main.rand.NextFloat(0, MaxRange);
                Vector2 corePos = startPoint + (Projectile.velocity * lengthOffset) + Main.rand.NextVector2Circular(12f, 12f); 
                
                Dust coreDust = Dust.NewDustPerfect(corePos, CoreDustType, Projectile.velocity * Main.rand.NextFloat(35f, 45f), 0, default, 2.8f);
                coreDust.noGravity = true;
            }

            // Outer funnel dust
            for (int i = 0; i < 15; i++) 
            {
                float lengthOffset = Main.rand.NextFloat(0, MaxRange);
                float outerWidthOffset = Main.rand.NextFloat(-BeamWidth / 2f, BeamWidth / 2f);
                
                float funnelFactor = MathHelper.Clamp(lengthOffset / 400f, 0.15f, 1f); 
                outerWidthOffset *= funnelFactor;

                Vector2 outerPos = startPoint + (Projectile.velocity * lengthOffset) + (perpendicular * outerWidthOffset);
                
                Dust outerDust = Dust.NewDustPerfect(outerPos, OuterDustType, Vector2.Zero, 100, default, 3.5f);
                outerDust.noGravity = true;
                outerDust.velocity = (Projectile.velocity * Main.rand.NextFloat(10f, 25f)) + (perpendicular * (outerWidthOffset > 0 ? 3f : -3f));
            }
            
            // Random sparks
            if (Main.rand.NextBool(2))
            {
                float randomOffset = Main.rand.NextFloat(0, MaxRange);
                float randomWidth = Main.rand.NextFloat(-BeamWidth / 2f, BeamWidth / 2f);
                Vector2 sparkPos = startPoint + (Projectile.velocity * randomOffset) + (perpendicular * randomWidth);
                
                Dust spark = Dust.NewDustPerfect(sparkPos, SparkDustType, Vector2.Zero, 0, default, 1.5f);
                spark.noGravity = true;
                spark.velocity = Projectile.velocity.RotatedByRandom(0.3f) * Main.rand.NextFloat(30f, 50f);
            }

            // Hitbox particle spawning
            if (Projectile.owner == Main.myPlayer && Main.GameUpdateCount % 4 == 0)
            {
                for (int i = 0; i < ParticlesPerShot; i++)
                {
                    Vector2 shootVel = Projectile.velocity * (BaseSpeed + Main.rand.NextFloat(-SpeedVariance, SpeedVariance));
                    shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(SpreadAngle)); 
                    
                    Vector2 spawnPos = player.Center + (Projectile.velocity * 45f);

                    Projectile.NewProjectile(
                        player.GetSource_FromThis(), spawnPos, shootVel, ParticleType, 
                        Projectile.damage / 3, Projectile.knockBack, player.whoAmI
                    );
                }
            }
        }
    }
}