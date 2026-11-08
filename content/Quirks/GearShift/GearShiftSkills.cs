using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;

using MyHeroMod.content.System;
using Microsoft.Xna.Framework.Graphics;
using KhacesCore.Content.System.Interfaces;
using KhacesCore.Content.System.BaseProjectiles;
using MyHeroMod.content.System.Interfaces;


namespace MyHeroMod.content.Quirks.Gearshift
{
    
    public partial class GearshiftPlayer : ModPlayer, IDashModifier, IQuirkResetter, IPunchModifier
    {


        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash ,
        ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.Gearshift))
            return;
            
            if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>())) 
            {
                hideNormalDash = true;
                explosionColor = Color.Cyan; 
                dustType = DustID.BlueTorch;
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, Player.position);
            }

            
        }

        public void ModifyPunch(ref float projSpeed, ref int baseDamage, ref int numberOfPunches, ref int mainProjType, ref int extraProjType)
        {
            if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
            
    {
    mainProjType = ModContent.ProjectileType<PunchAttackProj>();
       projSpeed = 25;
            baseDamage = 20;
            
            numberOfPunches = 5;
    }

        }
    }
}
