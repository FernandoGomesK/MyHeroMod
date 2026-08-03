using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using KhacesCore.Content.System.Interfaces;
using Terraria.Audio;
using KhacesCore.Content.System.BaseProjectiles;


namespace MyHeroMod.content.Quirks.FaJin
{
    public partial class FajinPlayer : ModPlayer, IDashModifier, IPunchModifier
    {
        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, 
        ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
    {
        var transPlayer = Player.GetModPlayer<TransformationPlayer>();
        
        if (!transPlayer.HasActiveQuirk(QuirkType.FaJin) && !transPlayer.HasActiveQuirk(QuirkType.OneForAll9th)) return;

        if (FaJinStored) 
        {
            speed += 10f; 
            isEnhanced = true;  
            Player.ClearBuff(ModContent.BuffType<FaJinBuff>());
            FaJinCharges = 0;  
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FaJinSound"), Player.Center);
        }
        else if (isFaJinActive)
        {
            ChargeFajin();
        }

        
        
    }

        public void ModifyPunch(ref float projSpeed, ref int baseDamage, ref int numberOfPunches, ref int mainProjType, ref int extraProjType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.FaJin) && !transPlayer.HasActiveQuirk(QuirkType.OneForAll9th)) return;

            if (FaJinStored)
            {
                projSpeed = 30;
                baseDamage = 15;
                mainProjType = ModContent.ProjectileType<PunchAttackProj>();
                extraProjType = ModContent.ProjectileType<SuperPunchProj>();
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