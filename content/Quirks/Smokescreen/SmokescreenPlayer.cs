using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;

using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.Smokescreen
{


    public partial class SmokescreenPlayer : ModPlayer, IHeroDodgeModifier
    {
        public bool isSmokescreenActive = false;

        public float dodgeChance = 0;

        public override void ResetEffects()
        {
            dodgeChance = 0;
            
            isSmokescreenActive = false;
        }   

        //  public override bool FreeDodge(Player.HurtInfo info)
        // {
        //     if (isSmokescreenActive && TryDodge(info))
        //     {
        //         return true; 
        //     }
            
        //     return false;
        // }

        
    }
}   