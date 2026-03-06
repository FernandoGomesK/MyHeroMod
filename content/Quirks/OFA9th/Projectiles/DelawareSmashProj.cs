using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{
    public class DelawareSmashProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/DelawareSmashProj";
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f);
            }
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.5f);
            }
        }
    }
}
