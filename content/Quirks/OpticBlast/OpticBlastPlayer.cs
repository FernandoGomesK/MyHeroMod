using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

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

        public int CurrentUncontrolledBlast = 0;

        public int MaxUncontrolledBlast = 500;

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
            
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!mainPlayer.HasActiveQuirk(QuirkType.OpticBlast))  
                return;
            else{

            
            

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
                CurrentUncontrolledBlast++;

                // Se superaquecer, aplica o debuff e zera a barra para ele começar a "esfriar"
                if (CurrentUncontrolledBlast >= MaxUncontrolledBlast)
                {
                    Player.AddBuff(ModContent.BuffType<Heatstroke>(), 500);
                    CurrentUncontrolledBlast = 0; 
                }

                if (Player.ownedProjectileCounts[ModContent.ProjectileType<ContinuousOpticBlastController>()] < 1)
                {
                    
                    PunchCameraModifier shake = new PunchCameraModifier(Player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "OpticBlastShake");
                    Main.instance.CameraModifiers.Add(shake);

                    Projectile.NewProjectile(
                        Player.GetSource_FromThis(),
                        Player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<ContinuousOpticBlastController>(),
                        20, 
                        4f,  
                        Player.whoAmI
                    );

                    Terraria.Audio.SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("MyHeroMod/Assets/Sounds/SingleOpticBlast"), Player.position);
                }
            }
            else 
            {
                
                if (CurrentUncontrolledBlast > 0)
                {
                    CurrentUncontrolledBlast--;
                }
            }
            }
        }
        }
    }
