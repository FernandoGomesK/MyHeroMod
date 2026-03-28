using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;

public class DashTouchSkill : QuirkSkill
{
    public override string Name => "Decay Dash";
    
        

    
    public override string Description => "Dash Forward reaching for your foes";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 200;
    public override QuirkType RequiredQuirk => QuirkType.Decay;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        
    }
}