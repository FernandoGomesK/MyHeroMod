using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.Float
{
    public partial class FloatPlayer : ModPlayer, IQuirkResetter
    {
        public bool isFloatActive = false;

    
        public void FullReset()
        {
            isFloatActive = false;
        }
        public override void ResetEffects()
        {
            
            isFloatActive = false;
        }

        public override void PostUpdate()
        {
            
            
        }

        
        

        
        }
    }