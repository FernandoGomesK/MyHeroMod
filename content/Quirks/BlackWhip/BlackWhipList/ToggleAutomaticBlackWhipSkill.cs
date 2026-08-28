using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.SpeedForce;
using Microsoft.Xna.Framework;
using Terraria.ID;

using Terraria.Audio;
using MyHeroMod.content.Quirks.Overclock;
using MyHeroMod.content.Quirks.DarkShadow;
using MyHeroMod.content.Quirks.BlackWhip;



public class ToggleAutomaticWhipSkill : QuirkBaseSkill
{
    public override string Name => "Toggle Automatic Whip";
    public override string Description => "Activate Overlay transformation.";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Blackwhip/AutomaticWhipIcon";
    public override string Category => "BlackWhip";
    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.BlackWhip;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override QuirkStage RequiredOfaStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
   

    public override void OnUse(Player player)
    {
        var darkPlayer = player.GetModPlayer<BlackWhipPlayer>();

        if (player.HasBuff(ModContent.BuffType<AutomaticWhipBuff>()))
        {
            if (player.HasBuff(ModContent.BuffType<AutomaticWhipBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<AutomaticWhipBuff>());
        }
        }
        else
        {

            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            
            player.AddBuff(ModContent.BuffType<AutomaticWhipBuff>(), 360000);

            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(player.position, player.width, player.height, DustID.YellowTorch, speed.X, speed.Y, 0, Color.Black, 2f);
            }
            
             
        }
            
        
        
    }
}
