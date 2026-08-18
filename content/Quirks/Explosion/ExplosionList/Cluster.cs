using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.GeneralSkills;
using Terraria.Audio;
using Microsoft.Xna.Framework;



public class Clusterkill : BaseToggleSkill
{
    public override string Name => "Toggle Cluster";
    public override string Description => "Begin To float to the skies";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Explosion/ClusterIcon";

    public override string Category => "Explosion";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    
    public override int BuffType => ModContent.BuffType<ClusterBuff>();
    public override SoundStyle? ToggleSound => new SoundStyle("MyHeroMod/Assets/Sounds/Crackle1");
        public override float ToggleSoundVolume => 0.4f;

    public override void OnUse(Player player)
    {
        base.OnUse(player);
        
    }

    public override void OnToggleOn(Player player)
    {
        base.OnToggleOn(player);
        ImpactFrameSystem.Trigger(Color.White, false, "MyHeroMod/Assets/Effects/Cluster/ClusterImpact1", "MyHeroMod/Assets/Effects/Cluster/ClusterImpact2",
         "MyHeroMod/Assets/Effects/Cluster/ClusterImpact3",
          "MyHeroMod/Assets/Effects/Cluster/ClusterImpact4");
    }



    
}