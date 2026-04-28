using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;
using Microsoft.Xna.Framework;
using Terraria.ID;

using Terraria.Audio;
using MyHeroMod.content.Quirks.Overhaul;



public class ChimeraSkill : QuirkSkill
{
    public override string Name => "Chimera";
    public override string Description => "Combine the powers of different quirks.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.Overhaul;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {
        var overhaulPlayer = player.GetModPlayer<OverhaulPlayer>();

        if (player.HasBuff(ModContent.BuffType<ChimeraBuff>()))
            {
                player.ClearBuff(ModContent.BuffType<ChimeraBuff>());
                
                Main.NewText("Chimera Deactivated!", Color.White);
                
                
                overhaulPlayer.isChimeraActive = false;
                overhaulPlayer.ActivationTimer = 0;
                return;
            }
            
            else if (overhaulPlayer.ChimeraActivation)
            {
                overhaulPlayer.ChimeraActivation = false;
                overhaulPlayer.ActivationTimer = 0;
                Main.NewText("Cancelled.", Color.Gray);
            }
            
            else
            {
                overhaulPlayer.ActivationTimer = 0;
                overhaulPlayer.ChimeraActivation = true;
                
            }
    }
}
