using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.FierceWings
{
    public partial class FierceWingsPlayer : ModPlayer, IQuirkResetter
    {
        public int maxfeathers = 100;
        public int currentFeathers = 100;
        public int featherRegen = 2;
        public int featherStage = 1;

        public void FullReset()
        {
            maxfeathers = 100; 
            currentFeathers = 100;
            featherRegen = 2;
        }

        public override void PostUpdateMiscEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            // 1. Calcula o estágio visual das asas
            if (currentFeathers >= maxfeathers * 0.75f) featherStage = 1;      
            else if (currentFeathers >= maxfeathers * 0.5f) featherStage = 2; 
            else if (currentFeathers >= maxfeathers * 0.25f) featherStage = 3; 
            else featherStage = 4;                                             

            // 2. Natureza Resourceful aumenta a regeneração
            int actualRegen = featherRegen;
            if (transPlayer.Nature == NatureType.Resourceful)
            {
                actualRegen += 1; 
            }

            
            if (currentFeathers < maxfeathers)
            {
                currentFeathers += actualRegen;
                if (currentFeathers > maxfeathers) 
                {
                    currentFeathers = maxfeathers;
                }
            }
        }

        public override void PostUpdateEquips()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();


            if (transPlayer.HasActiveQuirk(QuirkType.FierceWings))
            {
                
                Player.wingTimeMax = 50;

                
                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29; 
                    Player.wings = -1;
                }

                
                Player.noFallDmg = true;
            }
        }
    }
}