using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA8th;

public abstract class ToggleStockPile : QuirkSkill
{
    public override string Name => "StockPile";
    public override string Description => "Activate One for All throught your entire body";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense"; 
    public override int BaseCooldown => 60; 
    public override QuirkType RequiredQuirk => QuirkType.OneForAll8th;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;



    protected abstract int stockform { get; }

    
    protected abstract int BuffType { get; }

    public override void OnUse(Player player)
    {

        var OfaPlayer = player.GetModPlayer<OneForAll8thPlayer>();
        
        if (player.HasBuff(BuffType))
    {
        
        player.ClearBuff(BuffType); 
        OfaPlayer.form = 0;
        CombatText.NewText(player.getRect(), Color.Red, "Deactivated");
    }
        else
    {
        OfaPlayer.form = stockform;
        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/watashigakita"), player.position);
        CombatText.NewText(player.getRect(), Color.LightGreen, Name);
        player.AddBuff(ModContent.BuffType<StockPileBuff>(), 3600000);
    }
    
    }
}

public class StockPile : ToggleStockPile
{
    public override string Name => "Stockpile";
    public override string IconPath => "MyHeroMod/Assets/Skills/FullCowling5";
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    protected override int BuffType => ModContent.BuffType<StockPileBuff>(); 

    protected override int stockform => 1;



     

    
}

public class StockPileMaximum : ToggleStockPile
{
    public override string Name => "Stockpile Maximum";
    public override string IconPath => "MyHeroMod/Assets/Skills/FullCowling10";
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    protected override int BuffType => ModContent.BuffType<StockPileBuff>();

    protected override int stockform => 2;


}
