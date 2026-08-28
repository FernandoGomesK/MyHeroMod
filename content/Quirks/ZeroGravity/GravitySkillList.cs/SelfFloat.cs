using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.ZeroGravity;

public class SelfFloatSkill : QuirkBaseSkill
{
    public override string Name => "Self Float";
    public override string Description => "Float around";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/ZeroGravity/SelfFloatIcon";
    public override string Category => "ZeroGravity";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.ZeroGravity;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;



    public override void OnUse(Player player)
    {
        var zPlayer = player.GetModPlayer<ZeroGravityPlayer>();


        
        if (player.HasBuff(BuffID.Confused) || zPlayer.Nausea >= zPlayer.NauseaMax)
        {
            Main.NewText("You feel too sick to use your Quirk...", Color.GreenYellow);
            return;
        }

        
        if (player.HasBuff(ModContent.BuffType<ZeroGravityBuff>()))
        {
            
            player.ClearBuff(ModContent.BuffType<ZeroGravityBuff>());
            zPlayer.isZeroGravityActive = false;
        }
        else
        {
            
            player.AddBuff(ModContent.BuffType<ZeroGravityBuff>(), 216000); 
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item29, player.position);
        }
    }
}
