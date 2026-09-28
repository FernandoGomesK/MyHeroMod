using Terraria.ModLoader;
using MyHeroMod.content.Projectiles.Base;

namespace MyHeroMod.content.Projectiles
{
    public class WhoompOnomatopoeia : BaseOnomatopoeia
    {
        public override int Duration => 45;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 160;
            Projectile.height = 36;
        }
    }
}