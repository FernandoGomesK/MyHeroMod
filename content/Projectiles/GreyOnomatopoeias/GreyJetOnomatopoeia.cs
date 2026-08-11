using Terraria.ModLoader;
using MyHeroMod.content.Projectiles.Base;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Projectiles.GreyOnomatopoeias
{
    public class GreyJetOnomatopoeia : BaseOnomatopoeia
    {
        public override int Duration => 45; 
        public override Color TextColor => Color.OrangeRed;

        public override void SetDefaults()
        {
            base.SetDefaults(); 
            Projectile.width = 76; 
            Projectile.height = 36; 
        }
    }
}