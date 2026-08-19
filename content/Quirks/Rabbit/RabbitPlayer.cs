using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Rabbit
{
    public class RabbitPlayer : ModPlayer, IDashModifier
    {
        public bool isIronSolesOn = false;

        public override void ResetEffects()
        {
            isIronSolesOn = false;
        }


        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            

            if (mainPlayer.HasActiveQuirk(QuirkType.Rabbit))
            {
                Player.moveSpeed += 1.5f;
                Player.jumpSpeedBoost += 1.5f;
                Player.noFallDmg = true;
            }

        }

        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, 
        ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
           
            if (!transPlayer.HasActiveQuirk(QuirkType.Rabbit)) return; 
        

            float dashSpeed = transPlayer.CurrentStage switch 
            {
                QuirkStage.Initial => 20f, QuirkStage.Adequation => 25f,
                QuirkStage.Intermediate => 35f, QuirkStage.Advanced => 40f,
                QuirkStage.Final => 60f, _ => 80f
            };
            
                isEnhanced = true;
                dustType = DustID.Cloud;
                explosionColor = Color.White; 
            
            
            speed = dashSpeed ;

            // SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, Player.position);
        }


        
    }
}