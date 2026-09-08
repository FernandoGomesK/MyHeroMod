using Terraria.ModLoader;
using MyHeroMod.content.Items.QuirkItems.QuirkGenes;

namespace MyHeroMod.content.Items.QuirkItems.QuirkSyringes
{
    public class OneForAll9thSyringe : SpecificQuirkSyringe
    {
        public override QuirkType TargetQuirk => QuirkType.OneForAll9th;
        public override int RequiredGeneType => ModContent.ItemType<OneForAll9thGene>();
    }
}