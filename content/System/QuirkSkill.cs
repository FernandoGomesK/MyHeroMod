using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;


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

        public virtual bool CheckUnlock(TransformationPlayer player)
{
    
    if (IsDefaultSkill) return true;

    if (player.SelectedQuirk == QuirkType.OneForAll9th)
            {
                bool isOfaSubQuirk = RequiredQuirk == QuirkType.BlackWhip || 
                RequiredQuirk == QuirkType.FaJin ||
                RequiredQuirk == QuirkType.Gearshift ||
                RequiredQuirk == QuirkType.DangerSense ||
                RequiredQuirk == QuirkType.Float ||
                RequiredQuirk == QuirkType.SmokeScreen;
            if (isOfaSubQuirk) 
                return player.CurrentStage >= RequiredStage;

            }


    
    if (IsBaseQuirk && player.SelectedQuirk == RequiredQuirk) return true;
    
    bool hasRightQuirk = player.SelectedQuirk == RequiredQuirk;
                                
    return hasRightQuirk && player.CurrentStage >= RequiredStage;
}
        
        
        public virtual bool CanUse(Player player) 
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            return CheckUnlock(transPlayer); 
        }

        
        public abstract void OnUse(Player player);
    }
}