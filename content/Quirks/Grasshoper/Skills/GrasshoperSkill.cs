using Microsoft.Xna.Framework;
using MyHeroMod.content;
using MyHeroMod.content.Buffs.Float;
using MyHeroMod.content.Buffs.Grasshoper;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.GeneralSkills;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class GrasshoperSkill : BaseToggleSkill
{
    public override string Name => "Grasshoper";
    public override string Description => "Turn into a Grasshoper! Henshin!";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Float/Float";
    public override string Category => "Grasshoper";
    public override int OnomatopoeiaProjType => ModContent.ProjectileType<HenshinOnomatopoeia>();


    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Grasshoper;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
   
    public override bool IsDefaultSkill => false;

    public override int BuffType => ModContent.BuffType<GrasshoperBuff>();

    public override void OnUse(Player player)
    {
        base.OnUse(player);

        Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0f, 0f, 100, default, 6.0f);
    }
}