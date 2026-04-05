using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;
using Terraria.Audio;
using MyHeroMod.content.System;
using Terraria.GameContent.Bestiary;
using MyHeroMod.content.Quirks.OFA9th;
using Terraria.ModLoader.IO;

namespace MyHeroMod.content.Quirks.AllForOne
{
    public partial class AllForOnePlayer : ModPlayer, IQuirkResetter
    {
        public int quirkCounter = 0;   
        public int maxQuirks = 0;

        public List<QuirkType> InternalQuirks = new List<QuirkType>();

        public void FullReset() 
        {
            
            InternalQuirks.Clear(); // Clears the stolen quirks list
            quirkCounter = 0; 
        }

        
        public bool TryStealQuirk(QuirkType newQuirk)
        {
            if (InternalQuirks.Count < maxQuirks && !HasInternalQuirk(newQuirk))
            {
                InternalQuirks.Add(newQuirk);
                return true; 
            }
            return false; 
        }

        public bool HasInternalQuirk(QuirkType type)
        {
            return InternalQuirks.Contains(type);
        }

        public int CurrentQuirkCount => InternalQuirks.Count;

        // Setting max limits
        public override void ResetEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            maxQuirks = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 2,
                QuirkStage.Adequation => 3,
                QuirkStage.Intermediate => 5,
                QuirkStage.Advanced => 8,
                QuirkStage.Final => 10,
                _ => 15, 
            };
        }

        public override void SaveData(TagCompound tag)
        {
            List<int> savedQuirks = new List<int>();
            foreach (var quirk in InternalQuirks)
            {
                savedQuirks.Add((int)quirk); 
            }
            tag["AfoStolenQuirks"] = savedQuirks;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("AfoStolenQuirks"))
            {
                InternalQuirks.Clear();
                var savedQuirks = tag.GetList<int>("AfoStolenQuirks");
                foreach (var quirkId in savedQuirks)
                {
                    InternalQuirks.Add((QuirkType)quirkId); 
                }
            }
        }

        public override void CopyClientState(ModPlayer targetCopy)
        {
            AllForOnePlayer clone = targetCopy as AllForOnePlayer;
            clone.InternalQuirks = new List<QuirkType>(InternalQuirks); 
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncAllForOne); 
            packet.Write((byte)Player.whoAmI); 
            
            packet.Write((int)InternalQuirks.Count);
            foreach (var quirk in InternalQuirks)
            {
                packet.Write((int)quirk);
            }
            
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            AllForOnePlayer clone = clientPlayer as AllForOnePlayer;
            
            if (InternalQuirks.Count != clone.InternalQuirks.Count)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncAllForOne);
                packet.Write((byte)Player.whoAmI);
                
                packet.Write((int)InternalQuirks.Count);
                foreach (var quirk in InternalQuirks)
                {
                    packet.Write((int)quirk);
                }
                
                packet.Send(-1, Player.whoAmI); 
            }
        }
    }
}