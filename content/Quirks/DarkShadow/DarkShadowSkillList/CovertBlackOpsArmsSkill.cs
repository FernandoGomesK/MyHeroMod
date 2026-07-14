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



public class CovertBlackOpsArmsSkill : QuirkSkill
{
    public override string Name => "Covert Black Ops Arms";
    public override string Description => "Enlarge your arms for a temporary boost.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override int BaseCooldown => 600;
    public override QuirkType RequiredQuirk => QuirkType.DarkShadow;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {
        var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            if (player.HasBuff(ModContent.BuffType<BlackAbyssBuff>()))
        {
          player.AddBuff(ModContent.BuffType<CBOArmsBuff>(), 800);   

        }
        else
        {

             
        }
            
        }
        
    
}
