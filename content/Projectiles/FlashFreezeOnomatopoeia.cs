using Terraria.ModLoader;
using MyHeroMod.content.Projectiles.Base;

namespace MyHeroMod.content.Projectiles
{
    public class FlashFreezeOnomatopoeia : BaseOnomatopoeia
    {
        public override int Duration => 60; 

        public override void SetDefaults()
        {
            base.SetDefaults(); 
            Projectile.width = 308; 
            Projectile.height = 72; 
        }
    }
}