using Terraria.ModLoader;
using MyHeroMod.content.Items.QuirkItems.QuirkGenes;

namespace MyHeroMod.content.Items.QuirkItems.QuirkSyringes
{
    public class ExplosionSyringe : SpecificQuirkSyringe
    {
        public override QuirkType TargetQuirk => QuirkType.Explosion;
        public override int RequiredGeneType => ModContent.ItemType<ExplosionGene>();
    }
}