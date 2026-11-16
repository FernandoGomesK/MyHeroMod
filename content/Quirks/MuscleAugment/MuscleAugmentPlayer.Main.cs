using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.MuscleAugment.Projectiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.MuscleAugment
{
    public class MuscleAugmentPlayer : ModPlayer
    {
        public bool isMuscleAugmentActive = false;

        public override void ResetEffects()
        {
            isMuscleAugmentActive = false;
        }

        public override void PostUpdateMiscEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.MuscleAugmentation))
                return;

            if (isMuscleAugmentActive)
            {
                SpawnMusclePart<BaseBodyProj>(0);

                
                Player.headPosition = new Vector2(0f, -18f);
            }
            else
            {
                Player.headPosition = default;
            }
        }


        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            if (isMuscleAugmentActive)
            {
                PlayerDrawLayers.Torso.Hide();
                PlayerDrawLayers.ArmOverItem.Hide();
                PlayerDrawLayers.ArmorLongCoat.Hide();

            }
        }

        private void SpawnMusclePart<T>(int damage) where T : ModProjectile
        {
            int projType = ModContent.ProjectileType<T>();
            if (Player.ownedProjectileCounts[projType] < 1)
            {
                Projectile.NewProjectile(
                    Player.GetSource_FromThis(),
                    Player.Center,
                    Vector2.Zero,
                    projType,
                    damage,
                    0f,
                    Player.whoAmI
                );
            }
        }
    }
}