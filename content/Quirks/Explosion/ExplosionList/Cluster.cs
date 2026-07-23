using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;


public class Clusterkill : QuirkBaseSkill
{
    public override string Name => "Toggle Cluster";
    public override string Description => "Begin To float to the skies";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Explosion";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<ClusterBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<ClusterBuff>());
        }
        else
        {
            player.AddBuff(ModContent.BuffType<ClusterBuff>(), 3600);
             
        }
    }
}