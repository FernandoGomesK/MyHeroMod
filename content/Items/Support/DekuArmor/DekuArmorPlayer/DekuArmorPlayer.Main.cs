using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MyHeroMod.content.System;
using KhacesCore.Content.System.Interfaces;

namespace MyHeroMod.content.Items.Support.DekuArmor
{
    public class DekuArmorPlayer : ModPlayer, IDashModifier
    {
        public bool isArmorBootsOn = false;
        public bool isChestArmorOn = false;
        public bool isArmorGauntletsOn = false;

        public override void ResetEffects()
        {
            isArmorBootsOn = false;
            isChestArmorOn = false;
            isArmorGauntletsOn = false;
        }

        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, 
        ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            if (isArmorBootsOn)
            {
                isEnhanced = true;
                dustType = DustID.Firework_Blue;
                explosionColor = Color.White;          
                speed = 65;
                speed *= 1.30f;

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, Player.position);
            }
        }

        public override void PostUpdate()
        {
            if (isArmorBootsOn)
            {
                UpdateFlyingDust();
            }
        }

        public override void PostUpdateEquips()
        {
            if (isArmorBootsOn)
            {
                Player.wingTimeMax = 50;
                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29; 
                    Player.wings = -1;
                }
                Player.noFallDmg = true;
            }
            if (isArmorBootsOn && isChestArmorOn && isArmorGauntletsOn)
            {
                
            
                Player.moveSpeed += 4.0f; 
                Player.statDefense += 4;  
                Player.jumpSpeedBoost += 6f;
                Player.noFallDmg = true;
            }
            
        }

        protected void UpdateFlyingDust()
        {     
            bool isFlying = (Player.velocity.Y != 0) && (Player.wingTime > 0 || Player.rocketDelay > 0) && !Player.mount.Active;

            float corVelocidade = 0.5f; 
            Color corArcoIris = Main.hslToRgb((Main.GlobalTimeWrappedHourly * corVelocidade) % 1f, 1f, 0.6f);
                
            Color corTranslucida = corArcoIris * 0.5f; 
            
            if (isFlying)
            {
                if (Main.rand.NextBool(2)) 
                {
                    int dustFire = Dust.NewDust(Player.position + new Vector2(-5, Player.height - 10), Player.width / 2, 10, DustID.FireworksRGB, 0, 2f, 100, corTranslucida, 1.5f);
                    Main.dust[dustFire].noGravity = true;
                    Main.dust[dustFire].velocity *= 0.5f; 
                }
                
                if (Main.rand.NextBool(2))
                {
                    int dustIce = Dust.NewDust(Player.position + new Vector2(Player.width / 2, Player.height - 10), Player.width / 2, 10, DustID.FireworksRGB, 0, 2f, 100, corTranslucida, 1.5f);
                    Main.dust[dustIce].noGravity = true;
                    Main.dust[dustIce].velocity *= 0.5f;
                }
            }
        }
    }
}