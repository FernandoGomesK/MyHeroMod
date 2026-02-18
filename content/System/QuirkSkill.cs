using Terraria;
using Terraria.ModLoader;
namespace MyHeroMod.content.System;



    public abstract class QuirkSkill
    {
        public abstract string Name { get; }
        public abstract int BaseCooldown { get; }

        public virtual QuirkType RequiredQuirk => QuirkType.Quirkless;
        public virtual QuirkStage RequiredStage => QuirkStage.Initial;
        public virtual bool IsDefaultSkill => false;

        public virtual bool CheckUnlock (TransformationPlayer player)
    {
        if (IsDefaultSkill) return true;
        return player.SelectedQuirk == RequiredQuirk && player.CurrentStage >= RequiredStage;
    }
        
        
        public virtual bool CanUse(Player player) => true;

        
        public abstract void OnUse(Player player);
    }