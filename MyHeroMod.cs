using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using System.IO;
using Terraria;
using Terraria.ID;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA8th;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.Gearshift;
using MyHeroMod.content.Quirks.Erasure;
using MyHeroMod.content.Quirks.Explosion;
using MyHeroMod.content.Quirks.AllForOne;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using MyHeroMod.content.Quirks.FaJin;

namespace MyHeroMod
    {
        // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
        public class MyHeroMod : Mod
        {

            public override void Load()
    {
        if (Main.netMode != Terraria.ID.NetmodeID.Server)
        {
            
            ReLogic.Content.Asset<Effect> screenAsset = ModContent.Request<Effect>("MyHeroMod/Assets/Effects/TimeStopShader", ReLogic.Content.AssetRequestMode.ImmediateLoad);
            
        
            Filters.Scene["MyHeroMod:TimeStop"] = new Filter(new ScreenShaderData(screenAsset, "GreyscaleEffect"), EffectPriority.VeryHigh);
            Filters.Scene["MyHeroMod:TimeStop"].Load();
        }
    }

        public override void Unload()
{
    if (Main.netMode != NetmodeID.Server)
    {
        try
        {
            if (Filters.Scene != null)
            {
                
                if (Filters.Scene["MyHeroMod:TimeStop"] != null &&
                    Filters.Scene["MyHeroMod:TimeStop"].IsActive())
                {
                    Filters.Scene.Deactivate("MyHeroMod:TimeStop");
                }

                Filters.Scene["MyHeroMod:TimeStop"] = null;
            }
        }
        catch (Exception e)
        {
            Logger.Warn("TimeStop filter unload failed: " + e.Message);
        }
    }
}

    public enum MessageType : byte
            {
                SyncTransformationPlayer,
                SyncOFA8th,
                SyncOFA9th,
                SyncGearshift,
                SyncErasure,
                SyncExplosion,
                SyncAllForOne,
                SyncFaJin,

                StealNPCQuirk,

            }

            // 2. LER AS MENSAGENS QUE CHEGAM
            public override void HandlePacket(BinaryReader reader, int whoAmI)
            {
                MessageType msgType = (MessageType)reader.ReadByte();

                try
                {

                switch (msgType)
                {
                    case MessageType.SyncTransformationPlayer:
                    {
                        byte senderIndexInPacket = reader.ReadByte(); 

                        byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
                        if (playerIndex >= Main.maxPlayers) break; 

                        int activeQuirkCount = reader.ReadInt32();
                        int maxQuirks = Enum.GetValues(typeof(QuirkType)).Length;
                        activeQuirkCount = Math.Clamp(activeQuirkCount, 0, maxQuirks);

                        List<QuirkType> receivedQuirks = new List<QuirkType>(activeQuirkCount);
                        for (int i = 0; i < activeQuirkCount; i++)
                        {
                            int rawQuirk = reader.ReadInt32();
                            if (Enum.IsDefined(typeof(QuirkType), rawQuirk))
                                receivedQuirks.Add((QuirkType)rawQuirk);
                        }

                        int stageInt = reader.ReadInt32();
                        stageInt = Math.Clamp(stageInt, (int)QuirkStage.Initial, (int)QuirkStage.Final);

                        int natureInt= reader.ReadInt32();
                        int currentStrain = reader.ReadInt32();

                        // Read everything into temporary local variables first
                        string slot1 = reader.ReadString();
                        string slot2 = reader.ReadString();
                        string slot3 = reader.ReadString();
                        string slot4 = reader.ReadString();
                        string slot5 = reader.ReadString();
                        string slot6 = reader.ReadString();
                        string slot7 = reader.ReadString();
                        string slot8 = reader.ReadString();

                        bool useSecondaryBar = reader.ReadBoolean();
                        string currentRaceId = reader.ReadString();

                        // Now safely fetch the ModPlayer and assign the variables!
                        TransformationPlayer transPlayer = Main.player[playerIndex].GetModPlayer<TransformationPlayer>();
                        
                        transPlayer.ActiveQuirks = receivedQuirks;
                        transPlayer.CurrentStage = (QuirkStage)stageInt;

                        if (Enum.IsDefined(typeof(NatureType), natureInt))
                            transPlayer.Nature = (NatureType)natureInt;

                        transPlayer.currentStrain = currentStrain;
                        
                        transPlayer.Slot1 = slot1;
                        transPlayer.Slot2 = slot2;
                        transPlayer.Slot3 = slot3;
                        transPlayer.Slot4 = slot4;
                        transPlayer.Slot5 = slot5;
                        transPlayer.Slot6 = slot6;
                        transPlayer.Slot7 = slot7;
                        transPlayer.Slot8 = slot8;

                        transPlayer.UseSecondaryBar = useSecondaryBar;
                        transPlayer.CurrentRaceId = currentRaceId;

                        // If the server received this, package ALL of it back up and send to everyone else
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket();
                            packet.Write((byte)MessageType.SyncTransformationPlayer);
                            packet.Write(playerIndex);
                            
                            packet.Write(receivedQuirks.Count); 
                            foreach (var quirk in receivedQuirks) packet.Write((int)quirk);
                            
                            packet.Write(stageInt);
                            packet.Write(natureInt);
                            packet.Write(currentStrain);
                            
                            packet.Write(slot1);
                            packet.Write(slot2);
                            packet.Write(slot3);
                            packet.Write(slot4);
                            packet.Write(slot5);
                            packet.Write(slot6);
                            packet.Write(slot7);
                            packet.Write(slot8);
                            
                            packet.Write(useSecondaryBar);
                            packet.Write(currentRaceId);
                            
                            packet.Send(-1, playerIndex);
                        }
                        break;
                    }

                    case MessageType.SyncOFA8th:
                    {
                        byte senderIndexInPacket = reader.ReadByte();
                        int form = reader.ReadInt32();

                        byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
                        if (playerIndex >= Main.maxPlayers) break;

                        var ofa8 = Main.player[playerIndex].GetModPlayer<OneForAll8thPlayer>();
                        ofa8.form = form;

                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket();
                            packet.Write((byte)MessageType.SyncOFA8th);
                            packet.Write(playerIndex);
                            packet.Write(form);
                            packet.Send(-1, playerIndex);
                        }
                        break;
                    }

                        case MessageType.SyncOFA9th:
                        {
                            byte senderIndexInPacket = reader.ReadByte();
                            int percentage9 = reader.ReadInt32();
                            int fingers = reader.ReadInt32();

                            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
                            if (playerIndex >= Main.maxPlayers) break;

                            percentage9 = Math.Clamp(percentage9, 0, 100);

                            var ofa9 = Main.player[playerIndex].GetModPlayer<OneForAll9thPlayer>();
                            ofa9.percentage = percentage9;

                            if (Main.netMode == NetmodeID.Server)
                            {
                                ModPacket packet = GetPacket();
                                packet.Write((byte)MessageType.SyncOFA9th);
                                packet.Write(playerIndex);
                                packet.Write(percentage9);
                                packet.Write(fingers);
                                packet.Send(-1, playerIndex);
                            }
                            break;
                        }



                        case MessageType.SyncGearshift:
                        {
                            byte senderIndexInPacket = reader.ReadByte();
                            bool gear = reader.ReadBoolean();

                            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
                            if (playerIndex >= Main.maxPlayers) break;

                            Main.player[playerIndex].GetModPlayer<GearshiftPlayer>().GearActivation = gear;

                            if (Main.netMode == NetmodeID.Server)
                            {
                                ModPacket packet = GetPacket();
                                packet.Write((byte)MessageType.SyncGearshift);
                                packet.Write(playerIndex);
                                packet.Write(gear);
                                packet.Send(-1, playerIndex);
                            }
                            break;
                        }

                        case MessageType.SyncErasure:
                        {
                            byte senderIndexInPacket = reader.ReadByte();
                            bool erasing = reader.ReadBoolean();
                            bool goggles = reader.ReadBoolean();
                            int eyetimer = reader.ReadInt32();

                            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
                            if (playerIndex >= Main.maxPlayers) break;

                            eyetimer = Math.Max(0, eyetimer);

                            var erase = Main.player[playerIndex].GetModPlayer<ErasurePlayer>();
                            erase.isErasureActive = erasing;
                            erase.isYellowGogglesOn = goggles;
                            erase.eyeTimer = eyetimer;

                            if (Main.netMode == NetmodeID.Server)
                            {
                                ModPacket packet = GetPacket();
                                packet.Write((byte)MessageType.SyncErasure);
                                packet.Write(playerIndex);
                                packet.Write(erasing);
                                packet.Write(goggles);
                                packet.Write(eyetimer);
                                packet.Send(-1, playerIndex);
                            }
                            break;
                        }

                        case MessageType.SyncExplosion:
                        {
                            byte senderIndexInPacket = reader.ReadByte();
                            bool cluster = reader.ReadBoolean();
                            bool grenadier = reader.ReadBoolean();
                            bool panzer = reader.ReadBoolean();
                            
                           

                            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
                            if (playerIndex >= Main.maxPlayers) break;

                            

                            var explode = Main.player[playerIndex].GetModPlayer<ExplosionPlayer>();
                            explode.IsClusterActive = cluster;
                            explode.IsGrenadierBracersOn = grenadier;
                            explode.IsStrafePanzerOn = panzer;
                            
                            

                            if (Main.netMode == NetmodeID.Server)
                            {
                                ModPacket packet = GetPacket();
                                packet.Write((byte)MessageType.SyncExplosion);
                                packet.Write(playerIndex);
                                packet.Write(cluster);
                                packet.Write(grenadier);
                                packet.Write(panzer);
                                
                                
                                packet.Send(-1, playerIndex);
                            }
                            break;
                        }

                        case MessageType.SyncAllForOne:
                        {
                            byte senderIndexInPacket = reader.ReadByte();
                            int quirkCount = reader.ReadInt32();

                            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
                            if (playerIndex >= Main.maxPlayers) break;

                            int maxQuirks = Enum.GetValues(typeof(QuirkType)).Length;
                            quirkCount = Math.Clamp(quirkCount, 0, maxQuirks);

                            var afoPlayer = Main.player[playerIndex].GetModPlayer<AllForOnePlayer>();
                            afoPlayer.InternalQuirks.Clear();

                            for (int i = 0; i < quirkCount; i++)
                            {
                                int rawQuirk = reader.ReadInt32();
                                if (Enum.IsDefined(typeof(QuirkType), rawQuirk))
                                    afoPlayer.InternalQuirks.Add((QuirkType)rawQuirk);
                            }

                            if (Main.netMode == NetmodeID.Server)
                            {
                                ModPacket packet = GetPacket();
                                packet.Write((byte)MessageType.SyncAllForOne);
                                packet.Write(playerIndex);
                                packet.Write(afoPlayer.InternalQuirks.Count);
                                foreach (var quirk in afoPlayer.InternalQuirks)
                                {
                                    packet.Write((int)quirk);
                                }
                                packet.Send(-1, playerIndex);
                            }
                            break;
                        }

                        case MessageType.SyncFaJin:
                        {
                            byte senderIndexInPacket = reader.ReadByte();
                            int fajinCharges = reader.ReadInt32();
                            bool isFajinActive = reader.ReadBoolean();

                            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
                            if (playerIndex >= Main.maxPlayers) break;

                            var fajinPlayer = Main.player[playerIndex].GetModPlayer<FajinPlayer>();
                            fajinPlayer.FaJinCharges = fajinCharges;
                            fajinPlayer.isFaJinActive = isFajinActive;

                            if (Main.netMode == NetmodeID.Server)
                            {
                                ModPacket packet = GetPacket();
                                packet.Write((byte)MessageType.SyncFaJin);
                                packet.Write(playerIndex);
                                packet.Write(fajinCharges);
                                packet.Write(isFajinActive);
                                packet.Send(-1, playerIndex);
                            }
                            break;
                        }

                        case MessageType.StealNPCQuirk:
                        {
                            int npcWhoAmI = reader.ReadInt32(); 

                            if (npcWhoAmI >= 0 && npcWhoAmI < Main.maxNPCs)
                            {
                                NPC targetNPC = Main.npc[npcWhoAmI];
                                if (targetNPC.active)
                                {
                                    var quirkNPC = targetNPC.GetGlobalNPC<QuirkGlobalNPC>();
                                    
                                    
                                    if (quirkNPC.HasQuirk)
                                    {
                                        quirkNPC.HasQuirk = false;
                                        quirkNPC.AssignedQuirk = QuirkType.Quirkless;

                                        
                                        if (targetNPC.boss)
                                        {
                                            targetNPC.lifeMax = (int)(targetNPC.lifeMax / 1.5f);
                                            targetNPC.damage = (int)(targetNPC.damage / 1.5f);
                                        }
                                        else
                                        {
                                            targetNPC.lifeMax = (int)(targetNPC.lifeMax / 4f);
                                            targetNPC.damage = (int)(targetNPC.damage / 3f);
                                        }

                                        
                                        if (targetNPC.life > targetNPC.lifeMax)
                                            targetNPC.life = targetNPC.lifeMax;

                                        
                                        if (Main.netMode == NetmodeID.Server)
                                        {
                                            targetNPC.netUpdate = true; 
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }   
                catch (Exception e)
                {
                    Logger.Warn($"[MyHeroMod] Failed handling packet '{msgType}' from player {whoAmI}: {e}");
                }
            }     
            }
        }
    
    