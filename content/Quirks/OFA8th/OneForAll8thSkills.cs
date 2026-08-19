using MyHeroMod.content.System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using KhacesCore.Content.System.Interfaces;
using Terraria.Audio;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.OFA8th
{
    public partial class OneForAll8thPlayer : ModPlayer, IDashModifier
    {
        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, 
        ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
           
            if (!transPlayer.HasActiveQuirk(QuirkType.OneForAll8th)) return; 
            
            float formModifier = 0f;
            if (form == 2) formModifier = 10f; 
            else if (form == 1) formModifier = 5f;

            float dashSpeed = transPlayer.CurrentStage switch 
            {
                QuirkStage.Initial => 20f, QuirkStage.Adequation => 25f,
                QuirkStage.Intermediate => 35f, QuirkStage.Advanced => 40f,
                QuirkStage.Final => 60f, _ => 80f
            };
            if (transPlayer.CurrentStage>= QuirkStage.Adequation)
            {
                isEnhanced = true;
                dustType = DustID.Cloud;
                explosionColor = Color.White; 
            }
            

            speed = dashSpeed + formModifier;

            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, Player.position);
        }
    }
}