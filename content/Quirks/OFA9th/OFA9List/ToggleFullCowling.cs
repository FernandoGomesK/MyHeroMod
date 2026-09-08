using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Graphics.Renderers;
using Steamworks;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;

public abstract class FullCowlingBase : QuirkBaseSkill
{
    public override string Name => "One For All: Full Cowling";
    public override string Description => "Activate One for All throught your entire body";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense"; 
    public override int BaseCooldown => 60; 
    public override string Category => "OneForAll9th";
    public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    



    protected abstract int CowlingPercentage { get; }

    
    protected abstract int BuffType { get; }

    public override void OnUse(Player player)
    {
        var OfaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
        
        
        if (player.HasBuff(BuffType))
        {
            player.ClearBuff(BuffType); 
            OfaPlayer.percentage = 0;
            CombatText.NewText(player.getRect(), Color.Red, "Deactivated");
        }
        
        else
        {
            CombatText.NewText(player.getRect(), Color.LightGreen, Name + " Charging!");
            
            
            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                Vector2.Zero, 
                ModContent.ProjectileType<FullCowlingChargeProj>(), 
                0, 
                0f, 
                player.whoAmI, 
                ai0: 0f, 
                ai1: 0f, 
                ai2: CowlingPercentage 
            );
        }
        
    }
}

public class FullCowling5 : FullCowlingBase
{
    public override string Name => "OFA Full Cowling 5%";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA9th/FullCowling5Icon";
    
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    protected override int BuffType => ModContent.BuffType<FullCowlingBuff>(); 
    public override string Category => "OneForAll9th";

    protected override int CowlingPercentage => 5;

}

public class FullCowling10 : FullCowlingBase
{
    public override string Name => "OFA Full Cowling 10%";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA9th/FullCowling10Icon";
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    protected override int BuffType => ModContent.BuffType<FullCowlingBuff>(); 
    public override string Category => "OneForAll9th";
    protected override int CowlingPercentage => 10;
}

public class FullCowling20: FullCowlingBase
{
    public override string Name => "OFA Full Cowling 20%";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA9th/FullCowling20Icon";
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    protected override int BuffType => ModContent.BuffType<FullCowlingBuff>(); 
    public override string Category => "OneForAll9th";
    protected override int CowlingPercentage => 20;
}
public class FullCowling45 : FullCowlingBase
{
    public override string Name => "OFA Full Cowling 45%";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA9th/FullCowling45Icon";
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    protected override int BuffType => ModContent.BuffType<FullCowlingBuff>(); 
    public override string Category => "OneForAll9th";
    protected override int CowlingPercentage => 45;


}