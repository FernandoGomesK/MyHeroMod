using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;
using Terraria.Audio;



public class Clusterkill : BaseToggleSkill
{
    public override string Name => "Toggle Cluster";
    public override string Description => "Begin To float to the skies";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Explosion";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    
    public override int BuffType => ModContent.BuffType<ClusterBuff>();
    public override SoundStyle? ToggleSound => new SoundStyle("MyHeroMod/Assets/Sounds/Crackle1");
        public override float ToggleSoundVolume => 0.4f; 



    
}