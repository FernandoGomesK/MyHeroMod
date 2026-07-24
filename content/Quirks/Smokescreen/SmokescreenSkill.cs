using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;

using MyHeroMod.content.System;


namespace MyHeroMod.content.Quirks.Smokescreen
{
    public partial class SmokescreenPlayer : ModPlayer, IHeroDodgeModifier
    {
        
            public bool TryDodge(Player.HurtInfo info) 
        {
           
            
            if (Main.rand.NextFloat() < dodgeChance)
            {
               {
            Player.SetImmuneTimeForAllTypes(80); 
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/DangerSenseSound") with { Volume = 2.0f }, Player.position);
             
            return true; 
        }
        }
            return false;
        }
            
        }

    
        }
    
