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

namespace MyHeroMod.content.Quirks.OFA8th
{
    public partial class OneForAll8thPlayer : ModPlayer, IQuirkResetter, IDashModifier
    {
       
       public int form = 0;
        public void FullReset()
        {
            form = 0;
        }
        public override void OnRespawn()
        {

            Player.GetModPlayer<TransformationPlayer>().ActiveForm = "None";    
        }

            public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            

            if (mainPlayer.HasActiveQuirk(QuirkType.OneForAll8th) && mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                Player.moveSpeed += 1.5f;
                Player.jumpSpeedBoost += 1.5f;
                Player.noFallDmg = true;
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


        