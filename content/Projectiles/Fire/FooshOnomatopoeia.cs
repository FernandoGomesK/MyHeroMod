using Terraria.ModLoader;
using MyHeroMod.content.Projectiles.Base;

namespace MyHeroMod.content.Projectiles.Fire
{
    public class FooshOnomatopoeia : BaseOnomatopoeia
    {
        public override int Duration => 45; 

        public override void SetDefaults()
        {
            base.SetDefaults(); 
            Projectile.width = 113; 
            Projectile.height = 44; 
        }
    }
}