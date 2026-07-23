using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.GeneralSkills
{
    public abstract class BaseToggleSkill : QuirkBaseSkill
    {
        
        public abstract int BuffType { get; }

        
        public virtual string ToggleOnText => $"{Name}: ON";
        public virtual string ToggleOffText => $"{Name}: OFF";
        public virtual Color ToggleOnColor => Color.Orange;
        public virtual Color ToggleOffColor => Color.Gray;

        public override void OnUse(Player player)
        {
            if (player.HasBuff(BuffType))
            {
                
                player.ClearBuff(BuffType);
                CombatText.NewText(player.getRect(), ToggleOffColor, ToggleOffText);
                SoundEngine.PlaySound(SoundID.Item4, player.position);
                
                
                OnToggleOff(player); 
            }
            else
            {
                
                player.AddBuff(BuffType, 36000); 
                CombatText.NewText(player.getRect(), ToggleOnColor, ToggleOnText);
                SoundEngine.PlaySound(SoundID.Item4, player.position);
                
                
                OnToggleOn(player);
            }
        }

        
        public virtual void OnToggleOn(Player player) { }
        public virtual void OnToggleOff(Player player) { }
    }
}