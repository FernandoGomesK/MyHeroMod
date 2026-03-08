using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;


namespace MyHeroMod.content.Quirks.Overclock
{
    
    public partial class OverclockPlayer : ModPlayer, IHeroDashModifier, IQuirkResetter
    {


        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor)
    {
        if (Player.HasBuff(ModContent.BuffType<OverclockBuff>()))
        {
            hideNormalDash = true;
            explosionColor = Color.Yellow;
        }
    }
    }
}
        
      

       

     