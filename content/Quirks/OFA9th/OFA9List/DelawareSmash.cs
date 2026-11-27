using Microsoft.Xna.Framework;
using MyHeroMod.content;
using MyHeroMod.content.Buffs.Float;
using MyHeroMod.content.Buffs.OFA9;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.System;
using Terraria;

using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.OFA9th.Skills
{
    public class DelawareSmashSkill : QuirkBaseSkill
    {
        public override string Name => "Delaware Smash";
        public override string Description => "Propel air forward with a flick of your fingers";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA9th/DelawareSmashIcon";
        public override int BaseCooldown => 300;
        public override string Category => "OneForAll9th";

        public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            bool hasFullCowling = player.HasBuff(ModContent.BuffType<FullCowlingBuff>());

            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            if (!hasFullCowling && ofaPlayer.currentFingers <= 0)
            {
                CombatText.NewText(player.getRect(), Color.Red, "No fingers left!");
                return ;
            }

            int type = ModContent.ProjectileType<ChargeDelawareProj>();
            if (player.ownedProjectileCounts[type] > 0)
                return;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero,
                type,
                0,
                0f,
                player.whoAmI
            );
        }
    }
}