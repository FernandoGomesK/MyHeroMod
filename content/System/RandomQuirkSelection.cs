using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod;
using Terraria.ID;

namespace MyHeroMod.content.System
{
    public class RandomQuirkSelection
    {
        public static void SelectRandomQuirk()
        {
            var transPlayer = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
            Array quirksArray = Enum.GetValues(typeof(QuirkType));

        
            if (transPlayer.ActiveQuirks.Count >= quirksArray.Length - 1)
            {
                Main.NewText("Your body cannot physically hold any more Quirks!", Color.Red);
                return;
            }

            QuirkType quirkType;
            do
            {
                int randomIndex = Main.rand.Next(0, quirksArray.Length);
                quirkType = (QuirkType)quirksArray.GetValue(randomIndex);
            }
            
            while (quirkType == QuirkType.Quirkless || transPlayer.HasActiveQuirk(quirkType));

            
            if (transPlayer.ActiveQuirks.Count >= transPlayer.naturalQuirkLimit)
            {
                
                Main.NewText("Your body feels heavy... taking another Quirk is mutating your cells!", Color.DarkRed);
            }

            
            transPlayer.ActiveQuirks.Add(quirkType);
            transPlayer.UpdateUnlockedSkills();

            
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                transPlayer.SendClientChanges(transPlayer);
            }

            Main.NewText($"You awakened: {quirkType}!", Color.Yellow);
        }
    }
}