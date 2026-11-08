using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Buffs.Gearshift;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;
using Microsoft.Xna.Framework;
using Terraria.Audio;



public class TopGearSkill : QuirkBaseSkill
{
    public override string Name => "Top Gear";
    public override string Description => "Speed yourself up to the maximum";

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
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override QuirkStage RequiredOfaStage => QuirkStage.Final;
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {
        var gearshiftPlayer = player.GetModPlayer<GearshiftPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

        int buffTime = transPlayer.CurrentStage switch
        {
            QuirkStage.Intermediate => 300,
            QuirkStage.Advanced => 400,
            QuirkStage.Final => 500,
            _ => 300
        };

        if (player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
        {
            
            player.AddBuff(ModContent.BuffType<TopGearBuff>(), buffTime);
        }
        else
        {

        }

        //if (player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
        //    {
        //        player.ClearBuff(ModContent.BuffType<GearshiftBuff>());

            //        Main.NewText("Gearshift Deactivated!", Color.White);


            //        gearshiftPlayer.GearActivation = false;
            //        gearshiftPlayer.ActivationTimer = 0;
            //        return;
            //    }

            //    else if (gearshiftPlayer.GearActivation)
            //    {
            //        gearshiftPlayer.GearActivation = false;
            //        gearshiftPlayer.ActivationTimer = 0;
            //        Main.NewText("Cancelled.", Color.Gray);
            //    }

            //    else
            //    {
            //        gearshiftPlayer.ActivationTimer = 0;
            //        gearshiftPlayer.GearActivation = true;
            //        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/GearShiftSound") with { Volume = 0.60f }, player.position);
            //    }
    }
}

