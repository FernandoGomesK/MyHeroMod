using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.SpeedForce;
using Microsoft.Xna.Framework;
using Terraria.ID;

using Terraria.Audio;
using MyHeroMod.content.Quirks.Overclock;
using MyHeroMod.content.Quirks.DarkShadow;



public class ToggleAutomaticShadowSkill : QuirkBaseSkill
{
    public override string Name => "Toggle Automatic Dark Shadow";
    public override string Description => "Summon DarkShadow.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override string Category => "DarkShadow";
    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.DarkShadow;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {
        var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

        if (player.HasBuff(ModContent.BuffType<DarkShadowBuff>()))
        {
            
        

        if (player.HasBuff(ModContent.BuffType < AutomaticDarkShadowBuff>()))
            {
                player.ClearBuff(ModContent.BuffType<AutomaticDarkShadowBuff>());
            }
        else
        {
            player.AddBuff(ModContent.BuffType<AutomaticDarkShadowBuff>(), 360000);
        }
        }
    }
}
