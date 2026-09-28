using MyHeroMod.content.Quirks.AllForOne;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.OFA9th
{
    // ========================================= Helper ===============================================================================
    public partial class OneForAll9thPlayer 
    {
        public bool hasLostDangerSense = false;
        public bool hasLostSmokescreen = false;
        public bool hasLostFloat = false;
        public bool hasLostFajin = false;
        public bool hasLostGearshift = false;
        public bool hasLostBlackwhip = false;

       public void MarkQuirkLost(QuirkType quirk)
        {
            switch (quirk)
            {
                case QuirkType.DangerSense: hasLostDangerSense = true; break;
                case QuirkType.BlackWhip: hasLostBlackwhip = true; break;
                case QuirkType.Float: hasLostFloat = true; break;
                case QuirkType.SmokeScreen: hasLostSmokescreen = true; break;
                case QuirkType.FaJin: hasLostFajin = true; break;
                case QuirkType.Gearshift: hasLostGearshift = true; break;
            }
        }
        private static void GrantQuirkOnce(List<QuirkType> list, QuirkType quirk, bool hasLost = false)
        {
            if (hasLost) return;
            if (!list.Contains(quirk)) list.Add(quirk);
        }

        public void UnlockQuirks()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            InternalQuirks.Clear(); 

            if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
            {
                if (transPlayer.ActiveQuirks.Contains(QuirkType.AllForOne))
                {
                    var afoPlayer = Player.GetModPlayer<AllForOnePlayer>();

                    if (transPlayer.CurrentStage >= QuirkStage.Initial)
                        GrantQuirkOnce(afoPlayer.InternalQuirks, QuirkType.OneForAll9th);

                    if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
                    {
                        GrantQuirkOnce(afoPlayer.InternalQuirks, QuirkType.DangerSense, hasLostDangerSense);
                        GrantQuirkOnce(afoPlayer.InternalQuirks, QuirkType.BlackWhip, hasLostBlackwhip);
                    }

                    if (transPlayer.CurrentStage >= QuirkStage.Advanced)
                    {
                        GrantQuirkOnce(afoPlayer.InternalQuirks, QuirkType.Float, hasLostFloat);
                        GrantQuirkOnce(afoPlayer.InternalQuirks, QuirkType.SmokeScreen, hasLostSmokescreen);
                        GrantQuirkOnce(afoPlayer.InternalQuirks, QuirkType.FaJin, hasLostFajin);
                    }

                    if (transPlayer.CurrentStage >= QuirkStage.Final)
                        GrantQuirkOnce(afoPlayer.InternalQuirks, QuirkType.Gearshift, hasLostGearshift);
                }

                if (transPlayer.ActiveQuirks.Count > 1)
                {
                    if (transPlayer.CurrentStage >= QuirkStage.Initial)
                        GrantQuirkOnce(InternalQuirks, QuirkType.OneForAll9th);
                }
                else 
                {
                    if (transPlayer.CurrentStage >= QuirkStage.Initial)
                        GrantQuirkOnce(InternalQuirks, QuirkType.OneForAll9th);

                    if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
                    {
                        GrantQuirkOnce(InternalQuirks, QuirkType.DangerSense, hasLostDangerSense);
                        GrantQuirkOnce(InternalQuirks, QuirkType.BlackWhip, hasLostBlackwhip);
                    }

                    if (transPlayer.CurrentStage >= QuirkStage.Advanced)
                    {
                        GrantQuirkOnce(InternalQuirks, QuirkType.Float, hasLostFloat);
                        GrantQuirkOnce(InternalQuirks, QuirkType.SmokeScreen, hasLostSmokescreen);
                        GrantQuirkOnce(InternalQuirks, QuirkType.FaJin, hasLostFajin);
                    }

                    if (transPlayer.CurrentStage >= QuirkStage.Final)
                        GrantQuirkOnce(InternalQuirks, QuirkType.Gearshift, hasLostGearshift);
                }
            }
        }


        
    }
}