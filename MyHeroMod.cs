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

    namespace MyHeroMod
    {
        // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
        public class MyHeroMod : Mod
        {
    public enum MessageType : byte
            {
                SyncTransformationPlayer,
                SyncOFA8th,
                SyncOFA9th
            }

            // 2. LER AS MENSAGENS QUE CHEGAM
            public override void HandlePacket(BinaryReader reader, int whoAmI)
            {
                // Lê o primeiro byte para saber de que tipo de mensagem se trata
                MessageType msgType = (MessageType)reader.ReadByte();

                switch (msgType)
                {
                    case MessageType.SyncTransformationPlayer:
                        // Lê os dados exatamente na mesma ordem em que foram enviados
                        byte playernumber = reader.ReadByte();
                        int quirkInt = reader.ReadInt32();
                        int stageInt = reader.ReadInt32();

                        // Aplica os dados recebidos ao jogador correto no mundo
                        TransformationPlayer transPlayer = Main.player[playernumber].GetModPlayer<TransformationPlayer>();
                        transPlayer.SelectedQuirk = (QuirkType)quirkInt;
                        transPlayer.CurrentStage = (QuirkStage)stageInt;

                        // O SEGREDO DO MULTIPLAYER:
                        // Se o Servidor recebeu esta mensagem, ele tem de a reencaminhar para TODOS os outros clientes!
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket();
                            packet.Write((byte)MessageType.SyncTransformationPlayer);
                            packet.Write(playernumber);
                            packet.Write(quirkInt);
                            packet.Write(stageInt);
                            // Envia para todos (-1) EXCETO para quem enviou originalmente (playernumber)
                            packet.Send(-1, playernumber); 
                        }
                        break;

                        case MessageType.SyncOFA8th:
                        byte player8 = reader.ReadByte();
                        int form8 = reader.ReadInt32();

                        Main.player[player8].GetModPlayer<OneForAll8thPlayer>().form = form8;

                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket();
                            packet.Write((byte)MessageType.SyncOFA8th);
                            packet.Write(player8);
                            packet.Write(form8);
                            packet.Send(-1, player8); 
                        }
                        break;

                        case MessageType.SyncOFA9th:
                        byte player9 = reader.ReadByte();
                        int percentage9 = reader.ReadInt32();
                        bool activating9 = reader.ReadBoolean();

                        var ofa9 = Main.player[player9].GetModPlayer<OneForAll9thPlayer>();
                        ofa9.percentage = percentage9;
                        ofa9.Activating = activating9;

                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket();
                            packet.Write((byte)MessageType.SyncOFA9th);
                            packet.Write(player9);
                            packet.Write(percentage9);
                            packet.Write(activating9);
                            packet.Send(-1, player9); 
                        }
                        break;

                        case MessageType.SyncGearshift:
                        byte playerGear = reader.ReadByte();
                        int gear = reader.ReadBoolean();

                        

                        Main.player[playerGear].GetModPlayer<GearshiftPlayer>().GearActivation = gear;

                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket();
                            packet.Write((byte)MessageType.SyncGearshift);
                            packet.Write(playerGear);
                            packet.Write(gear);
                            packet.Send(-1, playerGear); 
                        }
                        break;
        }
    }
        }}