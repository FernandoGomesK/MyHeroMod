using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Projectiles.Base;

namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{
    public class DelawareSmashProj : BaseSimpleProj
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/DelawareSmashProj";

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f);
            }
        }

        public override void AI()
        {
            base.AI(); 
            
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.5f);
            }
        }
    }
}