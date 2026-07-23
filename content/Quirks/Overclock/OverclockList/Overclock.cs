using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;
using Microsoft.Xna.Framework;
using Terraria.ID;

using Terraria.Audio;
using MyHeroMod.content.Quirks.Overclock;



public class OverclockSkill : QuirkBaseSkill
{
    public override string Name => "Overclock";
    public override string Description => "Speed up your brain for a limited time.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override string Category => "Overclock";
    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.Overclock;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {
        var overclockPlayer = player.GetModPlayer<OverclockPlayer>();

        if (player.HasBuff(ModContent.BuffType<OverclockBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<OverclockBuff>());
            overclockPlayer.isOverclockBuffActive = false;

        }
        else
        {

            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

            
            player.AddBuff(ModContent.BuffType<OverclockBuff>(), 360000);
            Main.NewText("Overclock!", Color.Yellow);
            
        
            
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(player.position, player.width, player.height, DustID.YellowTorch, speed.X, speed.Y, 0, Color.Yellow, 2f);
            }
            
             
        }
    }
}
