using Microsoft.Xna.Framework;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Projectiles.Blueflame;
using MyHeroMod.content.Projectiles.Fire;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueFireball;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Skills
{
    public class MeltdownSkill : QuirkBaseSkill
    {
        public override string Name => "Meltdown";
        public override string Description => "Detonate yourself";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/Blueflame/MeltdownIcon";
        public override string Category => "Blueflame";

        public override int BaseCooldown => 10800;

        public override QuirkType RequiredQuirk => QuirkType.Blueflame;
        public override QuirkStage RequiredStage => QuirkStage.Intermediate;
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.HasActiveQuirk(QuirkType.Blueflame))
            {
                var bluePlayer = player.GetModPlayer<BlueflamePlayer>();
                int BaseDamage = 0;

                switch (transPlayer.CurrentStage)
                {
                    case QuirkStage.Intermediate:
                        BaseDamage = 10000;
                        break;
                    case QuirkStage.Advanced:
                        BaseDamage = 15000;
                        break;
                    case QuirkStage.Final:
                        BaseDamage = 20000;
                        break;
                    default:
                        BaseDamage = 10000;
                        break;
                }

                float currentTemp = MathHelper.Max(0, bluePlayer.Temperature);

                float chargePercentage = currentTemp / bluePlayer.MaxTemperature;

                chargePercentage = MathHelper.Clamp(chargePercentage, 0f, 1f);

                int finalDamage = (int)(BaseDamage * chargePercentage);

                if (finalDamage > 0)
                {
                    var projectileSource = player.GetSource_FromThis();

                    Projectile.NewProjectile(projectileSource, player.Center, Vector2.Zero, ModContent.ProjectileType<BlueflameMassiveExplosion>(), finalDamage, 10f, player.whoAmI);

                    bluePlayer.currentExplosionCount = 0;

                    bluePlayer.Temperature = 0;
                }
            }
        }
    }
}