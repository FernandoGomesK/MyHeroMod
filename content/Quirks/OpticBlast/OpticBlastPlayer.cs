using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.OpticBlast
{
    public partial class OpticBlastPlayer : ModPlayer, IQuirkResetter
    {
        
        
        
        public int Percentage = 0;
        public int MaxOpticBlast = 100;
        public int CurrentOpticBlast = 0;
        

        public void FullReset()
        {
            
            Percentage = 0;
            MaxOpticBlast = 100;
            CurrentOpticBlast = 0;
        }

        public override void ResetEffects()
        {
            
        }

        public override void PostUpdateMiscEffects()
        {
           
        
        }

        public override void PostUpdate()
        {
            if (CurrentOpticBlast > 0)
            {
                
            }
        }
            }
            
            }
        

