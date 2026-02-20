using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;

public abstract class FullCowlingBase : QuirkSkill
{
    public override string Name => "One For All: Full Cowling";
    public override string Description => "Activate One for All throught your entire body";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense"; 
    public override int BaseCooldown => 60; 
    public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public int ActivationTimer = 0;
    public int ActivationMaxTime = 40;

    
    protected abstract int BuffType { get; }

    public override void OnUse(Player player)
    {
        
        if (player.HasBuff(BuffType))
        {
            player.ClearBuff(BuffType);
            CombatText.NewText(player.getRect(), Color.Red, "Deactivated");
        }
        else
        {
            if (ActivationTimer > 0){
                ActivationTimer++;
                player.moveSpeed -= 2f;

                if (ActivationTimer >= ActivationMaxTime)
                {
                    player.AddBuff(BuffType, 360000); 
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
            CombatText.NewText(player.getRect(), Color.LightGreen, Name + " Ativado!");
                }

            }
            
            
        }
    }
}

public class FullCowling5 : FullCowlingBase
{
    public override string Name => "OFA Full Cowling 5%";
    public override string IconPath => "MyHeroMod/Assets/Skills/FullCowling5";
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    protected override int BuffType => ModContent.BuffType<FullCowlingBuff5>(); 
}

public class FullCowling10 : FullCowlingBase
{
    public override string Name => "OFA Full Cowling 10%";
    public override string IconPath => "MyHeroMod/Assets/Skills/FullCowling10";
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    protected override int BuffType => ModContent.BuffType<FullCowlingBuff10>(); 
}