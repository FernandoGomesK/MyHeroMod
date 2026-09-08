using Terraria.ModLoader;
using MyHeroMod.content.Items.QuirkItems.QuirkGenes;

namespace MyHeroMod.content.Items.QuirkItems.QuirkSyringes
{
    public class DecaySyringe : SpecificQuirkSyringe
    {
        public override QuirkType TargetQuirk => QuirkType.Decay;
        public override int RequiredGeneType => ModContent.ItemType<DecayGene>();
    }
}