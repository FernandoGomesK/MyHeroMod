using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod;

namespace MyHeroMod.content.System
{
    public class RandomQuirkSelection
    {
        public static void SelectRandomQuirk()
        {

            Array quirksArray = Enum.GetValues(typeof(QuirkType));

            

            QuirkType quirkType;
            do
            {
                int randomIndex = Main.rand.Next(0, quirksArray.Length);
                quirkType = (QuirkType)quirksArray.GetValue(randomIndex);
            }
            while ( quirkType == QuirkType.Quirkless || quirkType == QuirkType.HellFlames ||
                    quirkType == QuirkType.BlueFlames || quirkType == QuirkType.AllForOne || quirkType == QuirkType.Erasure) ;

            var transPlayer = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();


            transPlayer.ResetSlot();
            transPlayer.CompleteReset();
            transPlayer.SelectedQuirk = quirkType;

            
            
            
        
            Main.NewText($"You awakened: {quirkType}!", Color.Yellow);
        }
    }
}