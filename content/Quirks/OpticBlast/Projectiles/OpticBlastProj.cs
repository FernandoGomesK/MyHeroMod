using Terraria.ModLoader;
using MyHeroMod.content.Projectiles.Base;
using Terraria;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.OpticBlast.Projectiles
{
    public class OpticBlastProj : BaseSimpleProj
    {
        
        
               
        public override void AI()
        {
            base.AI(); 
            
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.5f);
            }
        }
    }
}