using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Engine.Projectiles;
using MyHeroMod.content.Quirks.Engine;
using MyHeroMod.content.Quirks.Rabbit;
using MyHeroMod.content.Quirks.Rabbit.RabbitProjectiles;
using MyHeroMod.content.Quirks.Jet;
using MyHeroMod.content.Quirks.Hardening.Projectiles;

public class SingleHeadbuttSkill : QuirkBaseSkill
{
    public override string Name => "Jet Headbutt";
    public override string Description => "Dash with your jets, headbutting forward";
    public override string IconPath => "MyHeroMod/Assets/Skills/SingleHeadbutt";
    public override string Category => "Jet";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.Jet;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;


    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var jetPlayer = player.GetModPlayer<JetPlayer>();


        int MaxDamage = transPlayer.CurrentStage switch
        {
            QuirkStage.Initial => 130,
            QuirkStage.Adequation => 350,
            QuirkStage.Intermediate => 500,
            QuirkStage.Advanced => 950,
            QuirkStage.Final => 2200,
            _ => 130
        };





        var extraDamage = 0;
        if (jetPlayer.isIronSolesOn)
        {
            extraDamage = 50;
        }



        var DamageMultiplier = 1f;

        var attackName = "Jet Headbutt!";





        int FinalDamage = (int)(MaxDamage * DamageMultiplier) + extraDamage;


        CombatText.NewText(player.getRect(), Color.Cyan, attackName);


        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center,
            Vector2.Zero,
            ModContent.ProjectileType<SingleHeadbuttProj>(),
            FinalDamage,
            10f,
            player.whoAmI
        );

        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
    }
}