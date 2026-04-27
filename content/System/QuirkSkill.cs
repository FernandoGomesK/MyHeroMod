using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Debuffs;


namespace MyHeroMod.content.System
{
    

    public abstract class QuirkSkill
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string IconPath { get; }
    
        public abstract int BaseCooldown { get; }

        public virtual QuirkType RequiredQuirk => QuirkType.Quirkless;
        public virtual QuirkStage RequiredStage => QuirkStage.Initial;
        public virtual bool IsDefaultSkill => false;
        public virtual bool IsBaseQuirk => false;

        public virtual bool CheckErasure(Player player)
        {
            
            if (IsDefaultSkill) return true;

            
            if (player.HasBuff(ModContent.BuffType<QuirkErased>()))
            {
                return false; 
            }

            return true;
        }

        public virtual bool CheckUnlock(TransformationPlayer player)
{
    
    if (IsDefaultSkill) return true;

    if (player.HasActiveQuirk(QuirkType.OneForAll9th))
    {
        var ofaPlayer = player.Player.GetModPlayer<OneForAll9thPlayer>();

        if (ofaPlayer.HasInternalQuirk(RequiredQuirk))
        {
            return player.CurrentStage >= RequiredStage; 
        }
    }

    if (player.HasActiveQuirk(QuirkType.AllForOne))
    {
        var ofaPlayer = player.Player.GetModPlayer<AllForOnePlayer>();

        if (ofaPlayer.HasInternalQuirk(RequiredQuirk))
        {
            return player.CurrentStage >= RequiredStage; 
        }
    }

    
    if (IsBaseQuirk && player.HasActiveQuirk(RequiredQuirk)) return true;
    
    bool hasRightQuirk = player.HasActiveQuirk(RequiredQuirk);
                                
    return hasRightQuirk && player.CurrentStage >= RequiredStage;
}
        
        
        public virtual bool CanUse(Player player) 
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            return CheckUnlock(transPlayer) && CheckErasure(player); 

        }

        
        public abstract void OnUse(Player player);
    }
}