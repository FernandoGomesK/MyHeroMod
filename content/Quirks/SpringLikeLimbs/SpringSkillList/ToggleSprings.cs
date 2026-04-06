using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;

public class ToggleSpringsSkill : QuirkSkill
{
    public override string Name => "Spring Like Limbs";
    public override string Description => "Allow your body to behave like Springs, increasing your jump height and allowing you to bounce off the ground";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.SpringLikeLimbs;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<SpringLikeLimbsBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<SpringLikeLimbsBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<SpringLikeLimbsBuff>(), 36000000);
             
        }
    }
}