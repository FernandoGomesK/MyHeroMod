using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.SlideAndGlide
{
    public partial class SlideAndGlidePlayer : ModPlayer, IQuirkResetter
    {
        public bool isSlideOn = false;

        public void FullReset()
        {
            isSlideOn = false;
        }

        public override void PreUpdate()
        {
            isSlideOn = false;
        }

        public override void PostUpdate()
        {
           
            }
        }
    }
