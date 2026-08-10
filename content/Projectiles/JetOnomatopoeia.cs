using Terraria.ModLoader;
using MyHeroMod.content.Projectiles.Base;

namespace MyHeroMod.content.Projectiles
{
    public class JetOnomatopoeia : BaseOnomatopoeia
    {
        public override int Duration => 45; 

        public override void SetDefaults()
        {
            base.SetDefaults(); 
            Projectile.width = 76; 
            Projectile.height = 36; 
        }
    }
}