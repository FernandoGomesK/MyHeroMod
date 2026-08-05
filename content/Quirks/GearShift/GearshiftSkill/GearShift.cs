using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;
using Microsoft.Xna.Framework;
using Terraria.Audio;



public class GearShiftSkill : QuirkBaseSkill
{
    public override string Name => "Gearshift";
    public override string Description => "Changes the user's gear to fit the situation.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override string Category => "Gearshift";
    public override int BaseCooldown => 600;
    public override QuirkType RequiredQuirk => QuirkType.Gearshift;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override QuirkStage RequiredOfaStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {
        var gearshiftPlayer = player.GetModPlayer<GearshiftPlayer>();

        if (player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
            {
                player.ClearBuff(ModContent.BuffType<GearshiftBuff>());
                
                Main.NewText("Gearshift Deactivated!", Color.White);
                
                
                gearshiftPlayer.GearActivation = false;
                gearshiftPlayer.ActivationTimer = 0;
                return;
            }
            
            else if (gearshiftPlayer.GearActivation)
            {
                gearshiftPlayer.GearActivation = false;
                gearshiftPlayer.ActivationTimer = 0;
                Main.NewText("Cancelled.", Color.Gray);
            }
            
            else
            {
                gearshiftPlayer.ActivationTimer = 0;
                gearshiftPlayer.GearActivation = true;
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/GearShiftSound") with { Volume = 0.60f }, player.position);
            }
    }
}

