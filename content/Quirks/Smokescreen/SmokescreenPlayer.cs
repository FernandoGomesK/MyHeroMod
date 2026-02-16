using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;

namespace MyHeroMod.content.Quirks.Smokescreen
{
    public partial class SmokescreenPlayer : BasePlayer
    {
        public bool isSmokescreenActive = false;

        public override void ResetEffects()
        {
            
            isSmokescreenActive = false;
        }   

        
    }
}   