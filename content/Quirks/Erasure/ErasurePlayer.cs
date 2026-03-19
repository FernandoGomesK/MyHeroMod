using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.OFA9th.Buffs;
using Terraria.Audio;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OFA9th;
using Humanizer;
using MyHeroMod.content.Quirks.Erasure.Projectiles;


namespace MyHeroMod.content.Quirks.Erasure;

    public partial class ErasurePlayer : ModPlayer, IQuirkResetter
    {
        
        public bool isErasureActive = false;
        public bool isYellowGogglesOn = false;
        
        public int eyeTimer = 0;

        public int maxEyeTimer = 180;

        public override void OnRespawn()
        {
            isErasureActive = false;
            eyeTimer = 0;
            
        }


        public override void PostUpdate()
{
    if (isYellowGogglesOn == true)
        {
            maxEyeTimer = 220;
            
        }
    if (isErasureActive)
    {
        eyeTimer++;
        
        if (eyeTimer ==  160) CombatText.NewText(Player.getRect(), Color.Orange, "Blinking soon!");
    }
    if (eyeTimer == maxEyeTimer)
    {
        isErasureActive = false;
        Player.ClearBuff(ModContent.BuffType<ErasingBuff>());
        CombatText.NewText(Player.getRect(), Color.Red, "BLINK!");
        eyeTimer = 0;
    }
}


        public override void ResetEffects()
        {
            if (Player.HasBuff(ModContent.BuffType<ErasingBuff>()))
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<ErasureController>()] >= 1) 
            {
            return; 
            }
            else
            {
               Projectile.NewProjectile(
                    Player.GetSource_FromThis(),
                    Player.Center,
                    Vector2.Zero, 
                    ModContent.ProjectileType<ErasureController>(),
                    0, 
                    0f,
                    Player.whoAmI
                ); 
            }
            
        }
            // isErasureActive = false;
        }

        public void FullReset()
    {
        isErasureActive = false;
        // eyeTimer = 0;
    }

     public override void CopyClientState(ModPlayer targetCopy)
        {
            ErasurePlayer clone = targetCopy as ErasurePlayer;
            clone.isErasureActive = isErasureActive;
                clone.isYellowGogglesOn = isYellowGogglesOn;
                clone.eyeTimer = eyeTimer;
            
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncErasure); 
            packet.Write((byte)Player.whoAmI); 
            
            packet.Write(isErasureActive);
            packet.Write(isYellowGogglesOn);
            packet.Write(eyeTimer);
         
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            ErasurePlayer clone = clientPlayer as ErasurePlayer;
            if (eyeTimer != clone.eyeTimer || isYellowGogglesOn != clone.isYellowGogglesOn || isErasureActive != clone.isErasureActive)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncGearshift);
                packet.Write((byte)Player.whoAmI);
                packet.Write(isErasureActive);
                packet.Write(isYellowGogglesOn);
                packet.Write(eyeTimer);
                packet.Send(-1, Player.whoAmI); 
            }
        }

    

    }