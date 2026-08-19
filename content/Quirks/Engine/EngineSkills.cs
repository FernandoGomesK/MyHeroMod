using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Engine
{
    public partial class EnginePlayer : ModPlayer, IQuirkResetter, IDashModifier
    {

        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, 
        ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
           
            if (!transPlayer.HasActiveQuirk(QuirkType.Engine)) return; 

            if (isEngineOn || Player.HasBuff(ModContent.BuffType<EngineBuff>()))
            {
            
                float dashSpeed = transPlayer.CurrentStage switch 
            {
                QuirkStage.Initial => 30f, QuirkStage.Adequation => 45f,
                QuirkStage.Intermediate => 50f, QuirkStage.Advanced => 60f,
                QuirkStage.Final => 80f, _ => 80f
            };

            

            dustType = transPlayer.CurrentStage switch
            {
                QuirkStage.Final => DustID.Clentaminator_Cyan,
                QuirkStage.Advanced => DustID.Clentaminator_Cyan,
                QuirkStage.Intermediate => DustID.BlueTorch,
                QuirkStage.Adequation => DustID.Torch,
                _ => DustID.Smoke
            };

            
            explosionColor = transPlayer.CurrentStage switch
            {
                QuirkStage.Final => Color.Cyan,
                QuirkStage.Advanced => Color.Cyan,
                QuirkStage.Intermediate => Color.LightBlue,
                QuirkStage.Adequation => Color.Orange,
                _ => Color.White
            };

            if (isBoosting)
                {
                    dashSpeed *= 1.5f; 
                    dustType = DustID.Clentaminator_Cyan;
                    explosionColor = Color.Cyan;
                }

                isEnhanced = true;
                speed = dashSpeed;

            
            
            
            momentumTimer += 50;

           
        }
            }
        

            
        
    }
}