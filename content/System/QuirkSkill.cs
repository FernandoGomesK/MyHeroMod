using Terraria;



    public abstract class QuirkSkill
    {
        public abstract string Name { get; }
        public abstract int BaseCooldown { get; }
        
        
        public virtual bool CanUse(Player player) => true;

        
        public abstract void OnUse(Player player);
    }