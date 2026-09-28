using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.QuirkItems.QuirkEssences
{
    public class GearshiftEssence : BaseQuirkEssence
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            EssenceQuirkType = QuirkType.Gearshift;
        }
    }
}