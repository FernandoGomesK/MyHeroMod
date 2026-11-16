using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.FaJin;
using Terraria.ID;
using MyHeroMod.content.Quirks.Erasure;
using MyHeroMod.content.Quirks.DarkShadow;
using MyHeroMod.content.Quirks.MuscleAugment;

namespace MyHeroMod.content.Buffs.MuscleAugment
{
    public class MuscleAugmentBuff : ModBuff
    {
        

        public override void SetStaticDefaults()
        {
            
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var muscleAugmentPlayer = player.GetModPlayer<MuscleAugmentPlayer>();
            

            muscleAugmentPlayer.isMuscleAugmentActive = true;

            
            

            
            
        }

        
    }
}