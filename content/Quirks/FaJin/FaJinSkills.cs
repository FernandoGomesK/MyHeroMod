using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using KhacesCore.Content.System.Interfaces;

namespace MyHeroMod.content.Quirks.FaJin
{
    public partial class FajinPlayer : ModPlayer, IDashModifier, IHeroPunchModifier
    {
        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            
            if (!transPlayer.HasActiveQuirk(QuirkType.FaJin) && !transPlayer.HasActiveQuirk(QuirkType.OneForAll9th)) return;

            if (FaJinStored) 
            {
                speed = 25f;
                isEnhanced = true;  
                Player.ClearBuff(ModContent.BuffType<FaJinBuff>());
                FaJinCharges = 0;  
            }
            else if (isFaJinActive)
            {
                ChargeFajin();
            }
        }

        public void ModifyPunch(ref float projSpeed, ref int baseDamage, ref bool isSuperPunch, ref int numberOfPunches)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.FaJin) && !transPlayer.HasActiveQuirk(QuirkType.OneForAll9th)) return;

            if (FaJinStored)
            {
                projSpeed = 30;
                baseDamage = 15;
                isSuperPunch = true;
                numberOfPunches = 1;
                Player.ClearBuff(ModContent.BuffType<FaJinBuff>());
                FaJinCharges = 0; 
            }
            else if (isFaJinActive)
            {
                ChargeFajin();
            }
        }
    }
}