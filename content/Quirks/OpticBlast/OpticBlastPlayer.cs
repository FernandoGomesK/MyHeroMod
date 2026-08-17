using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Quirks.OpticBlast.Projectiles; 
using Terraria.Graphics.CameraModifiers;
using System;

namespace MyHeroMod.content.Quirks.OpticBlast
{
    public partial class OpticBlastPlayer : ModPlayer, IQuirkResetter
    {
        public enum Percentage 
        {
            Zero, TwentyFive, Fifty, SeventyFive, Full
        };

        public bool isRubyGlassesEquipped = false;
        public bool isGoldenVisorEquipped = false;
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
            isGoldenVisorEquipped = false;
        }

        public override void ResetEffects()
        {
            isRubyGlassesEquipped = false;
            isGoldenVisorEquipped = false; 
        }

        public bool isBlockingEyes()
        {
            if (isGoldenVisorEquipped || isRubyGlassesEquipped)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.OpticBlast))  
                return;

            if (CurrentOpticBlast < MaxOpticBlast)
            {
                regenTimer++;
                if (regenTimer >= 6)
                {
                    if (isGoldenVisorEquipped)
                    {
                        CurrentOpticBlast += 2; 
                        regenTimer = 0;
                    }
                    else
                    {
                        CurrentOpticBlast++;
                        regenTimer = 0;
                    }
                    
                    
                    if (CurrentOpticBlast > MaxOpticBlast)
                        CurrentOpticBlast = MaxOpticBlast;
                }
            }

            if (CurrentOpticBlast <= 0)
            {
                CurrentOpticBlast = 0;
                CurrentPercentage = Percentage.Zero;
                Player.AddBuff(ModContent.BuffType<Heatstroke>(), 300); 
            }

            
            if (!isBlockingEyes() && !Player.HasBuff(ModContent.BuffType<Heatstroke>()) && !Player.HasBuff(BuffID.Darkness))
            {
                if (Main.GameUpdateCount % 6 == 0)
                {
                    int strainIncrease = (int)(transPlayer.maxStrain * 0.01f); 
                    transPlayer.currentStrain += Math.Max(1, strainIncrease);
                }

                Player.moveSpeed *= 0.2f;
                Player.statDefense -= 15;

                Vector2 aimDirection = Main.MouseWorld - Player.Center;
                aimDirection = aimDirection.SafeNormalize(Vector2.Zero);

                float recoilStrength = transPlayer.CurrentStage switch
                {
                    QuirkStage.Initial => 0.8f,
                    QuirkStage.Adequation => 0.5f,
                    QuirkStage.Intermediate => 0.3f,
                    QuirkStage.Advanced => 0.15f,
                    QuirkStage.Final => 0.05f,
                    _ => 0.8f
                };

                
                Player.velocity.X -= aimDirection.X * recoilStrength;
   

                if (transPlayer.currentStrain >= transPlayer.maxStrain)
                {
                    Player.AddBuff(ModContent.BuffType<Heatstroke>(), 500);
                    Player.AddBuff(BuffID.Obstructed, 300);  
                    Player.AddBuff(BuffID.Weak, 300); 
                    Player.AddBuff(BuffID.BrokenArmor, 300); 
               
                    return; 
                }

                if (Player.ownedProjectileCounts[ModContent.ProjectileType<ContinuousOpticBlastController>()] < 1)
                {
                    PunchCameraModifier shake = new PunchCameraModifier(
                        Player.Center, 
                        Main.rand.NextVector2CircularEdge(1f, 1f), 
                        10f, 
                        15f, 
                        20, 
                        1000f, 
                        "OpticBlastShake"
                    );
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

                    Terraria.Audio.SoundEngine.PlaySound(
                        new Terraria.Audio.SoundStyle("MyHeroMod/Assets/Sounds/SingleOpticBlast"), 
                        Player.position
                    );
                }
            }
        }
    }
}