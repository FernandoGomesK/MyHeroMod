using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;

namespace MyHeroMod.content.Quirks.Float
{
    public partial class FloatPlayer : BasePlayer
    {
        public bool isFloatActive = false;

       

        public override void OnRespawn() => ResetAll();

        

        public override void ResetEffects()
        {
            
            isFloatActive = false;
        }

        public override void PostUpdate()
        {
            
            
        }

        
        

        
        }
    }