using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Explosion;
using MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot;


using MyHeroMod.content.Quirks.SlideAndGlide.Projectiles.ScrappyThrust;
using MyHeroMod.content.Quirks.SlideAndGlide.Projectiles.ShootyGo;

public class ScrappyThrustSkill : QuirkSkill
{
    public override string Name
    {
        get
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                return "Shooty go Blam";
            }
            else
            {
                return "Scrappy Thrust Style";
            }
            
        } 
        
    }
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.SlideAndGlide;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


                    public override void OnUse(Player player)
            {

                var transPlayer = player.GetModPlayer<TransformationPlayer>();

            

                 


        float damageMultiplier = 1.0f;
        int MaxDamage = 45;
         

            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                MaxDamage = 45;
                break;
            
                case QuirkStage.Adequation:
                MaxDamage = 45;
                break;
          
                case QuirkStage.Intermediate:
                MaxDamage = 60;
                break;
            
                case QuirkStage.Advanced:
                MaxDamage = 90;
                break;
          
                case QuirkStage.Final:
                MaxDamage = 180;
                break;
        
                default:
                MaxDamage =45;
                break;
                    
            }

            // if (player.HasBuff(ModContent.BuffType<ClusterBuff>())) {
            //     damageMultiplier = 2.5f; 
            // }

            var finalDamage = (int)(damageMultiplier * MaxDamage);

            var text = transPlayer.CurrentStage >= QuirkStage.Intermediate ? "Shooty Go Blam!" : "Scrappy Thrust Style!";
            

            CombatText.NewText(player.getRect(), Color.Blue, text);

            

            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    player.Center,
                    Velocity,
                    ModContent.ProjectileType<ShootyGoProj>(),
                    finalDamage, 
                    4f,  
                    player.whoAmI
                );
            }
            else
            {
                Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    player.Center,
                    Velocity,
                    ModContent.ProjectileType<ScrappyThrustProj>(),
                    finalDamage, 
                    2f,  
                    player.whoAmI
                );
            }

            
        }}
        