using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;
using KhacesCore.Content.System.Interfaces;


namespace MyHeroMod.content.Quirks.Overclock
{
    
    public partial class OverclockPlayer : ModPlayer, IDashModifier, IQuirkResetter, IHeroPunchModifier
    {


        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor, ref int dustType)
{
    var transPlayer = Player.GetModPlayer<TransformationPlayer>();

    if (!transPlayer.HasActiveQuirk(QuirkType.Overclock))
        return;

        
    if (Player.HasBuff(ModContent.BuffType<OverclockBuff>()))
    {
        hideNormalDash = true;
        explosionColor = Color.Yellow; 
        dustType = DustID.YellowTorch; 
    }
    }

        public void ModifyPunch(ref float projSpeed, ref int baseDamage, ref bool isSuperPunch, ref int numberOfPunches)
        {

            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (Player.HasBuff(ModContent.BuffType<OverclockBuff>()))
    {
       projSpeed = 30;
            baseDamage = 20;
            isSuperPunch = false;

            if (transPlayer.CurrentStage >= QuirkStage.Intermediate){
                    numberOfPunches = 8;
                }
                else
                {
                    numberOfPunches = 5;
                }
            
    }

        }
}
}
      

       

     