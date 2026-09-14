using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.QuirkItems.QuirkEssences
{
    public class DangersenseEssence : BaseQuirkEssence
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
             EssenceQuirkType = QuirkType.DangerSense;
        }
    }
}  