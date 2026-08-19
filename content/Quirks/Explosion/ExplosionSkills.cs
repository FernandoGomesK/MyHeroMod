using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.System;
using Terraria.Audio;
using System.Collections.Generic;
using MyHeroMod.content.Dusts;
using MyHeroMod.content.Buffs;
using KhacesCore.Content.System.Interfaces;
using MyHeroMod.content.Projectiles;
using System;

namespace MyHeroMod.content.Quirks.Explosion
{
    public partial class ExplosionPlayer : ModPlayer, IFlightModifier, IDashModifier
    {
        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.Explosion))
                return;

            int halfSweat = MaxSweat / 2;
            float sweatBonus = 0f;

        
            if (CurrentSweat >= MaxSweat)
            {
                sweatBonus = 20f;
            }
            else if (CurrentSweat >= halfSweat)
            {
                sweatBonus = 10f;
            }

            float dashSpeed = transPlayer.CurrentStage switch 
            {
                QuirkStage.Initial => 20f, 
                QuirkStage.Adequation => 25f,
                QuirkStage.Intermediate => 35f, 
                QuirkStage.Advanced => 40f,
                QuirkStage.Final => 60f, 
                _ => 80f
            };

            speed = dashSpeed + sweatBonus;
                
            if (Player.HasBuff(ModContent.BuffType<ClusterBuff>()))
            {
                hideNormalDash = true;
                explosionColor = Color.Orange; 
                dustType = ModContent.DustType<ClusterDust>(); 
            }
            else
            {
                hideNormalDash = false;
                isEnhanced = true;
                explosionColor = Color.Orange; 
                dustType = DustID.FireworkFountain_Red; 
            }

            onomatopoeiaType = ModContent.ProjectileType<BoomOnomatopoeia>(); 
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Explosion2Sound"), Player.Center);
        }

        public void ModifyFlight(ref float speed)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            if (!transPlayer.HasActiveQuirk(QuirkType.Explosion)) return; 

           
            int halfSweat = MaxSweat / 2;
            float sweatBonus = 0f;

        
            if (CurrentSweat >= MaxSweat)
            {
                sweatBonus = 20f;
            }
            else if (CurrentSweat >= halfSweat)
            {
                sweatBonus = 10f;
            }

            float dashSpeed = transPlayer.CurrentStage switch 
            {
                QuirkStage.Initial => 8f, 
                QuirkStage.Adequation => 12f,
                QuirkStage.Intermediate => 15f, 
                QuirkStage.Advanced => 18f,
                QuirkStage.Final => 20f, 
                _ => 8f
            };

            
            speed = dashSpeed + sweatBonus;
        }
        
        public bool CanCruiseFlight()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            return transPlayer.HasActiveQuirk(QuirkType.Explosion);
        }
    }
}