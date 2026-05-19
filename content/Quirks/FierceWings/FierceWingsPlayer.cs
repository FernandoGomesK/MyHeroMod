
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.FierceWings
{

    public partial class FierceWingsPlayer: ModPlayer, IQuirkResetter{

        public int maxfeathers = 100;
        public int currentFeathers = 100;

        public int featherRegen = 2;

        public int featherStage = 1;

        public void FullReset()
        {
            maxfeathers = 0;
            currentFeathers = 0;
            featherRegen = 0;
        }

        public override void PostUpdateMiscEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (currentFeathers >= maxfeathers * 0.75) featherStage = 1;
            else if (currentFeathers >= maxfeathers * 0.5) featherStage = 2;
            else if (currentFeathers >= maxfeathers * 0.25) featherStage = 3;
            else if (currentFeathers <= maxfeathers * 0.24) featherStage = 4;

            if (transPlayer.Nature == NatureType.Resourceful)

            if (currentFeathers < maxfeathers)
            {
                currentFeathers += featherRegen;
                if (currentFeathers > featherRegen){
                    currentFeathers = maxfeathers;
                    
                }
            }
        }

    }


    
    

}

