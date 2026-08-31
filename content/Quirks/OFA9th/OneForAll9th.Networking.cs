
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using Terraria.Audio;
using System.Collections.Generic;

using rail;
using MyHeroMod.content.System;
using KhacesCore.Content.System.Interfaces;
using KhacesCore.Content.System;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhip;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackChain;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhipStun;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.PinpointFocus;
using MyHeroMod.content.System.Interfaces;




namespace MyHeroMod.content.Quirks.OFA9th
{
    // ========================================= Net ===============================================================================
    public partial class OneForAll9thPlayer : ModPlayer, IQuirkResetter, IDashModifier, IStrainSource
    {
        public override void CopyClientState(ModPlayer targetCopy)
        {
            OneForAll9thPlayer clone = targetCopy as OneForAll9thPlayer;
            clone.percentage = percentage;
            clone.currentFingers = currentFingers;
            
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncOFA9th); 
            packet.Write((byte)Player.whoAmI); 
            packet.Write((int)percentage); 
            packet.Write(currentFingers);
            
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            OneForAll9thPlayer clone = clientPlayer as OneForAll9thPlayer;
            
            
            if (percentage != clone.percentage)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncOFA9th);
                packet.Write((byte)Player.whoAmI);
                packet.Write((int)percentage);
                packet.Write(currentFingers);
                packet.Send(-1, Player.whoAmI); 
            }
        }
    }
}

