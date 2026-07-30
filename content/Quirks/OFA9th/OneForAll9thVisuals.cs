using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using KhacesCore.Content.System.Interfaces;


namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer: ModPlayer, IQuirkResetter, IDashModifier
    {

        private void HandleFullCowlingEffects()
        {
            


            Lighting.AddLight(Player.Center, Color.Green.ToVector3() * 1.5f);
            ElectricSoundTimer++;

            if (ElectricSoundTimer >= 900 + Main.rand.Next(-120, 120))
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingAura") with { Volume = 0.2f }, Player.position);
                ElectricSoundTimer = 0;
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, default, 0.5f);
            }
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
        if (Player.HasBuff(ModContent.BuffType<FaJinBuff>()))
        {
            drawInfo.colorArmorLegs = Color.Red;
        }
        if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
        {
        
        drawInfo.colorArmorBody = Color.LightBlue;
        drawInfo.colorArmorHead = Color.LightBlue;
        drawInfo.colorArmorLegs = Color.LightBlue;
        
        
        Player.armorEffectDrawShadow = true; 
        
        }
        // if (FaJinStored)
        //     {
                
        //     }
        }
    }
}