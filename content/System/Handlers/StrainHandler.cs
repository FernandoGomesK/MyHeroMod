using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using KhacesCore.Content.System.Interfaces;
using KhacesCore.Content.System;

namespace MyHeroMod.content.System.Handlers
{
    public class StrainHandler : ModPlayer, IStrainSource
    {
        
        public int StrainPenaltyPerSecond { get; set; }

        public void AddStrain(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var corePlayer = Player.GetModPlayer<CorePlayer>();
            corePlayer.currentStrain += amount;

            if (corePlayer.currentStrain < 0) corePlayer.currentStrain = 0;
            if (corePlayer.currentStrain > corePlayer.maxStrain)corePlayer.currentStrain = corePlayer.maxStrain;
        }

        public override void ResetEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var corePlayer = Player.GetModPlayer<CorePlayer>();
            
           StrainPenaltyPerSecond = -5; 

        if (transPlayer.Nature == NatureType.ResistantBody)
        {
            StrainPenaltyPerSecond = -10;
        }
                  
            if (corePlayer.currentStrain <= 0 && StrainPenaltyPerSecond < 0)
            {
                StrainPenaltyPerSecond = 0;
            }
        }

    
        public static bool HasLethalStrainQuirk(TransformationPlayer transPlayer)
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
            var corePlayer = Player.GetModPlayer<CorePlayer>();

            if (transPlayer.HasActiveQuirk(QuirkType.SuperRegeneration) && !HasLethalStrainQuirk(transPlayer))
                return;

            float strainRatio = corePlayer.maxStrain > 0 ? (float)corePlayer.currentStrain / corePlayer.maxStrain : 0f;
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