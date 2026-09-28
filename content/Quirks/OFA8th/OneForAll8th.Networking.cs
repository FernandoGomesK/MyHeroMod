using MyHeroMod.content.System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using KhacesCore.Content.System.Interfaces;
using Terraria.Audio;
using Terraria.ID;
using JetBrains.Annotations;
using Terraria.ModLoader.IO;
using MyHeroMod.content.System.Interfaces;

namespace MyHeroMod.content.Quirks.OFA8th
{
    public partial class OneForAll8thPlayer : ModPlayer, IQuirkResetter, IDashModifier
    {

         // ================================== MULTIPLAYER =================================================
        public override void CopyClientState(ModPlayer targetCopy)
        {
            OneForAll8thPlayer clone = targetCopy as OneForAll8thPlayer;
            clone.form = form;
            clone.timeUsed = timeUsed;
            clone.embersAmount = embersAmount;
            clone.initialEmbers = initialEmbers;
            clone.isQuirkless = isQuirkless;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncOFA8th); 
            packet.Write((byte)Player.whoAmI); 
            packet.Write((int)form);
            packet.Write(timeUsed);
            packet.Write(embersAmount);
            packet.Write(initialEmbers);
            packet.Write(isQuirkless);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            OneForAll8thPlayer clone = clientPlayer as OneForAll8thPlayer;
            
        
            if (form != clone.form || timeUsed != clone.timeUsed || embersAmount != clone.embersAmount)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncOFA8th);
                packet.Write((byte)Player.whoAmI);
                packet.Write((int)form);
                packet.Write(timeUsed);
                packet.Write(embersAmount);
                packet.Write(initialEmbers);
                packet.Send(-1, Player.whoAmI); 
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag["ofa8_form"] = form;
            tag["ofa8_timeUsed"] = timeUsed;
            tag["ofa8_embersAmount"] = embersAmount;
            tag["ofa8_initialEmbers"] = initialEmbers;
            tag["ofa8_isQuirkless"] = isQuirkless;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("ofa8_form"))
                form = tag.GetInt("ofa8_form");

            if (tag.ContainsKey("ofa8_timeUsed"))
                timeUsed = tag.GetInt("ofa8_timeUsed");

            if (tag.ContainsKey("ofa8_embersAmount"))
                embersAmount = tag.GetInt("ofa8_embersAmount"); 

            if (tag.ContainsKey("ofa8_initialEmbers"))
                initialEmbers = tag.GetInt("ofa8_initialEmbers");

            if (tag.ContainsKey("ofa8_isQuirkless"))
                isQuirkless = tag.GetBool("ofa8_isQuirkless");
        }
    
    }
}