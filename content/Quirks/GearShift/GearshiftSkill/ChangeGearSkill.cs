using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;
using Microsoft.Xna.Framework;
using Terraria.Audio;



public class ChangeGearSkill : QuirkBaseSkill
{
    public override string Name => "Gearshift";
    public override string Description => "Change the Gear of Whoever you touch";

    public override string IconPath
        {
            get
            {   
                Player player = Main.LocalPlayer;
                var transPlayer = player.GetModPlayer<TransformationPlayer>();

                if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
                {
                    return "MyHeroMod/Assets/SkillIcons/Gearshift/OFAGearshiftIcon"; 
                }
                else
                {
                    return "MyHeroMod/Assets/SkillIcons/Gearshift/GearshiftIcon"; 
                }
            }
        } 
    public override string Category => "Gearshift";
    public override int BaseCooldown => 600;
    public override QuirkType RequiredQuirk => QuirkType.Gearshift;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override QuirkStage RequiredOfaStage => QuirkStage.Final;
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {
        
    }
}

