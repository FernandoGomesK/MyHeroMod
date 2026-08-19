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

            
            if (quirkType == QuirkType.OpticBlast)
            {
                
                if (Main.rand.Next(100) < 80)
                {
                    transPlayer.CurrentVariant = QuirkVariant.Variant1;
                    Main.NewText("You awakened: Beams From your Eyes", Color.HotPink);
                }
                else
                {
                    transPlayer.CurrentVariant = QuirkVariant.Default;
                    Main.NewText($"You awakened: An Optic Blast?!", Color.Yellow);
                }
            }
            else if (quirkType == QuirkType.Engine)
            {
                if (Main.rand.Next(100) < 48)
                {
                    transPlayer.CurrentVariant = QuirkVariant.Variant1;
                    Main.NewText("You awakened: Engine(Tensei)", Color.White);
                }
                else
                {
                    transPlayer.CurrentVariant = QuirkVariant.Default;
                    Main.NewText("You awakened: Engine(Tenya)", Color.White);
                }
            }
            else 
            {
            
                transPlayer.CurrentVariant = QuirkVariant.Default;
                Main.NewText($"You awakened: {quirkType}!", Color.Yellow);
            }
            
            transPlayer.UpdateUnlockedSkills();

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                transPlayer.SendClientChanges(transPlayer);
            }
        }
    }
}