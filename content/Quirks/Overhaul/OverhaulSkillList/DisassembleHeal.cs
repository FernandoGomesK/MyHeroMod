using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Overhaul.Projectiles.DisassembleRange;

public class DisassembleHealSkill : QuirkBaseSkill
{
    public override string Name => "Disassemble Heal";
    
        

    
    public override string Description => "Dissassemble Yourself to heal";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Overhaul";

    public override int BaseCooldown => 200;
    public override QuirkType RequiredQuirk => QuirkType.Overhaul;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        if (player.statLife < player.statLifeMax2)
        {
            player.statLife += (int)(player.statLifeMax2 * 0.15f);
        }
        
        
    }
}