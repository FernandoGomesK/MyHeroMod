using Terraria.ModLoader;
using MyHeroMod.content.Projectiles.Base;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Projectiles
{
    public class FajinOnomatopoeia : BaseOnomatopoeia
    {
        public override int Duration => 45; 
        public override Color TextColor => Color.White;

        public override void SetDefaults()
        {
            base.SetDefaults(); 
            Projectile.width = 20; 
            Projectile.height = 160; 
        }
    }
}