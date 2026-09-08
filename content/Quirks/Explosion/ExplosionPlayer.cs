using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.System;
using Terraria.Audio;
using System.Collections.Generic;
using MyHeroMod.content.Dusts;
using MyHeroMod.content.Buffs;
using KhacesCore.Content.System.Interfaces;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Projectiles;
using System;


namespace MyHeroMod.content.Quirks.Explosion
{
    public partial class ExplosionPlayer : ModPlayer, IFlightModifier, IDashModifier, IStrainSource
    {
    
        public int StrainPenaltyPerSecond { get; set; }

        public void AddStrain(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            transPlayer.currentStrain += amount;

            if (transPlayer.currentStrain <= 0)
            {
                transPlayer.currentStrain = 0;
            }
            else if (transPlayer.currentStrain >= transPlayer.maxStrain)
            {
                transPlayer.currentStrain = transPlayer.maxStrain;
                IsClusterActive = false;
                Player.ClearBuff(ModContent.BuffType<ClusterBuff>()); 
            }
        }
        

        public bool IsClusterActive = false;

    

     

        public int SweatChangePerSecond { get; set; }
        public int MaxSweat = 100;  
        public int CurrentSweat = 0;

        public void AddSweat(int amount)
        {
            CurrentSweat += amount;
            if (CurrentSweat < 0) CurrentSweat = 0;
            if (CurrentSweat > MaxSweat) CurrentSweat = MaxSweat;
        }

        
        public bool TryConsumeSweat(int requiredSweat, int baseDrain)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            int actualDrain = baseDrain;

            
            if (transPlayer.Nature == NatureType.Resourceful)
            {
                actualDrain = (int)(baseDrain * 0.5f);
            }

            if (CurrentSweat >= requiredSweat)
            {
                AddSweat(-actualDrain);
                return true;
            }
            
            return false; 
        }

        public bool IsGrenadierBracersOn = false;
        public bool IsStrafePanzerOn = false;


        public override void OnRespawn()
        {
            IsClusterActive = false;
            
        }

        
        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.Explosion))
            {
                return; 
            }


            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

             if (CurrentSweat > 0) {
                Player.AddBuff(ModContent.BuffType<SweatBuff>(), 2);
                
            }

            if (!mainPlayer.HasActiveQuirk(QuirkType.Explosion))
                return;
            
            if (IsClusterActive)
            {
                var transPlayer = Player.GetModPlayer<TransformationPlayer>();

                Player.AddBuff(ModContent.BuffType<ClusterBuff>(), 2);

                StrainPenaltyPerSecond = Math.Max(1, (int)(transPlayer.maxStrain * 0.01f));
            }
            else
            {
                StrainPenaltyPerSecond = 0;
            }

            if (mainPlayer.CurrentStage >= QuirkStage.Advanced && mainPlayer.HasActiveQuirk(QuirkType.Explosion) && IsClusterActive)
            {
                Player.wingTimeMax = 150;

                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29;
                    Player.wings = -1;
                }
                Player.noFallDmg = true;
            }
            else if (mainPlayer.CurrentStage >= QuirkStage.Adequation && mainPlayer.HasActiveQuirk(QuirkType.Explosion))
            {
                Player.wingTimeMax = 30;

                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29;
                    Player.wings = -1;
                }

                Player.noFallDmg = true;
            }
            
           
        }

        

        public override void ResetEffects()
        {
            
            var ModPlayer = Player.GetModPlayer<TransformationPlayer>();
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            if (transPlayer.HasActiveQuirk(QuirkType.Explosion) && transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                Player.noFallDmg = true; 
            }

            IsGrenadierBracersOn = false;
            IsStrafePanzerOn = false;
            IsClusterActive = false;

            SweatChangePerSecond = CurrentSweat > 0 ? -1 : 0;
        }
        
        // public override void PreUpdate()
        // {
        //     if (CurrentSweat > 0)
        //     {
        //         sweatTimer++;
        //         if (sweatTimer >= 60)
        //         {
        //             sweatTimer = 0;
        //             int recoveryRate = 1;

        //             if (CurrentSweat > 0) 
        //             {
        //                 CurrentSweat -= recoveryRate;
                    
        //                 if (CurrentSweat < 0) CurrentSweat = 0;
        //             }
        
        //         }   
        //     }
        //     else
        //     {
        //         sweatTimer = 0;
        //     }
        // }
        

    // ===================================================Sync data ====================================================================================================
    public override void CopyClientState(ModPlayer targetCopy)
        {
            ExplosionPlayer clone = targetCopy as ExplosionPlayer;
            clone.IsClusterActive = IsClusterActive;
            clone.IsGrenadierBracersOn = IsGrenadierBracersOn;
            clone.IsStrafePanzerOn = IsStrafePanzerOn;
           
            clone.CurrentSweat = CurrentSweat;
            
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncExplosion); 
            packet.Write((byte)Player.whoAmI); 
            packet.Write(IsClusterActive);
            packet.Write(IsGrenadierBracersOn);
            packet.Write(IsStrafePanzerOn);
           
            packet.Write(CurrentSweat);
         
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            ExplosionPlayer clone = clientPlayer as ExplosionPlayer;
            if (IsClusterActive != clone.IsClusterActive || IsGrenadierBracersOn != clone.IsGrenadierBracersOn ||
             IsStrafePanzerOn != clone.IsStrafePanzerOn)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncExplosion);
                packet.Write((byte)Player.whoAmI);
                packet.Write(IsClusterActive);
                packet.Write(IsGrenadierBracersOn);
                packet.Write(IsStrafePanzerOn);
              
                packet.Write(CurrentSweat);
                packet.Send(-1, Player.whoAmI); 
            }
        }
    }
    }

