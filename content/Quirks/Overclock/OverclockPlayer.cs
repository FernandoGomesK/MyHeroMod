using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;
using Mono.Cecil.Cil;
using MyHeroMod.content.System;


namespace MyHeroMod.content.Quirks.Overclock
{
    // PARTE 1: DADOS E LÓGICA
    public partial class OverclockPlayer : ModPlayer, IHeroDashModifier, IQuirkResetter
    {
        // Variáveis de Estado
        
        public int form = 0;
        public bool isOverclockBuffActive = false;
        
        
        

        private int ElectricSoundTimer = 0;
        

        public void FullReset()
        {
            isOverclockBuffActive = false;
            
        }
         public override void OnRespawn()
        {
            
        }

        public override void ResetEffects()
        {
            isOverclockBuffActive = false; 
        }

        public override void PreUpdate()
        { 
        
        }
        

        
    }
}