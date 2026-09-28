using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.QuirkItems.QuirkEssences
{
    public class BlackwhipEssence : BaseQuirkEssence
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            EssenceQuirkType = QuirkType.BlackWhip;
        }
    }
}