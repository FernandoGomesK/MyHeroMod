using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System.Interfaces;
using KhacesCore.Content.System.Interfaces;
using KhacesCore.Content.System;

namespace MyHeroMod.content.System.Handlers
{
    public class StrainHandler : ModPlayer, IStrainSource
    {
        public int StrainPenaltyPerSecond { get; set; }

        bool IStrainSource.IsLethalStrain => false;
        bool IStrainSource.CausesStrainDamage => false;
        bool IStrainSource.IsStrainActive => true; 

        string IStrainSource.SourceName => "Natural Recovery";

        public void AddStrain(int amount)
        {
            Player.GetModPlayer<CorePlayer>().AddStrain(amount);
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

            if (corePlayer.currentStrain <= 0)
            {
                StrainPenaltyPerSecond = 0;
            }
        }

    }
}