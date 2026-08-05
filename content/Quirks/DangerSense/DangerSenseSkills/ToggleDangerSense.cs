using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th;

public class ToggleDangerSenseSkill : QuirkBaseSkill
{
    public override string Name => "Toggle DangerSense";
    public override string Description => "Activates DangerSense";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";
    public override string Category => "DangerSense";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.DangerSense;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override QuirkStage RequiredOfaStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {
        var dsPlayer = player.GetModPlayer<DangerSensePlayer>();   

        
        if (dsPlayer.HasDangerSenseAccess())
        {
            dsPlayer.isDangerSenseActive = !dsPlayer.isDangerSenseActive;

            if (dsPlayer.isDangerSenseActive)
            {
                CombatText.NewText(player.getRect(), Color.Orange, "Danger Sense: ON");
                SoundEngine.PlaySound(SoundID.Item4, player.position);
            }
            else
            {
                CombatText.NewText(player.getRect(), Color.Gray, "Danger Sense: OFF");
                SoundEngine.PlaySound(SoundID.Item4, player.position);
                player.ClearBuff(ModContent.BuffType<DangerSenseBuff>());
            }
        }
    }
}   
            
            

    
    
    