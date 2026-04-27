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
using MyHeroMod.Buffs;


namespace MyHeroMod.content.Quirks.Explosion
{
    public partial class ExplosionPlayer : ModPlayer
    {
        

        public bool IsClusterActive = false;

        public int MaxSweat = 100;  
        public int CurrentSweat = 0;
        public int sweatTimer = 0;

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
                Player.AddBuff(ModContent.BuffType<ClusterBuff>(),2 );
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
        }
        
        public override void PreUpdate()
        {
             if (CurrentSweat > 0)
{
    sweatTimer++;

    
    if (sweatTimer >= 60)
    {
        sweatTimer = 0;
        
        
        int recoveryRate = 1;

        
        // if (IsCombatVestAlphaOn) recoveryRate += 1; 
        // if (IsCombatVestBetaOn)  recoveryRate += 5; 

        
        if (CurrentSweat > 0) 
        {
            CurrentSweat -= recoveryRate;
           
            if (CurrentSweat < 0) CurrentSweat = 0;
        }
        
    }
}
else
{
    sweatTimer = 0;
}
        }
        public override void PostUpdate()
        {
            if (!Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.Explosion))
            {
                return; 
            }   
            
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (mainPlayer.HasActiveQuirk(QuirkType.Explosion) && mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
            if (Player.velocity.Y != 0 && !Player.mount.Active)
                {
                    // Lado Esquerdo (Fogo)
                    if (Main.rand.NextBool(10)) 
                    {
                        int dustFire = Dust.NewDust(
                            Player.position + new Vector2(-5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Torch, 
                            0, 2f, 100, default, 3.5f 
                        );
                        int dustSmoke = Dust.NewDust(
                            Player.position + new Vector2(-5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Ash, 
                            0, 2f, 100, default, 3.5f 
                        );
                        Main.dust[dustFire].noGravity = true;
                        Main.dust[dustFire].velocity *= 0.5f; 
                        Main.dust[dustSmoke].noGravity = true;
                        Main.dust[dustSmoke].velocity *= 0.5f;
                    }

                    // Lado Direito (Fogo tbm)
                    if (Main.rand.NextBool(10))
                    {
                        int dustFire2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / 2, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Torch, 
                            0, 2f, 100, default, 3.5f
                        );
                        int dustSmoke2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / -5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Ash, 
                            0, 2f, 100, default, 3.5f 
                        );
                        Main.dust[dustFire2].noGravity = true;
                        Main.dust[dustFire2].velocity *= 0.5f;
                        Main.dust[dustSmoke2].noGravity = true;
                        Main.dust[dustSmoke2].velocity *= 0.5f;
                    }
                    // cluster
                    if (Main.rand.NextBool(6) && IsClusterActive)
                    {
                         int dustFire2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / 2, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            ModContent.DustType<ClusterDust>(), 
                            0, 2f, 100, default, 2.5f
                        );
                        int dustSmoke2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / -5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            ModContent.DustType<ClusterDust>(), 
                            0, 2f, 100, default, 2.5f 
                        );
                        Main.dust[dustFire2].noGravity = true;
                        Main.dust[dustFire2].velocity *= 0.5f;
                        Main.dust[dustSmoke2].noGravity = true;
                        Main.dust[dustSmoke2].velocity *= 0.5f;
                        
                    }
                }
           
            
        }
    }
    public override void CopyClientState(ModPlayer targetCopy)
        {
            ExplosionPlayer clone = targetCopy as ExplosionPlayer;
            clone.IsClusterActive = IsClusterActive;
            clone.IsGrenadierBracersOn = IsGrenadierBracersOn;
            clone.IsStrafePanzerOn = IsStrafePanzerOn;
            clone.sweatTimer = sweatTimer;
            
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncExplosion); 
            packet.Write((byte)Player.whoAmI); 
            packet.Write(IsClusterActive);
            packet.Write(IsGrenadierBracersOn);
            packet.Write(IsStrafePanzerOn);
            packet.Write(sweatTimer);
         
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            ExplosionPlayer clone = clientPlayer as ExplosionPlayer;
            if (IsClusterActive != clone.IsClusterActive || IsGrenadierBracersOn != clone.IsGrenadierBracersOn ||
             IsStrafePanzerOn != clone.IsStrafePanzerOn || sweatTimer != clone.sweatTimer)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncGearshift);
                packet.Write((byte)Player.whoAmI);
                packet.Write(IsClusterActive);
                packet.Write(IsGrenadierBracersOn);
                packet.Write(IsStrafePanzerOn);
                packet.Write(sweatTimer);
                packet.Send(-1, Player.whoAmI); 
            }
        }
    }
    }

