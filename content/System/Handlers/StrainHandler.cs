using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;

namespace MyHeroMod.content.Handlers
{
    public class StrainHandler : ModPlayer, IStrainSource
    {
        
        public int StrainPenaltyPerSecond { get; set; }

        public void AddStrain(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            transPlayer.currentStrain += amount;

            if (transPlayer.currentStrain < 0) transPlayer.currentStrain = 0;
            if (transPlayer.currentStrain > transPlayer.maxStrain) transPlayer.currentStrain = transPlayer.maxStrain;
        }

        public override void ResetEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
           StrainPenaltyPerSecond = -5; 

        if (transPlayer.Nature == NatureType.ResistantBody)
        {
            StrainPenaltyPerSecond = -10;
        }
                  
            if (transPlayer.currentStrain <= 0 && StrainPenaltyPerSecond < 0)
            {
                StrainPenaltyPerSecond = 0;
            }
        }

    
        public bool HasLethalStrainQuirk(TransformationPlayer transPlayer)
        {
            foreach (var quirk in transPlayer.ActiveQuirks)
            {
                if (quirk == QuirkType.OneForAll9th || quirk == QuirkType.Blueflame) return true;
            }
            return false;
        }

        public override void UpdateBadLifeRegen()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.HasActiveQuirk(QuirkType.SuperRegeneration) && !HasLethalStrainQuirk(transPlayer))
                return;

            float strainRatio = transPlayer.maxStrain > 0 ? (float)transPlayer.currentStrain / transPlayer.maxStrain : 0f;
            if (strainRatio < 0.25f) return;

            bool lethal = HasLethalStrainQuirk(transPlayer);

            float damagePercent = strainRatio switch
            {
                >= 0.75f => lethal ? 0.06f : 0.10f,
                >= 0.50f => lethal ? 0.03f : 0.05f,
                _        => lethal ? 0.01f : 0.02f,
            };

            int floorPercent = lethal ? 5 : strainRatio switch
            {
                >= 0.75f => 25,
                >= 0.50f => 50,
                _        => 75,
            };
            
            int floorHealth = (int)(Player.statLifeMax2 * (floorPercent / 100f));

            if (Player.statLife > floorHealth)
            {
                int damagePerSecond = (int)(Player.statLifeMax2 * damagePercent);
                if (Player.lifeRegen > 0) Player.lifeRegen = 0;
                Player.lifeRegen -= damagePerSecond * 2;
            }
           
            
        }
    }
}