using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.MuscleAugment
{
    public class MuscleHeadLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
            => new BeforeParent(PlayerDrawLayers.Head);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            var modPlayer = drawInfo.drawPlayer.GetModPlayer<MuscleAugmentPlayer>();
            return modPlayer.isMuscleAugmentActive;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            
        }
    }
}