using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;

public class FloatSkill : QuirkSkill
{
    public override string Name => "Float";
    public override int BaseCooldown => 30;

    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<FloatBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<FloatBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<FloatBuff>(), 3600);
             
        }
    }
}