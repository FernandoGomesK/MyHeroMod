using Microsoft.Xna.Framework;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.Hardening.Projectiles;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueVanishingFist;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;




public class RadGauntletSkill : QuirkBaseSkill
{

    public override string Name => "Red Gauntlet";

    public override string Description => "Dash and punch through a foe";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Blueflame/BlueVanishIcon";
    public override string Category => "Hardening";

    public override int BaseCooldown => 320;

    public override QuirkType RequiredQuirk => QuirkType.Hardening;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        int BaseDamage = 0;

        switch (transPlayer.CurrentStage)
        {
            case QuirkStage.Initial:
                BaseDamage = 90;
                break;

            case QuirkStage.Adequation:
                BaseDamage = 140;
                break;

            case QuirkStage.Intermediate:
                BaseDamage = 180;
                break;

            case QuirkStage.Advanced:
                BaseDamage = 250;
                break;

            case QuirkStage.Final:
                BaseDamage = 350;
                break;

            default:
                BaseDamage = 20;
                break;

        }

        float modifiedDamage = 1f;


        int finalDamage = (int)(BaseDamage * modifiedDamage);

        Vector2 textPosition = player.Center + new Vector2(0, -60f);
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            textPosition,
            Vector2.Zero,
            ModContent.ProjectileType<WhoompOnomatopoeia>(),
            0,
            0f,
            player.whoAmI
        );


        Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<RedGauntletProj>(),
                finalDamage,
                2f,
                player.whoAmI
            );
           
        



    }
}