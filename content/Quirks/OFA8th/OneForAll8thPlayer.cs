using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.Quirks;
using MyHeroMod.content.Quirks.OFA8th.Projectiles;
using Terraria.Audio;
using MyHeroMod.content.System;
using System.Collections.Generic;
using KhacesCore.Content.System.Interfaces;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.Interfaces;
using System;
using MyHeroMod.content.Quirks.AllForOne;
using Humanizer;

namespace MyHeroMod.content.Quirks.OFA8th
{
    public partial class OneForAll8thPlayer : ModPlayer, IQuirkResetter, IDashModifier, IStrainSource
    {
        // ===================================== Embers ==========================================================

        public bool isQuirkless = false;

        public int timeUsed = 0;
        public readonly int maxTimeUsed = 7200;
        
        public int EmbersTime => Math.Max(maxTimeUsed - timeUsed, 0); 
        
        public int baseQuirklessTimer = 1200;

        // Total max capacity
        public int EmberBar => EmbersTime + Math.Max(baseQuirklessTimer, 0); 

        // The max value for your gauge
        public int maxFormTimer => EmberBar / 6;

        // The active gauge that goes up and down
        public int currentFormTimer = 0; 

        // ============================ Strain ==================================

        public int StrainPenaltyPerSecond { get; set; }

        public void AddStrain(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var AFOPlayer = Player.GetModPlayer<AllForOnePlayer>();

            if (isQuirkless)
            {
                // Chunk down the base capacity AND the active gauge so it reflects immediately
                baseQuirklessTimer -= amount;
                currentFormTimer -= amount;

                // Standard strain application
                transPlayer.currentStrain += amount;
                if (transPlayer.currentStrain <= 0)
                {
                    transPlayer.currentStrain = 0;
                }
                else if (transPlayer.currentStrain >= transPlayer.maxStrain)
                {
                    transPlayer.currentStrain = transPlayer.maxStrain;
                    Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
                }

                // If taking this strain chunk completely zeroed out the gauge, strip the quirk
                if (currentFormTimer <= 0)
                {
                    if (transPlayer.ActiveQuirks.Contains(QuirkType.OneForAll8th))
                        transPlayer.ActiveQuirks.Remove(QuirkType.OneForAll8th);

                    if (AFOPlayer.HasInternalQuirk(QuirkType.OneForAll8th))
                        AFOPlayer.InternalQuirks.Remove(QuirkType.OneForAll8th);

                    Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
                    FullReset();

                    timeUsed = 0;
                    isQuirkless = false;
                }

                return;
            }

            // Normal logic for when NOT quirkless
            transPlayer.currentStrain += amount;
            if (timeUsed < maxTimeUsed) { timeUsed += 1; }

            if (transPlayer.currentStrain <= 0) { transPlayer.currentStrain = 0; }
            else if (transPlayer.currentStrain >= transPlayer.maxStrain)
            {
                transPlayer.currentStrain = transPlayer.maxStrain;
                Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
            }
        }

        public int form = 0;
        
        public void FullReset()
        {
            form = 0; 
            currentFormTimer = maxFormTimer; // Refill the clock on a full reset
            Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
        }

        public override void OnRespawn()
        {
            form = 0;
            Player.ClearBuff(ModContent.BuffType<StockPileBuff>());
        }

        public override void PostUpdateMiscEffects()
        {
            int strainDrain = 0;

            if (form == 1)
            {
                strainDrain = 20;
            }
            else if (form == 2)
            {
                strainDrain = 30;
            }

            if (form != 0)
            {
                StrainPenaltyPerSecond = strainDrain;

                // --- CLOCK DRAIN LOGIC ---
                if (isQuirkless)
                {
                    if (currentFormTimer > 0)
                    {
                        currentFormTimer--; // Drain 1 tick per frame
                    }
                    else
                    {
                        // Embers ran out from continuous use!
                        var transPlayer = Player.GetModPlayer<TransformationPlayer>();
                        var AFOPlayer = Player.GetModPlayer<AllForOnePlayer>();

                        if (transPlayer.ActiveQuirks.Contains(QuirkType.OneForAll8th))
                            transPlayer.ActiveQuirks.Remove(QuirkType.OneForAll8th);

                        if (AFOPlayer.HasInternalQuirk(QuirkType.OneForAll8th))
                            AFOPlayer.InternalQuirks.Remove(QuirkType.OneForAll8th);

                        baseQuirklessTimer = 1200;
                        timeUsed = 0;
                        isQuirkless = false;
                        
                        FullReset();
                        transPlayer.ActiveForm = "None";
                    }
                }
            }
            else
            {
                StrainPenaltyPerSecond = 0;

                // --- CLOCK REGEN LOGIC ---
                if (isQuirkless && currentFormTimer < maxFormTimer)
                {
                    currentFormTimer++; // Regenerate 1 tick per frame when idle
                    
                    if (currentFormTimer > maxFormTimer) 
                    {
                        currentFormTimer = maxFormTimer;
                    }
                }
            }
        }

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            if (mainPlayer.HasActiveQuirk(QuirkType.OneForAll8th) && mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                Player.moveSpeed += 1.5f;
                Player.jumpSpeedBoost += 1.5f;
                Player.noFallDmg = true;

                Player.statDefense += 15;
                Player.GetDamage(DamageClass.Melee) += 0.20f;
                Player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
            }
        }

        // --- MULTIPLAYER ---
        public override void CopyClientState(ModPlayer targetCopy)
        {
            OneForAll8thPlayer clone = targetCopy as OneForAll8thPlayer;
            clone.form = form;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncOFA8th); 
            packet.Write((byte)Player.whoAmI); 
            packet.Write((int)form); 
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            OneForAll8thPlayer clone = clientPlayer as OneForAll8thPlayer;
            if (form != clone.form)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncOFA8th);
                packet.Write((byte)Player.whoAmI);
                packet.Write((int)form);
                packet.Send(-1, Player.whoAmI); 
            }
        }
    }
}