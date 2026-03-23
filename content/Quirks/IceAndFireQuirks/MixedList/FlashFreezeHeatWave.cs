using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;

using MyHeroMod.content.Quirks.HalfColdHalfHot;




public class FlashFreezeSkill : QuirkSkill
{
    
    public override string Name => "Flash Freeze Heatwave";

   
    public override string Description => "Cool the air around yourself and quickly heat it up releasing a powerfull Heatwave";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

        

        if (!hchhPlayer.IsFlashFreezeActive)
                    {
                        hchhPlayer.IsFlashFreezeActive = true;
                        hchhPlayer.FlashFreezeTimer = 0;
                        
                    }
    }}
      