using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Handlers
{
    public class QuirkHandler : ModPlayer
    {
        // Define the complexity of each quirk
        public int GetQuirkCost(QuirkType quirk)
        {
            return quirk switch
            {
                QuirkType.OneForAll9th => 4,
                QuirkType.AllForOne => 4,
                QuirkType.Overhaul => 3,
                QuirkType.Decay => 3,
                QuirkType.Explosion => 2,
                QuirkType.Tape => 1,
                QuirkType.Quirkless => 0,
                _ => 2 
            };
        }

        public override void PostUpdateMiscEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            int totalQuirkWeight = 0;

            // 1. Calculate total weight
            foreach (var quirk in transPlayer.ActiveQuirks)
            {
                totalQuirkWeight += GetQuirkCost(quirk);
            }

            // 2. Apply Nature discounts to weight (e.g., Higher Brain Power reduces complex quirk load)
            if (transPlayer.Nature == NatureType.HigherBrainPower && transPlayer.HasActiveQuirk(QuirkType.Overhaul))    
            {
                totalQuirkWeight -= 1; 
            }

            // 3. Calculate Capacity
            int currentCapacity = transPlayer.naturalQuirkLimit;
            if (transPlayer.Nature == NatureType.StrongMinded) currentCapacity += 2;
            if (transPlayer.Nature == NatureType.PerfectVessel) currentCapacity += 4;

            // 4. Apply Overload Debuffs if they exceed capacity
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
            else if (overloadAmount >= 2)
            {
                Player.moveSpeed *= 0.5f;
                Player.statDefense -= 20;
                Player.AddBuff(BuffID.Confused, 2);
                Player.AddBuff(BuffID.Weak, 2);
                
                // Cellular decay simulation
                if (overloadAmount >= 3)
                {
                    Player.statLifeMax2 = (int)(Player.statLifeMax2 * 0.5f);
                    Player.AddBuff(BuffID.Blackout, 2);
                }
            }
        }
    }
}