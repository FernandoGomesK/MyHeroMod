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
        #region Ciclo de Vida do Mod (Load/Unload)

        /// <summary>
        /// Carrega o shader de tela usado pelo efeito visual de "Time Stop" (apenas em clientes).
        /// </summary>
        public override void Load()
        {
            if (Main.netMode != Terraria.ID.NetmodeID.Server)
            {
                ReLogic.Content.Asset<Effect> screenAsset = ModContent.Request<Effect>("MyHeroMod/Assets/Effects/TimeStopShader", ReLogic.Content.AssetRequestMode.ImmediateLoad);

                Filters.Scene["MyHeroMod:TimeStop"] = new Filter(new ScreenShaderData(screenAsset, "GreyscaleEffect"), EffectPriority.VeryHigh);
                Filters.Scene["MyHeroMod:TimeStop"].Load();
            }
        }

        /// <summary>
        /// Desativa e libera o filtro de tela do "Time Stop" ao descarregar o mod, evitando
        /// que um filtro ativo sobreviva a um reload.
        /// </summary>
       
        public override void Unload()
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            Main.QueueMainThreadAction(() =>
            {
                try
                {
                    if (Filters.Scene != null)
                    {
                        string key = "MyHeroMod:TimeStop";
                        var filter = Filters.Scene[key];
                        if (filter != null && filter.IsActive())
                            Filters.Scene.Deactivate(key);

                        if (Filters.Scene["MyHeroMod:TimeStop"].IsActive())
                        {
                            Filters.Scene["MyHeroMod:TimeStop"].Deactivate();
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn("TimeStop filter unload failed: " + e.Message);
                }
            });
        }

        #endregion


        #region Tipos de Mensagem de Rede

        /// <summary>
        /// Identificadores de todos os pacotes de rede (netcode) trocados pelo mod.
        /// </summary>
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
            StealPlayerQuirk,
        }

        #endregion


        #region Manipulação de Pacotes (Netcode)

        /// <summary>
        /// Ponto de entrada de todo pacote recebido pelo mod; identifica o tipo de mensagem
        /// e despacha para o handler correspondente. Qualquer exceção durante o processamento
        /// é registrada no log em vez de derrubar a conexão.
        /// </summary>
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            try
            {
                switch (msgType)
                {
                    case MessageType.SyncTransformationPlayer:
                        HandleSyncTransformationPlayer(reader, whoAmI);
                        break;

                    case MessageType.SyncOFA9th:
                        HandleSyncOFA9th(reader, whoAmI);
                        break;

                    case MessageType.SyncOFA8th:
                        HandleSyncOFA8th(reader, whoAmI);
                        break;

                    case MessageType.SyncGearshift:
                        HandleSyncGearshift(reader, whoAmI);
                        break;

                    case MessageType.SyncErasure:
                        HandleSyncErasure(reader, whoAmI);
                        break;

                    case MessageType.SyncExplosion:
                        HandleSyncExplosion(reader, whoAmI);
                        break;

                    case MessageType.SyncAllForOne:
                        HandleSyncAllForOne(reader, whoAmI);
                        break;

                    case MessageType.SyncFaJin:
                        HandleSyncFaJin(reader, whoAmI);
                        break;

                    case MessageType.StealNPCQuirk:
                        HandleStealNPCQuirk(reader);
                        break;

                    case MessageType.StealPlayerQuirk:
                        HandleStealPlayerQuirk(reader, whoAmI);
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Warn($"[MyHeroMod] Failed handling packet '{msgType}' from player {whoAmI}: {e}");
            }
        }

        /// <summary>
        /// Sincroniza a lista de quirks ativos, o estágio, a variante e a natureza
        /// do <see cref="TransformationPlayer"/>.
        /// </summary>
        private void HandleSyncTransformationPlayer(BinaryReader reader, int whoAmI)
        {
            byte senderIndexInPacket = reader.ReadByte();
            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
            if (playerIndex >= Main.maxPlayers) return;

            int activeQuirkCount = reader.ReadInt32();
            int maxQuirks = Enum.GetValues(typeof(QuirkType)).Length;
            activeQuirkCount = Math.Clamp(activeQuirkCount, 0, maxQuirks);

            List<QuirkType> receivedQuirks = new(activeQuirkCount);
            for (int i = 0; i < activeQuirkCount; i++)
            {
                int rawQuirk = reader.ReadInt32();
                if (Enum.IsDefined(typeof(QuirkType), rawQuirk))
                    receivedQuirks.Add((QuirkType)rawQuirk);
            }

            int stageInt = reader.ReadInt32();
            int variantInt = reader.ReadInt32();
            int natureInt = reader.ReadInt32();

            TransformationPlayer transPlayer = Main.player[playerIndex].GetModPlayer<TransformationPlayer>();

            transPlayer.ActiveQuirks = receivedQuirks;
            transPlayer.CurrentStage = (QuirkStage)stageInt;

            if (Enum.IsDefined(typeof(QuirkVariant), variantInt))
                transPlayer.CurrentVariant = (QuirkVariant)variantInt;

            if (Enum.IsDefined(typeof(NatureType), natureInt))
                transPlayer.Nature = (NatureType)natureInt;

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = GetPacket();
                packet.Write((byte)MessageType.SyncTransformationPlayer);
                packet.Write(playerIndex);

                packet.Write(receivedQuirks.Count);
                foreach (var quirk in receivedQuirks) packet.Write((int)quirk);

                packet.Write(stageInt);
                packet.Write(variantInt);
                packet.Write(natureInt);

                packet.Send(-1, playerIndex);
            }
        }

        /// <summary>Sincroniza o estado do One For All (9th): porcentagem, dedos, embers e quirkless.</summary>
        private void HandleSyncOFA9th(BinaryReader reader, int whoAmI)
        {
            byte senderIndexInPacket = reader.ReadByte();
            int percentage9 = reader.ReadInt32();
            int fingers = reader.ReadInt32();

            int timeUsed = reader.ReadInt32();
            int embersAmount = reader.ReadInt32();
            int initialEmbers = reader.ReadInt32();
            bool isQuirkless = reader.ReadBoolean();

            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
            if (playerIndex >= Main.maxPlayers) return;

            percentage9 = Math.Clamp(percentage9, 0, 100);

            var ofa9 = Main.player[playerIndex].GetModPlayer<OneForAll9thPlayer>();
            ofa9.percentage = percentage9;
            ofa9.currentFingers = fingers;
            ofa9.timeUsed = timeUsed;
            ofa9.embersAmount = embersAmount;
            ofa9.initialEmbers = initialEmbers;
            ofa9.isQuirkless = isQuirkless;

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = GetPacket();
                packet.Write((byte)MessageType.SyncOFA9th);
                packet.Write(playerIndex);
                packet.Write(percentage9);
                packet.Write(fingers);

                packet.Write(timeUsed);
                packet.Write(embersAmount);
                packet.Write(initialEmbers);
                packet.Write(isQuirkless);
                packet.Send(-1, playerIndex);
            }
        }

        /// <summary>Sincroniza o estado do One For All (8th): forma ativa, embers e quirkless.</summary>
        private void HandleSyncOFA8th(BinaryReader reader, int whoAmI)
        {
            byte senderIndex = reader.ReadByte();
            int form = reader.ReadInt32();
            int timeUsed = reader.ReadInt32();
            int emberAmount = reader.ReadInt32();
            int initialEmbers = reader.ReadInt32();
            bool isQuirkless = reader.ReadBoolean();

            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndex;
            if (playerIndex >= Main.maxPlayers) return;

            var ofa8 = Main.player[playerIndex].GetModPlayer<OneForAll8thPlayer>();
            ofa8.form = form;
            ofa8.timeUsed = timeUsed;
            ofa8.embersAmount = emberAmount;
            ofa8.initialEmbers = initialEmbers;
            ofa8.isQuirkless = isQuirkless;

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = GetPacket();
                packet.Write((byte)MessageType.SyncOFA8th);
                packet.Write(playerIndex);
                packet.Write(form);
                packet.Write(timeUsed);
                packet.Write(emberAmount);
                packet.Write(initialEmbers);
                packet.Write(isQuirkless);
                packet.Send(-1, playerIndex);
            }
        }

        /// <summary>Sincroniza a ativação do Gearshift.</summary>
        private void HandleSyncGearshift(BinaryReader reader, int whoAmI)
        {
            byte senderIndexInPacket = reader.ReadByte();
            bool gear = reader.ReadBoolean();

            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
            if (playerIndex >= Main.maxPlayers) return;

            Main.player[playerIndex].GetModPlayer<GearshiftPlayer>().GearActivation = gear;

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = GetPacket();
                packet.Write((byte)MessageType.SyncGearshift);
                packet.Write(playerIndex);
                packet.Write(gear);
                packet.Send(-1, playerIndex);
            }
        }

        /// <summary>Sincroniza o estado de Erasure: se está apagando, os óculos amarelos e o timer dos olhos.</summary>
        private void HandleSyncErasure(BinaryReader reader, int whoAmI)
        {
            byte senderIndexInPacket = reader.ReadByte();
            bool erasing = reader.ReadBoolean();
            bool goggles = reader.ReadBoolean();
            int eyetimer = reader.ReadInt32();

            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
            if (playerIndex >= Main.maxPlayers) return;

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
        }

        /// <summary>Sincroniza o estado de Explosion: cluster, grenadier, panzer e o suor atual.</summary>
        private void HandleSyncExplosion(BinaryReader reader, int whoAmI)
        {
            byte senderIndexInPacket = reader.ReadByte();
            bool cluster = reader.ReadBoolean();
            bool grenadier = reader.ReadBoolean();
            bool panzer = reader.ReadBoolean();
            int currentSweat = reader.ReadInt32();

            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
            if (playerIndex >= Main.maxPlayers) return;

            var explode = Main.player[playerIndex].GetModPlayer<ExplosionPlayer>();
            explode.IsClusterActive = cluster;
            explode.IsGrenadierBracersOn = grenadier;
            explode.IsStrafePanzerOn = panzer;
            explode.CurrentSweat = currentSweat;

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = GetPacket();
                packet.Write((byte)MessageType.SyncExplosion);
                packet.Write(playerIndex);
                packet.Write(cluster);
                packet.Write(grenadier);
                packet.Write(panzer);
                packet.Write(currentSweat);
                packet.Send(-1, playerIndex);
            }
        }

        /// <summary>Sincroniza a lista de quirks internos roubados via All For One.</summary>
        private void HandleSyncAllForOne(BinaryReader reader, int whoAmI)
        {
            byte senderIndexInPacket = reader.ReadByte();
            int quirkCount = reader.ReadInt32();
            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;

            var afoPlayer = Main.player[playerIndex].GetModPlayer<AllForOnePlayer>();

            if (playerIndex >= Main.maxPlayers) return;

            int maxQuirks = Enum.GetValues(typeof(QuirkType)).Length;
            quirkCount = Math.Clamp(quirkCount, 0, maxQuirks);

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
        }

        /// <summary>Synchronizes Fa Jin Charges</summary>
        private void HandleSyncFaJin(BinaryReader reader, int whoAmI)
        {
            byte senderIndexInPacket = reader.ReadByte();
            int fajinCharges = reader.ReadInt32();
            bool isFajinActive = reader.ReadBoolean();

            byte playerIndex = Main.netMode == NetmodeID.Server ? (byte)whoAmI : senderIndexInPacket;
            if (playerIndex >= Main.maxPlayers) return;

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
        }

        /// <summary>
        /// Removes the quirk from a npc and debuffs it's stats
        /// </summary>
        private static void HandleStealNPCQuirk(BinaryReader reader)
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
        }

        /// <summary>
        /// Transfere um quirk de um jogador-alvo para o jogador atacante: remove do alvo
        /// (incluindo o registro no OFA9th) e adiciona como quirk interno do All For One do atacante.
        /// </summary>
        private void HandleStealPlayerQuirk(BinaryReader reader, int whoAmI)
        {
            byte attackerId = reader.ReadByte();
            byte targetId = reader.ReadByte();
            QuirkType stolenQuirk = (QuirkType)reader.ReadInt32();

            Player target = Main.player[targetId];
            Player attacker = Main.player[attackerId];

            var targetTrans = target.GetModPlayer<TransformationPlayer>();
            var targetOfa9th = target.GetModPlayer<OneForAll9thPlayer>();

            targetTrans.ActiveQuirks.Remove(stolenQuirk);
            targetOfa9th.MarkQuirkLost(stolenQuirk);
            targetTrans.UpdateUnlockedSkills();
            targetOfa9th.UnlockQuirks();

            var afosyncPlayer = attacker.GetModPlayer<AllForOnePlayer>();
            afosyncPlayer.TryStealQuirk(stolenQuirk);
            attacker.GetModPlayer<TransformationPlayer>().UpdateUnlockedSkills();

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = GetPacket();
                packet.Write((byte)MessageType.StealPlayerQuirk);
                packet.Write(attackerId);
                packet.Write(targetId);
                packet.Write((int)stolenQuirk);
                packet.Send(-1, whoAmI);
            }
        }

        #endregion
    }
}
