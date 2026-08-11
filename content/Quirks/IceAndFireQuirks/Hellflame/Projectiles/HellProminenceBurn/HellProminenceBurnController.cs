using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System.BaseProjectiles;


namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles
{
    public class HellProminenceBurnController : BaseLaserProj
    {
        protected override float MaxRange => 1200f;
        protected override float BeamWidth => 500f;
        protected override int DustType => DustID.FireworkFountain_Red;
        protected override float DustScale => 6.5f;
        protected override int HitCooldown => 15;

  
        protected int ParticleType => ModContent.ProjectileType<HellProminceBurnEffectProj>();
        protected float BaseSpeed => 30f;
        protected float SpeedVariance => 5f;
        protected float SpreadAngle => 25f; 
        protected int ParticlesPerShot => 4;

        protected override bool IsChannelingValid(Player player)
        {
            if (Projectile.ai[0] > 0)
            {
                Projectile.ai[0]--; 
                
                if (Projectile.ai[0] <= 0) 
                {
                    return false;
                }
                
                return player.active && !player.dead; 
            }

            return player.active && !player.dead && player.channel;
        }
    
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 300);
        }

       protected override void SpawnBeamDust(Player player)
{
    Vector2 startPoint = player.Center;
    Vector2 perpendicular = new Vector2(-Projectile.velocity.Y, Projectile.velocity.X);

    Lighting.AddLight(player.Center, 0.3f, 0.6f, 1.2f); 

 
    for (int i = 0; i < 5; i++) 
    {
        Vector2 handOffset = Projectile.velocity * 30f;
        Dust origin = Dust.NewDustPerfect(startPoint + handOffset + Main.rand.NextVector2Circular(80f, 80f), DustID.Torch, Projectile.velocity * 30f, 0, default, 5.5f);
        origin.noGravity = true;
    }

    
    for (int i = 0; i < 5; i++) 
    {
        float lengthOffset = Main.rand.NextFloat(0, MaxRange);
        Vector2 corePos = startPoint + (Projectile.velocity * lengthOffset);
        
        corePos += Main.rand.NextVector2Circular(40f, 40f); 
        
        Dust coreDust = Dust.NewDustPerfect(corePos, DustID.RedTorch, Projectile.velocity * Main.rand.NextFloat(35f, 45f), 0, default, 6.8f);
        coreDust.noGravity = true;
    }

  
    for (int i = 0; i < 10; i++) 
    {
        float lengthOffset = Main.rand.NextFloat(0, MaxRange);
        
     
        float outerWidthOffset = Main.rand.NextFloat(-BeamWidth / 2f, BeamWidth / 2f);
        
     
        Vector2 outerPos = startPoint + (Projectile.velocity * lengthOffset) + (perpendicular * outerWidthOffset);
        
        Dust blueDust = Dust.NewDustPerfect(outerPos, DustID.Torch, Vector2.Zero, 100, default, 6.5f);
        blueDust.noGravity = true;
        blueDust.velocity = (Projectile.velocity * Main.rand.NextFloat(20f, 45f)) + (perpendicular * (outerWidthOffset > 0 ? 3f : -3f));
    }
    
    
    if (Main.rand.NextBool(2))
    {
        float randomOffset = Main.rand.NextFloat(0, MaxRange);
        float randomWidth = Main.rand.NextFloat(-BeamWidth / 2f, BeamWidth / 2f);
        Vector2 sparkPos = startPoint + (Projectile.velocity * randomOffset) + (perpendicular * randomWidth);
        
        Dust spark = Dust.NewDustPerfect(sparkPos, DustID.FireworkFountain_Red, Vector2.Zero, 0, default, 8.5f);
        spark.noGravity = true;
        spark.velocity = Projectile.velocity.RotatedByRandom(0.3f) * Main.rand.NextFloat(30f, 50f);
    }

    
    if (Projectile.owner == Main.myPlayer && Main.GameUpdateCount % 4 == 0)
    {
        for (int i = 0; i < ParticlesPerShot; i++)
        {
            Vector2 shootVel = Projectile.velocity;
            
            float speed = BaseSpeed + Main.rand.NextFloat(-SpeedVariance, SpeedVariance);
            shootVel *= speed;
            shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(SpreadAngle)); 
            
            Vector2 spawnPos = player.Center + (Projectile.velocity * 45f);

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                spawnPos,
                shootVel,
                ParticleType, 
                Projectile.damage / 3, 
                Projectile.knockBack,
                player.whoAmI
            );
        }
    }

        }
    }
}