using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles.ContinuousBlast.HellSpider
{
    public class HellSpiderProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";
        
        public override void SetDefaults()
        {
            Projectile.width = 14; 
            Projectile.height = 14;
            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.penetrate = -1; 
            
            
            Projectile.timeLeft = 120; 
            
            Projectile.alpha = 255; 
            Projectile.ignoreWater = false; 
            Projectile.tileCollide = true; 
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10; 
            Projectile.hide = true;
            Projectile.extraUpdates = 2; 
        }

        public override void AI()
        {
            int fireColor = (int)Projectile.ai[1];
            if (fireColor == 0) fireColor = DustID.Torch; 

            if (fireColor == DustID.BlueTorch) 
                Lighting.AddLight(Projectile.Center, 0.1f, 0.4f, 1f);
            else 
                Lighting.AddLight(Projectile.Center, 1f, 0.5f, 0.1f);

            
            Dust core = Dust.NewDustPerfect(Projectile.Center, fireColor, Vector2.Zero, 50, default, 2.0f);
            core.noGravity = true;

            
            if (Main.rand.NextBool(3))
            {
                Vector2 flameDrift = Projectile.velocity * Main.rand.NextFloat(-0.2f, 0.2f) + Main.rand.NextVector2Circular(2f, 2f);
                Dust outerFlame = Dust.NewDustPerfect(Projectile.Center, fireColor, flameDrift, 100, default, 1.2f);
                outerFlame.noGravity = true;
            }

            if (Main.rand.NextBool(10))
            {
                int sparkColor = fireColor == DustID.BlueTorch ? DustID.Frost : DustID.SolarFlare;
                Vector2 sparkVel = Projectile.velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat(0.3f, 0.8f);
                Dust spark = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(6f, 6f), sparkColor, sparkVel, 100, default, 1.0f);
                spark.noGravity = false; 
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int fireColor = (int)Projectile.ai[1];

            if (fireColor == DustID.BlueTorch)
            {
                target.AddBuff(BuffID.Frostburn, 180); 
            }
            else
            {
                target.AddBuff(BuffID.OnFire3, 180); 
            }
        }
    }
}