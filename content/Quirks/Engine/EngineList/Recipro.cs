using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;

public class ReciproSkill : QuirkSkill
{
    public override string Name
    {
        get
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.CurrentStage == QuirkStage.Advanced)
            {
                return "Recipro Turbo";
            }
            else if (transPlayer.CurrentStage == QuirkStage.Intermediate)
            {
                return "Recipro Extend";
            }
            else if (transPlayer.CurrentStage == QuirkStage.Adequation)
            {
                return "Recipro Boost";
            }
            else
            {
                return "Engine Boost";
            }
        }
    }

    
    public override string Description => "Supercharge your engines for a short period of time";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Engine;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


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
                    QuirkStage.Advanced => 300,      
                    QuirkStage.Final => 500, 
                    _ => 120
                };

    
            player.AddBuff(ModContent.BuffType<ReciproBuff>(), buffTime);
             
        }
    }
}