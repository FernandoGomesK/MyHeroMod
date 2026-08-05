using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;


namespace MyHeroMod.content.Quirks.BlackWhip
{
    public partial class BlackWhipPlayer : ModPlayer
    {

        public bool isOverlayActive = false;
        public bool isAutomaticWhipActive = false;

        public override void ResetEffects()
        {
            isOverlayActive = false;
            isAutomaticWhipActive = false;
        }

        public override void FrameEffects()
        {
            if (Player.HasBuff(ModContent.BuffType<OverlayBuff>()))
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, "OverlayHead", EquipType.Head);
                Player.handon = EquipLoader.GetEquipSlot(Mod, "OverlayArms", EquipType.HandsOn);
                Player.handoff = EquipLoader.GetEquipSlot(Mod, "OverlayArms", EquipType.HandsOff);
                Player.front = EquipLoader.GetEquipSlot(Mod, "OverlayBody", EquipType.Front);
                
            }

        }

        

        


        
        

        
        }
    }