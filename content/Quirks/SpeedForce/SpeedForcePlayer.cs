using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;

using Terraria.DataStructures;

namespace MyHeroMod.content.Quirks.SpeedForce
{
    public partial class SpeedForcePlayer : ModPlayer
    {
        public bool isSpeedForceBuffActive = false;
        

        public override void ResetEffects()
        {
            isSpeedForceBuffActive = false;
            
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
        
        if (Player.HasBuff(ModContent.BuffType<SpeedForceBuff>()))
        {
        
        drawInfo.colorArmorBody = Color.Yellow;
        drawInfo.colorArmorHead = Color.Yellow;
        drawInfo.colorArmorLegs = Color.Yellow;
        
        
        Player.armorEffectDrawShadow = true; 
        Lighting.AddLight(Player.Center, Color.Yellow.ToVector3() * 1.0f);

        

        

        


        
        

        
        }
        }}}