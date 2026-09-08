using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod;
using Terraria.ID;

namespace MyHeroMod.content.System
{
    public class RandomNatureSelection
    {
        public static void SelectRandomNature()
        {
            var transPlayer = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
            
            Array naturesArray = Enum.GetValues(typeof(NatureType));

            
            int randomIndex = Main.rand.Next(0, naturesArray.Length);
            NatureType natureType = (NatureType)naturesArray.GetValue(randomIndex);

            transPlayer.Nature = natureType;

            
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                transPlayer.SendClientChanges(transPlayer);
            }

           
            if (natureType == NatureType.None)
            {
                Main.NewText("Your body doesn't seem to have any special traits.", Color.Gray);
            }
            else if (natureType == NatureType.PerfectVessel)
            {
                SelectRandomNature();
            }
            else
            {
                Main.NewText($"Your body developed a special trait: {natureType}!", Color.LightGreen);
            }
        }
    }
}