using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Handlers
{
    public class QuirkHandler : ModPlayer
    {
        public int GetQuirkCost(QuirkType quirk)
        {
            return quirk switch
            {
                QuirkType.OneForAll9th => 4,
                QuirkType.OneForAll8th => 4,
                QuirkType.AllForOne => 4,

                QuirkType.Overhaul => 3,
                QuirkType.Decay => 3,
                QuirkType.HalfColdHalfHot => 3,
                QuirkType.Explosion => 3,
                QuirkType.Blueflame => 3,
                QuirkType.HellFlames => 3,
                QuirkType.Engine => 3,
                QuirkType.OpticBlast => 3,
                QuirkType.Overclock => 3,
                QuirkType.SuperRegeneration => 3,

                QuirkType.Tape => 1,
                QuirkType.FaJin => 1,
                QuirkType.Float => 1,
                QuirkType.Quirkless => 0,

                _ => 2 
            };
        }

        public override void PostUpdateMiscEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            int totalQuirkWeight = 0;
            foreach (var quirk in transPlayer.ActiveQuirks)
            {
                totalQuirkWeight += GetQuirkCost(quirk);
            }

            
            int currentCapacity = 4;

           
            if (transPlayer.Nature == NatureType.StrongMinded) 
                currentCapacity += 2; 

            if (transPlayer.Nature == NatureType.PerfectVessel) 
                currentCapacity += 4; 

            int overloadAmount = totalQuirkWeight - currentCapacity;
            ApplyOverloadPenalties(overloadAmount);
        }

        private void ApplyOverloadPenalties(int overloadAmount)
        {
            if (overloadAmount <= 0) return;

            if (overloadAmount == 1)
            {
                Player.moveSpeed *= 0.8f;
                Player.GetDamage(DamageClass.Generic) *= 0.9f;
            }
            else if (overloadAmount == 2)
            {
                Player.moveSpeed *= 0.5f;
                Player.statDefense -= 20;
                Player.AddBuff(BuffID.Confused, 2);
                Player.AddBuff(BuffID.Weak, 2);
            }
            else if (overloadAmount >= 3)
            {
                Player.moveSpeed *= 0.5f;
                Player.statDefense -= 20;
                Player.AddBuff(BuffID.Confused, 2);
                Player.AddBuff(BuffID.Weak, 2);
                
               
                Player.statLifeMax2 = (int)(Player.statLifeMax2 * 0.5f);
                Player.AddBuff(BuffID.Blackout, 2);
            }
        }
    }
}