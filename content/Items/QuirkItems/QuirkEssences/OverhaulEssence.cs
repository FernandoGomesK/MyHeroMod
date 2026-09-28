using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.QuirkItems.QuirkEssences
{
    public class OverhaulEssence : BaseQuirkEssence
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            EssenceQuirkType = QuirkType.Overhaul;
        }
    }
}