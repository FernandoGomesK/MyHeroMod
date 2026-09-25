using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Explosion;
using MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot;
using MyHeroMod.content.Projectiles;
using Terraria.Graphics.CameraModifiers;
using MyHeroMod.content.Quirks.FierceWings;
using MyHeroMod.content.Quirks.FierceWings.Projectiles;


public class PiercingFeatherSkill : QuirkBaseSkill
{
    public override string Name => "Piercing Feather";
    public override string Description => "Shoot a single high speed feather";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Explosion/ApShotIcon";

    public override string Category => "Fierce Wings";

    public override int BaseCooldown => 300;

    public override QuirkType RequiredQuirk => QuirkType.FierceWings;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;



    public override void OnUse(Player player)
    {

        var transPlayer = player.GetModPlayer<TransformationPlayer>();

        var fiercePlayer = player.GetModPlayer<FierceWingsPlayer>();
        if (fiercePlayer.currentFeathers <= 20)
        {
            return;
        }
        else
        {



            float damageMultiplier = 1.0f;
            int MaxDamage = 45;


            switch (transPlayer.CurrentStage)
            {
                case QuirkStage.Initial:
                    MaxDamage = 45;
                    break;

                case QuirkStage.Adequation:
                    MaxDamage = 45;
                    break;

                case QuirkStage.Intermediate:
                    MaxDamage = 60;
                    break;

                case QuirkStage.Advanced:
                    MaxDamage = 90;
                    break;

                case QuirkStage.Final:
                    MaxDamage = 180;
                    break;

                default:
                    MaxDamage = 45;
                    break;

            }

            //if (player.HasBuff(ModContent.BuffType<ClusterBuff>()))
            //{
            //    damageMultiplier = 2.5f;
            //}

            var finalDamage = (int)(damageMultiplier * MaxDamage);


            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 45f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<RedFeatherProj>(),
                finalDamage,
                2f,
                player.whoAmI
            );

            fiercePlayer.RemoveFeathers(10);
        }
    }
}