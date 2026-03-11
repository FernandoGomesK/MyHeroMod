using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;


namespace MyHeroMod.content.Quirks.Overclock
{
    public partial class OverclockPlayer: ModPlayer, IQuirkResetter, IHeroDashModifier, IHeroPunchModifier
    {
        private void HandleFullCowlingEffects()
        {
            


            
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
        
        if (Player.HasBuff(ModContent.BuffType<OverclockBuff>()))
        {
        // This adds a blue tint/glow to the character sprite itself
        drawInfo.colorArmorBody = Color.Yellow;
        drawInfo.colorArmorHead = Color.Yellow;
        drawInfo.colorArmorLegs = Color.Yellow;
        
        
        Player.armorEffectDrawShadow = true; 
        Lighting.AddLight(Player.Center, Color.Yellow.ToVector3() * 1.0f);
            ElectricSoundTimer++;

            if (ElectricSoundTimer >= 900 + Main.rand.Next(-120, 120))
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingAura") with { Volume = 0.2f }, Player.position);
                ElectricSoundTimer = 0;
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.YellowTorch, 0, 0, 100, default, 0.5f);
            }
        }
        // if (FaJinStored)
        //     {
                
        //     }
        }
    }
}