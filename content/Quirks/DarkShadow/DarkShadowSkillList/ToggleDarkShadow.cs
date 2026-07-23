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



public class ToggleDarkShadowSkill : QuirkBaseSkill
{
    public override string Name => "Toggle Dark Shadow";
    public override string Description => "Summon DarkShadow.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.DarkShadow;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {
        var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

        if (player.HasBuff(ModContent.BuffType<DarkShadowBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<DarkShadowBuff>());
            

        }
        else
        {

            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            
            player.AddBuff(ModContent.BuffType<DarkShadowBuff>(), 360000);
            
            
        
            
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(player.position, player.width, player.height, DustID.YellowTorch, speed.X, speed.Y, 0, Color.Black, 2f);
            }
            
             
        }
    }
}
