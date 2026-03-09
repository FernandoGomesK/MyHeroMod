using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;
using Microsoft.Xna.Framework;
using Terraria.ID;

using Terraria.Audio;



public class OverclockSkill : QuirkSkill
{
    public override string Name => "Overclock";
    public override string Description => "Speed yourself up for a limited time.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.Overclock;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {
        if (player.HasBuff(ModContent.BuffType<OverclockBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<OverclockBuff>());
        }
        else
        {

            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            int buffDuration = 180;

            switch(transformPlayer.CurrentStage)
            {
                case QuirkStage.Initial: buffDuration = 187; break;
                case QuirkStage.Adequation: buffDuration = 250; break;
                case QuirkStage.Intermediate: buffDuration = 300; break;
                case QuirkStage.Advanced: buffDuration = 450; break;
                case QuirkStage.Final: buffDuration = 600; break;
                default: buffDuration = 650; break;
            }

            
            player.AddBuff(ModContent.BuffType<OverclockBuff>(), buffDuration);
            Main.NewText("Overclock!", Color.Yellow);
            
        
            
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(player.position, player.width, player.height, DustID.YellowTorch, speed.X, speed.Y, 0, Color.Yellow, 2f);
            }
            
             
        }
    }
}
