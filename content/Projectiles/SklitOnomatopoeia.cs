using MyHeroMod.content.Projectiles.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHeroMod.content.Projectiles
{
    public class SklitOnomatopoeia : BaseOnomatopoeia
    {
        public override int Duration => 45;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 102;
            Projectile.height = 36;
        }
    }
}
