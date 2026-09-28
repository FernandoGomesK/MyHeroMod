using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.QuirkItems.QuirkEssences
{
    public class ExplosionEssence : BaseQuirkEssence
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            EssenceQuirkType = QuirkType.Explosion;
        }
    }
}