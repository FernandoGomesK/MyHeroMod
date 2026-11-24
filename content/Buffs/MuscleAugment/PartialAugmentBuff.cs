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
    public class PartialAugmentBuff : ModBuff
    {
        

        public override void SetStaticDefaults()
        {
            
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            var muscleAugmentPlayer = player.GetModPlayer<MuscleAugmentPlayer>();
            

            muscleAugmentPlayer.isPartialMuscleAugmentActive = true;

            player.moveSpeed += 0.5f;
            player.jumpSpeedBoost += 1.0f;
            player.statDefense += 7;
            player.GetDamage(DamageClass.Melee) += 0.05f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
            player.noFallDmg = true;


        }

        
    }
}