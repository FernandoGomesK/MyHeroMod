using MyHeroMod.content.System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.OFA8th
{
    public partial class OneForAll8thPlayer : ModPlayer, IHeroDashModifier
    {
        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor, ref int dustType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
           
            if (!transPlayer.HasActiveQuirk(QuirkType.OneForAll8th)) return; 
            
            float formModifier = 0f;
            if (form == 2) formModifier = 10f; 
            else if (form == 1) formModifier = 5f;

            float dashSpeed = transPlayer.CurrentStage switch 
            {
                QuirkStage.Initial => 15f, QuirkStage.Adequation => 20f,
                QuirkStage.Intermediate => 25f, QuirkStage.Advanced => 30f,
                QuirkStage.Final => 35f, _ => 40f
            };

            speed = dashSpeed + formModifier;
        }
    }
}