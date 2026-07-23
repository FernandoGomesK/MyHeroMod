using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Buffs;
using Microsoft.Xna.Framework;
using MyHeroMod.content;

public class DangerSenseSkill : QuirkBaseSkill
{
    public override string Name => "DangerSense";
    public override string Description => "Activates DangerSense Overtime";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.DangerSense;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;
    public override string Category => "DangerSense";

    public override void OnUse(Player player)
    {
        var dsPlayer = player.GetModPlayer<DangerSensePlayer>();

        if (dsPlayer.CurrentStage >= QuirkStage.Adequation)
        {
            player.AddBuff(ModContent.BuffType<OvertimeBuff>(), 300);
            CombatText.NewText(player.getRect(), Color.Yellow, "Overtime!");
        }
    }
    }