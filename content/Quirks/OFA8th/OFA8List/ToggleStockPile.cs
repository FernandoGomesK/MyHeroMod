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
using System;
using MyHeroMod.content.Quirks.AllForOne;

public abstract class ToggleStockPile : QuirkBaseSkill
{
    public override string Name => "StockPile";
    public override string Description => "Activate One for All throught your entire body";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA8th/StockPileIcon"; 
    public override string Category => "OneForAll8th";
    public override int BaseCooldown => 60; 
    public override QuirkType RequiredQuirk => QuirkType.OneForAll8th;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    



    protected abstract int stockform { get; }

    
    protected abstract int BuffType { get; }

   public override void OnUse(Player player)
    {
        var ofaPlayer = player.GetModPlayer<OneForAll8thPlayer>();

        if (player.HasBuff(BuffType))
        {
            player.ClearBuff(BuffType); 
            ofaPlayer.form = 0;
            CombatText.NewText(player.getRect(), Color.Red, "Deactivated");
        }
        else
        {
            
            int upfrontCost = 100;
            int minDurationTicks = 1200; 

            if (ofaPlayer.isQuirkless)
            {
    
                if (ofaPlayer.embersAmount < upfrontCost)
                {
                    CombatText.NewText(player.getRect(), Color.Orange, "Embers too low!");
                    return;
                }

                ofaPlayer.embersAmount -= upfrontCost;
                
                int calculatedDuration = Math.Max(ofaPlayer.embersAmount, minDurationTicks);
                
                ofaPlayer.form = stockform;
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/watashigakita"), player.position);
                Main.NewText(Name, Color.LightGreen);
                CombatText.NewText(player.getRect(), Color.Yellow, "WATASHI GA KITA!");
                
               
                player.AddBuff(BuffType, calculatedDuration);

                if (ofaPlayer.embersAmount <= 0)
                {
                    var transPlayer = player.GetModPlayer<TransformationPlayer>();
                    var afoPlayer = player.GetModPlayer<AllForOnePlayer>();
                    ofaPlayer.RemoveOneForAll8th(transPlayer, afoPlayer);
                }
            }
            else
            {
              
                ofaPlayer.form = stockform;
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/watashigakita"), player.position);
                Main.NewText(Name, Color.LightGreen);
                CombatText.NewText(player.getRect(), Color.Yellow, "WATASHI GA KITA!");
                player.AddBuff(BuffType, 3600000);
            }
        }
    }
}

public class StockPile : ToggleStockPile
{
    public override string Name => "Stockpile";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA8th/StockPileIcon"; 
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    protected override int BuffType => ModContent.BuffType<StockPileBuff>(); 

    protected override int stockform => 1;



     

    
}

public class StockPileMaximum : ToggleStockPile
{
    public override string Name => "Stockpile Maximum";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA8th/StockPileMaxIcon"; 
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    protected override int BuffType => ModContent.BuffType<StockPileBuff>();

    protected override int stockform => 2;


}
