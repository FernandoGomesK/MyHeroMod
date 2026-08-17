using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;

using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles;



public class BlueFlamethrowerSkill: QuirkBaseSkill
{
    
    public override string Name => "Blue Flamethrower";

    public override string GetDisplayName(Player player)
        {
            
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                return "Flashfire Fist: Jet Burn";
            }
            else if (transPlayer.CurrentStage == QuirkStage.Adequation)
            {
                return "Blue Flamethrower";
            }
            else
            {
                return "Flamethrower";
            }
             
        }
   
    public override string Description => "Shoot a constant stream of fire";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Blueflame/BlueJetBurnIcon";
    public override string Category => "Fire";

    public override int BaseCooldown => 900;

    public override QuirkType RequiredQuirk => QuirkType.Blueflame;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var bluePlayer = player.GetModPlayer<BlueflamePlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        int BaseDamage = 0;
        
            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                BaseDamage = 25;
                break;
            
                case QuirkStage.Adequation:
                BaseDamage = 45;
                break;
          
                case QuirkStage.Intermediate:
                BaseDamage =  65;
                break;
            
                case QuirkStage.Advanced:
                BaseDamage = 80;
                break;
          
                case QuirkStage.Final:
                BaseDamage = 120;
                break;
        
                default:
                BaseDamage =25;
                break;
                    
            }
        
            float modifiedDamage = 1f;

            
            if (bluePlayer.IsFlashFireFistActive)
            {
                modifiedDamage += 2.0f; 
            }
        
            if (bluePlayer.isSurgeArmGauntletsOn)
            {
                modifiedDamage += 1.5f; 
            }

            int finalDamage = (int)(BaseDamage * modifiedDamage);



        if (transPlayer.HasActiveQuirk(QuirkType.Blueflame)){
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<ChargeBlueJetBurnProj>(),
                finalDamage, 
                2f, 
                player.whoAmI
            );
            
        }

        
            
        }}