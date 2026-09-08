using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Debuffs;
using KhacesCore.Content.System; 

namespace MyHeroMod.content.System
{
    public abstract class QuirkBaseSkill : BaseSkill
    {
        public virtual QuirkType RequiredQuirk => QuirkType.Quirkless;
        public virtual QuirkStage RequiredStage => QuirkStage.Initial;
        public virtual QuirkStage RequiredOfaStage => RequiredStage;
        public virtual bool IsDefaultSkill => false;

        public virtual bool isItemSkill => false;
        
    
        public virtual int RequiredItemId => 0;
        

        public virtual bool isOfaSkill => false;

        public virtual bool CheckErasure(Player player)
        {
            if (IsDefaultSkill) return true;

            if (player.HasBuff(ModContent.BuffType<QuirkErased>()))
            {
                return false; 
            }

            return true;
        }

       public virtual bool CheckItemSkill(Player player)
        {
            if (!isItemSkill) return true; 

            if (RequiredItemId > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!player.armor[i].IsAir && player.armor[i].type == RequiredItemId)
                        return true;
                }

                return false; 
            }
            
            return true; 
        }

      public virtual bool CheckUnlock(TransformationPlayer player)
        {
            if (IsDefaultSkill) return true;

           
            if (isItemSkill)
            {
              
                if (!CheckItemSkill(player.Player))
                    return false; 

              
                if (RequiredQuirk == QuirkType.Quirkless)
                    return true;
            }

            
            if (player.HasActiveQuirk(QuirkType.OneForAll9th))
            {
                var ofaPlayer = player.Player.GetModPlayer<OneForAll9thPlayer>();
                if (ofaPlayer.HasInternalQuirk(RequiredQuirk))
                {
                    return player.CurrentStage >= RequiredOfaStage; 
                }
            }
            
            if (player.HasActiveQuirk(QuirkType.AllForOne))
            {
                var afoPlayer = player.Player.GetModPlayer<AllForOnePlayer>();
                if (afoPlayer.HasInternalQuirk(RequiredQuirk))
                {
                    return player.CurrentStage >= RequiredStage; 
                }
            }

            return player.HasActiveQuirk(RequiredQuirk) && player.CurrentStage >= RequiredStage;
        }
        
        
        public override bool CanUse(Player player) 
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            return CheckUnlock(transPlayer) && CheckErasure(player); 
        }

    }
}