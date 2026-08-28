using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;

public class ReciproSkill : QuirkBaseSkill
{
    public override string Name => "Recipro";
    

    public override string GetDisplayName(Player player)
{
    var transPlayer = player.GetModPlayer<TransformationPlayer>();
    return transPlayer.CurrentStage switch
    {
        QuirkStage.Advanced => "Recipro Turbo",
        QuirkStage.Intermediate => "Recipro Extend",
        QuirkStage.Adequation => "Recipro Boost",
        _ => "Engine Boost"
    };
}

    
    public override string Description => "Supercharge your engines for a short period of time";
     public override string IconPath
        {
            get
            {   
                Player player = Main.LocalPlayer;
                var transPlayer = player.GetModPlayer<TransformationPlayer>();

                if (transPlayer.CurrentVariant == QuirkVariant.Variant1)
                {
                    return "MyHeroMod/Assets/SkillIcons/Engine/TenseiBoostIcon"; 
                    
                    
                }
                else
                {
                    return "MyHeroMod/Assets/SkillIcons/Engine/TenyaBoostIcon"; 
                }
            }
        } 
    public override string Category => "Engine";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Engine;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;



    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<ReciproBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<ReciproBuff>());
        }
        else
        {
            
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var buffTime = transPlayer.CurrentStage switch
            {
                    QuirkStage.Initial => 120,       
                    QuirkStage.Adequation => 160,     
                    QuirkStage.Intermediate => 250,   
                    QuirkStage.Advanced => 1200,      
                    QuirkStage.Final => 1800, 
                    _ => 120
                };

    
            player.AddBuff(ModContent.BuffType<ReciproBuff>(), buffTime);
             
        }
    }
}