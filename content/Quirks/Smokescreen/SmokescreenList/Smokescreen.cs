using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;



public class SmokescreenSkill : QuirkSkill
{
    public override string Name => "Smoke Screen";
    public override string Description => "creates a Smokescreen";
    public override string IconPath => "Quirks/Smokescreen/Smokescreen";
    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.SmokeScreen ;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<SmokescreenBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<SmokescreenBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<SmokescreenBuff>(), 3600);
        }
    }
}

