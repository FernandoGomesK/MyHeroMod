using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using Microsoft.Xna.Framework;
using Terraria.Audio;

public class TogglePhosphorSkill : QuirkBaseSkill
{
public override string Name => "Phosphor";
    public override string Description => "Toggle Phosphor";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Fire";

    public override int BaseCooldown => 30;
     public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    

    public override bool CheckUnlock(TransformationPlayer player)
    {
        
        if(player.HasActiveQuirk(QuirkType.HalfColdHalfHot))
        {
            return player.CurrentStage >= QuirkStage.Advanced; 
        } 
            
        else if(player.HasActiveQuirk(QuirkType.Blueflame))
        {
            return player.CurrentStage >= QuirkStage.Final; 
        }
        return false;
    }


    public override void OnUse(Player player)
    {

    var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

    if (player.HasBuff(ModContent.BuffType<PhosphorBuff>()))
    {
        
        player.ClearBuff(ModContent.BuffType<PhosphorBuff>());
        
        CombatText.NewText(player.getRect(), Color.Red, "Deactivated");
    }
        else if (hchhPlayer.Activating)
    {
        hchhPlayer.Activating = false;
        hchhPlayer.ActivationTimer = 0;
        hchhPlayer.pendingPercentage = 0;
        CombatText.NewText(player.getRect(), Color.Red, "Canceled");
    }
        else
    {
        hchhPlayer.ActivationTimer = 0;
        hchhPlayer.Activating = true;
        player.AddBuff(ModContent.BuffType<PhosphorBuff>(), 3600);
        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
        CombatText.NewText(player.getRect(), Color.LightGreen, Name + " Charging!");
    }
    }}