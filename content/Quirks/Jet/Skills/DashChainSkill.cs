using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Jet;
using MyHeroMod.content.Quirks.Jet.Projectiles;

public class DashChainSkill : QuirkBaseSkill
{
    public override string Name => "Dash Chain";
    public override string Description => "Perform a series of dashes in quick succession";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Jet/DashChainIcon";
    public override string Category => "Jet";

    public override int BaseCooldown => 320;

    public override QuirkType RequiredQuirk => QuirkType.Jet;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var jetPlayer = player.GetModPlayer<JetPlayer>();

        var maxDashRange = 300f;
        var dashDirection = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero);

        float distanceToMouse = Vector2.Distance(player.Center, Main.MouseWorld);
        if (distanceToMouse > maxDashRange)
        {
            distanceToMouse = maxDashRange;
        }

        Vector2 step = dashDirection * 8f;
        int steps = (int)(distanceToMouse / 8f);

        Vector2 finalPosition = player.Center;

        for (int i = 0; i < steps; i++)
        {
            Vector2 nextPos = finalPosition + step;

     
            if (Collision.SolidCollision(nextPos - player.Size / 2f, player.width, player.height))
            {
                break;
            }

            finalPosition = nextPos;
        }

        int MaxDamage = transPlayer.CurrentStage switch
        {
            QuirkStage.Initial => 130,
            QuirkStage.Adequation => 350,
            QuirkStage.Intermediate => 500,
            QuirkStage.Advanced => 950,
            QuirkStage.Final => 2200,
            _ => 130
        };

        var DamageMultiplier = 1f;
        var attackName = "Jet Headbutt!";

        int FinalDamage = (int)(MaxDamage * DamageMultiplier);

        CombatText.NewText(player.getRect(), Color.Cyan, attackName);


        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            finalPosition,
            Vector2.Zero,
            ModContent.ProjectileType<DashChainProj>(),
            FinalDamage,
            10f,
            player.whoAmI
        );

        player.fallStart = (int)(player.position.Y / 16f);

        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/DashChainSound") with { Volume = 0.5f }, player.position);
    }
}