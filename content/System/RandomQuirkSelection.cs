using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod;
using Terraria.ID;
using System.Collections.Generic;

namespace MyHeroMod.content.System
{
    public class RandomQuirkSelection
    {

        public static Dictionary<QuirkType, int> QuirkWeights = new Dictionary<QuirkType, int>
        {
            // S-Tier (Cost 4) - Very Rare (~1% chance each)
            // Weight: 2
            { QuirkType.OneForAll9th, 2 },
            { QuirkType.OneForAll8th, 2 },
            { QuirkType.AllForOne, 2 },

            // A-Tier (Cost 3) - Rare (~3-4% chance each)
            // Weight: 10
            { QuirkType.Overhaul, 10 },
            { QuirkType.Decay, 10 },
            { QuirkType.HalfColdHalfHot, 10 },
            { QuirkType.Explosion, 10 },
            { QuirkType.Blueflame, 10 },
            { QuirkType.HellFlames, 10 },
            { QuirkType.Engine, 10 },
            { QuirkType.OpticBlast, 10 },
            { QuirkType.Overclock, 10 },
            { QuirkType.SuperRegeneration, 10 },
            { QuirkType.FierceWings, 10 },
            { QuirkType.DarkShadow, 10 },

            // B-Tier (Cost 2) - Uncommon (~7% chance each)
            // Weight: 25
            { QuirkType.Erasure, 25 },
            // { QuirkType.Hardening, 25 },
            { QuirkType.Rabbit, 25 },
            { QuirkType.SlideAndGlide, 25 },
            { QuirkType.ZeroGravity, 25 },
            { QuirkType.SpringLikeLimbs, 25 },

            // C-Tier (Cost 1) - Common (~11% chance each)
            // Weight: 40
            { QuirkType.Tape, 40 },
            { QuirkType.FaJin, 40 },
            { QuirkType.Float, 40 },
            { QuirkType.DangerSense, 40 },
            { QuirkType.BlackWhip, 40 },
            { QuirkType.Gearshift, 40 },
            { QuirkType.Flight, 40 },
            { QuirkType.Rivet, 40 }
        };

        public static void SelectRandomQuirk()
        {
            var transPlayer = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();

           
            int totalWeight = 0;
            List<KeyValuePair<QuirkType, int>> availableQuirks = new List<KeyValuePair<QuirkType, int>>();

            foreach (var kvp in QuirkWeights)
            {
                if (kvp.Key != QuirkType.Quirkless && !transPlayer.HasActiveQuirk(kvp.Key))
                {
                    availableQuirks.Add(kvp);
                    totalWeight += kvp.Value;
                }
            }

            
            if (availableQuirks.Count == 0)
            {
                Main.NewText("Your body cannot physically hold any more Quirks!", Color.Red);
                return;
            }

            // 3. Roll a random number against the remaining total weight
            int randomRoll = Main.rand.Next(totalWeight);
            QuirkType quirkType = QuirkType.Quirkless;

            foreach (var kvp in availableQuirks)
            {
                randomRoll -= kvp.Value;
                if (randomRoll < 0)
                {
                    quirkType = kvp.Key;
                    break;
                }
            }

            
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

        public static QuirkType GetRandomQuirkForNPC()
        {
            int totalWeight = 0;
            
    
            foreach (var weight in QuirkWeights.Values)
            {
                totalWeight += weight;
            }

         
            int randomRoll = Main.rand.Next(totalWeight);

            
            foreach (var kvp in QuirkWeights)
            {
                randomRoll -= kvp.Value;
                if (randomRoll < 0)
                {
                    return kvp.Key;
                }
            }

            return QuirkType.Quirkless;
        }
    }
}