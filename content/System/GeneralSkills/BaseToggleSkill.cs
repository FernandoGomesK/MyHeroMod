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
        public virtual SoundStyle? ToggleSound => SoundID.Item4;
        public virtual float ToggleSoundVolume => 1f;

        public virtual int OnomatopoeiaProjType => 0; 
        public virtual float OnomatipoeiaSpawnOffset => -30f;

        public override void OnUse(Player player)
        {
            if (player.HasBuff(BuffType))
            {
                player.ClearBuff(BuffType);
                CombatText.NewText(player.getRect(), ToggleOffColor, ToggleOffText);
                
                if (ToggleSound.HasValue) 
                {
                    SoundEngine.PlaySound(ToggleSound.Value with { Volume = ToggleSoundVolume }, player.position);
                }
                
                OnToggleOff(player); 
            }
            else
            {
                player.AddBuff(BuffType, 36000); 
                CombatText.NewText(player.getRect(), ToggleOnColor, ToggleOnText);
                
                if (ToggleSound.HasValue) 
                {
                    SoundEngine.PlaySound(ToggleSound.Value with { Volume = ToggleSoundVolume }, player.position);
                }

            
                if (OnomatopoeiaProjType > 0)
                {
                    Vector2 textPosition = player.Center + new Vector2(0, OnomatipoeiaSpawnOffset);
                    
                    
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(), 
                        textPosition,
                        Vector2.Zero, 
                        OnomatopoeiaProjType, 
                        0,  
                        0f, 
                        player.whoAmI
                    );
                }
                
                OnToggleOn(player);
            }
        }
        
        public virtual void OnToggleOn(Player player) { }
        public virtual void OnToggleOff(Player player) { }
    }
}