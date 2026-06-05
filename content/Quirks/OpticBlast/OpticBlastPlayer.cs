using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Quirks.OpticBlast.Projectiles; // Added to access the Controller
using Terraria.Graphics.CameraModifiers; 

namespace MyHeroMod.content.Quirks.OpticBlast
{
    public partial class OpticBlastPlayer : ModPlayer, IQuirkResetter
    {
        public enum Percentage 
        {
            Zero, TwentyFive, Fifty, SeventyFive, Full
        };

        

        public bool isRubyGlassesEquipped = false;
        public Percentage CurrentPercentage = Percentage.Zero;

        public int MaxOpticBlast = 100;
        public int MinOpticBlast = 0;
        public int CurrentOpticBlast = 100;
        public int regenTimer = 0; 

        public void FullReset()
        {
            MaxOpticBlast = 100;
            CurrentOpticBlast = MaxOpticBlast;
            CurrentPercentage = Percentage.Zero; 
            regenTimer = 0;
            isRubyGlassesEquipped = false;
        }

        public override void ResetEffects()
        {
            
            isRubyGlassesEquipped = false;
        }

        
        public override void PostUpdate()
        {
            
            if (CurrentOpticBlast < MaxOpticBlast)
            {
                regenTimer++;
                if (regenTimer >= 6)
                {
                    CurrentOpticBlast++;
                    regenTimer = 0;
                }
            }

            // 2. Overheat Logic
            if (CurrentOpticBlast <= 0)
            {
                CurrentOpticBlast = 0;
                CurrentPercentage = Percentage.Zero;
                Player.AddBuff(ModContent.BuffType<Heatstroke   >(), 300); 
            }

            // 3. Spawning the Laser Beam Controller Safely
            
            if (!isRubyGlassesEquipped && !Player.HasBuff(ModContent.BuffType<Heatstroke>()))
            {

                if (Player.ownedProjectileCounts[ModContent.ProjectileType<ContinuousOpticBlastController>()] < 1)
                {
                    Projectile.NewProjectile(
                        Player.GetSource_FromThis(),
                        Player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<ContinuousOpticBlastController>(),
                        20, // Base Damage of the uncontrolled laser
                        4f,  
                        Player.whoAmI
                    );

                    
                    // The sound only plays ONCE when the glasses come off
                    Terraria.Audio.SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("MyHeroMod/Assets/Sounds/SingleOpticBlast"), Player.position);
                }
            }
        }
    }
}