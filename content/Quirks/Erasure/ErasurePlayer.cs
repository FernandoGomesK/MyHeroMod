using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.OFA9th.Buffs;
using Terraria.Audio;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OFA9th;
using Humanizer;


namespace MyHeroMod.content.Quirks.Erasure;

    public partial class ErasurePlayer : ModPlayer, IQuirkResetter
    {
        
        public bool isErasureActive = false;
        
        // public int eyeTimer = 0;

        // public int maxEyeTimer = 120;

        public override void OnRespawn()
        {
            // isErasureActive = false;
            // eyeTimer = 0;
            
        }

        public override void PostUpdate()
{
    // if (isErasureActive)
    // {
    //     eyeTimer--;
        
    //     if (eyeTimer == 30) CombatText.NewText(Player.getRect(), Color.Orange, "Blinking soon!");
    // }
}


        public override void ResetEffects()
        {
            
            // isErasureActive = false;
        }

        public void FullReset()
    {
        isErasureActive = false;
        // eyeTimer = 0;
    }

    }