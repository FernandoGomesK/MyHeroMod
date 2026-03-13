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
using MyHeroMod.content.Quirks.Erasure.Projectiles;


namespace MyHeroMod.content.Quirks.Erasure;

    public partial class ErasurePlayer : ModPlayer, IQuirkResetter
    {
        
        public bool isErasureActive = false;
        
        public int eyeTimer = 0;

        public int maxEyeTimer = 180;

        public override void OnRespawn()
        {
            isErasureActive = false;
            eyeTimer = 0;
            
        }


        public override void PostUpdate()
{
    if (isErasureActive)
    {
        eyeTimer++;
        
        if (eyeTimer ==  160) CombatText.NewText(Player.getRect(), Color.Orange, "Blinking soon!");
    }
    if (eyeTimer == maxEyeTimer)
    {
        isErasureActive = false;
        Player.ClearBuff(ModContent.BuffType<ErasingBuff>());
        CombatText.NewText(Player.getRect(), Color.Red, "BLINK!");
        eyeTimer = 0;
    }
}


        public override void ResetEffects()
        {
            if (Player.HasBuff(ModContent.BuffType<ErasingBuff>()))
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<ErasureController>()] >= 1) 
            {
            return; 
            }
            else
            {
               Projectile.NewProjectile(
                    Player.GetSource_FromThis(),
                    Player.Center,
                    Vector2.Zero, 
                    ModContent.ProjectileType<ErasureController>(),
                    0, 
                    0f,
                    Player.whoAmI
                ); 
            }
            
        }
            // isErasureActive = false;
        }

        public void FullReset()
    {
        isErasureActive = false;
        // eyeTimer = 0;
    }

    }