using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;

using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueVanishingFist;
using MyHeroMod.content.Quirks.Hardening.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;




public class UnbreakableSkill : QuirkBaseSkill
{

    public override string Name => "Red Riot: Unbreakable";

    public override string Description => "Raise your resistant to the maximum";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Blueflame/BlueVanishIcon";
    public override string Category => "Hardening";

    public override int BaseCooldown => 800;

    public override QuirkType RequiredQuirk => QuirkType.Hardening;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
       


        Vector2 Velocity = Main.MouseWorld - player.Center;
        Velocity.Normalize();
        Velocity *= 0f; 

        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center,
            Velocity,
            ModContent.ProjectileType<ChargeUnbreakableProj>(),
            0,
            2f,
            player.whoAmI
        );





    }
}