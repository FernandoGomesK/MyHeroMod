using Humanizer;
using KhacesCore.Content.System;
using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.OFA8th.Projectiles;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MyHeroMod.content.Quirks.OFA8th
{
    public partial class OneForAll8thPlayer : ModPlayer, IQuirkResetter, IDashModifier, IStrainSource
    {

        public CorePlayer Core => Player.GetModPlayer<CorePlayer>();
        // ===================================== Embers ==========================================================

        public bool isQuirkless = false;

        public int timeUsed = 0;
        public readonly int maxTimeUsed = 7200;

        public readonly int baseEmbersAmount = 1600;
        public int embersAmount = 0;
        public int initialEmbers = 0;

        public int EmberBar => Math.Max(embersAmount, 0);

        public void SetEmbers()
        {
            embersAmount = timeUsed + baseEmbersAmount;
            initialEmbers = timeUsed + baseEmbersAmount;
        }

        // ============================ Strain ==================================

        public int StrainPenaltyPerSecond { get; set; }

        public void AddStrain(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var AFOPlayer = Player.GetModPlayer<AllForOnePlayer>();

            if (isQuirkless)
            {
                embersAmount -= amount;
                Core.currentStrain += amount;
                if ( Core.currentStrain <= 0)
                {
                    Core.currentStrain = 0;
                }
                else if (Core.currentStrain >= Core.maxStrain)
                {
                    Core.currentStrain = Core.maxStrain;
                    Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
                }

                return;
            }

            Core.currentStrain += amount;
            
           if (timeUsed < maxTimeUsed) { timeUsed += 1; }

            if (Core.currentStrain <= 0) { Core.currentStrain = 0; }
            else if (Core.currentStrain >= Core.maxStrain)
            {
                Core.currentStrain = Core.maxStrain;
                Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
            }
        }

        public void RemoveOneForAll8th(TransformationPlayer transPlayer, AllForOnePlayer AFOPlayer)
        {
            if (transPlayer.ActiveQuirks.Contains(QuirkType.OneForAll8th))
                transPlayer.ActiveQuirks.Remove(QuirkType.OneForAll8th);

            if (AFOPlayer.HasInternalQuirk(QuirkType.OneForAll8th))
                AFOPlayer.InternalQuirks.Remove(QuirkType.OneForAll8th);

            Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
            FullReset();

            embersAmount = 0;
            timeUsed = 0;
            isQuirkless = false;
        }

        public int form = 0;
        
        public void FullReset()
        {
            form = 0; 
            Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
            isQuirkless = false;
            timeUsed = 0;
        }

        public override void OnRespawn()
        {
            form = 0;
            Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
        }

        public override void PostUpdateMiscEffects()
        {
            if (!Player.HasBuff(ModContent.BuffType<StockPileBuff>()))
            {
                form = 0;
            }

            int strainDrain = 0;

            if (form == 1)
            {
                strainDrain = 10;
            }
            else if (form == 2)
            {
                strainDrain = 20;
            }

            if (form != 0)
            {
                StrainPenaltyPerSecond = strainDrain;
            }
            else
            {
                StrainPenaltyPerSecond = 0;
            }
        }

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            if (mainPlayer.HasActiveQuirk(QuirkType.OneForAll8th))
            {
               
                

                if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
                {
                    Player.moveSpeed += 1.5f;
                    Player.jumpSpeedBoost += 1.5f;
                    Player.noFallDmg = true;

                    Player.statDefense += 15;
                    Player.GetDamage(DamageClass.Melee) += 0.20f;
                    Player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
                }
            }
        }

       
    }
}