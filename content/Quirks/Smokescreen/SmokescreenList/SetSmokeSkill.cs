using Microsoft.Xna.Framework;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.GeneralSkills;
using MyHeroMod.content.Quirks.Smokescreen.Projectiles;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ModLoader;

public class SetSmokeSkill : QuirkBaseSkill
{
    public override string Name => "Set Smokescreen";
    public override string Description => "Creates Smoke in a set place";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Smokescreen/ToggleSmokescreenIcon";
    public override string Category => "Smokescreen";
    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.SmokeScreen;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override QuirkStage RequiredOfaStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        if (player.whoAmI != Main.myPlayer)
            return;

        QuirkStage stage = player.GetModPlayer<TransformationPlayer>().CurrentStage;

        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center,
            Vector2.Zero,
            ModContent.ProjectileType<SmokePointProj>(),
            0,
            0f,
            player.whoAmI,
            ai0: SmokePointProj.GetDuration(stage) 
        );
    }
}