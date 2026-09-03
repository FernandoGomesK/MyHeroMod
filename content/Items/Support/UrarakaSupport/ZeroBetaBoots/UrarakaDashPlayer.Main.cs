using Microsoft.Xna.Framework;
using KhacesCore.Content.System.Interfaces;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Support.UrarakaSupport
{
    public class UrarakaDashPlayer : ModPlayer, IDashModifier
    {
        public bool isUrarakaDashBootsOn;

        public override void ResetEffects()
        {
            isUrarakaDashBootsOn = false;
        }


        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Microsoft.Xna.Framework.Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            if (isUrarakaDashBootsOn)
            {
                isEnhanced = false;
                dustType = DustID.Cloud;
                explosionColor = Color.White;          
                speed = 15;
                speed *= 1.30f;

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/AirPressureSoundEffect") with { Volume = 0.8f }, Player.position);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/WooshSound") with { Volume = 0.8f }, Player.position);
            }
        }
    }
    
}