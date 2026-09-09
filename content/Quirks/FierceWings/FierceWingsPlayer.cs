using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using KhacesCore.Content.System.Interfaces;

namespace MyHeroMod.content.Quirks.FierceWings
{
    public partial class FierceWingsPlayer : ModPlayer, IQuirkResetter, IFlightModifier
    {
        public int maxfeathers = 100;
        public int currentFeathers = 100;
        public int featherRegen = 2;
        public int featherStage = 1;

        public override void Load()
        {
            if (!Main.dedServ) 
            {
              
                EquipLoader.AddEquipTexture(Mod, "MyHeroMod/content/Quirks/FierceWings/Visuals/FierceWings_1", EquipType.Wings, null, "FierceWings_Stage1");
                EquipLoader.AddEquipTexture(Mod, "MyHeroMod/content/Quirks/FierceWings/Visuals/FierceWings_2", EquipType.Wings, null, "FierceWings_Stage2");
                EquipLoader.AddEquipTexture(Mod, "MyHeroMod/content/Quirks/FierceWings/Visuals/FierceWings_3", EquipType.Wings, null, "FierceWings_Stage3");
                EquipLoader.AddEquipTexture(Mod, "MyHeroMod/content/Quirks/FierceWings/Visuals/FierceWings_4", EquipType.Wings, null, "FierceWings_Stage4");
            }
        }

        public void FullReset()
        {
            maxfeathers = 100; 
            currentFeathers = 100;
            featherRegen = 2;
        }

        public override void PostUpdateMiscEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            if (currentFeathers >= maxfeathers * 0.75f) featherStage = 1;      
            else if (currentFeathers >= maxfeathers * 0.5f) featherStage = 2; 
            else if (currentFeathers >= maxfeathers * 0.25f) featherStage = 3; 
            else featherStage = 4;                                             

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
             
                Player.wingTimeMax = currentFeathers; 
                Player.noFallDmg = true;

                
                if (currentFeathers > 0)
                {
                    string currentWingName = "FierceWings_Stage" + featherStage;
                    int wingID = EquipLoader.GetEquipSlot(Mod, currentWingName, EquipType.Wings);
                    
                    Player.wingsLogic = 29; 
                    Player.wings = wingID;  
                }
                else
                {
                   
                    Player.wings = -1;
                    Player.wingsLogic = 0; 
                }
            }
        }

        public void ModifyFlight(ref float speed)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            if (!transPlayer.HasActiveQuirk(QuirkType.FierceWings)) return; 

           
            
           
            float dashSpeed = transPlayer.CurrentStage switch 
            {
                QuirkStage.Initial => 10f, 
                QuirkStage.Adequation => 15f,
                QuirkStage.Intermediate => 20f, 
                QuirkStage.Advanced => 25f,
                QuirkStage.Final => 30f, 
                _ => 10f
            };

            
            speed = dashSpeed ;
        }

        public bool CanCruiseFlight()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            return transPlayer.HasActiveQuirk(QuirkType.FierceWings);
        }


    }
}