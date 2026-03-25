using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;

using MyHeroMod.content.Quirks.Blueflames;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.BlueVanishingFist;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.BlueFireBall;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.IgnitedArrow;
using MyHeroMod.content.System.Interfaces;



public class IgnitedArrowSkill: QuirkSkill
{
    
    public override string Name 
    {
        get 
        {
            
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.SelectedQuirk == QuirkType.BlueFlames)
            {
                if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
                {
                    return "Flashfire Fist: Vanishing Fist";
                }
                
                else
                {
                    return "Fireball";
                }
            }

            return "Ignited Arrow"; 
        }
    }

   
    public override string Description => "Shoot a Projectile of Fire";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public override bool CheckUnlock(TransformationPlayer player)
    {
        var bluePlayer = player.Player.GetModPlayer<BlueFlamesPlayer>();
        var afoPlayer = player.Player.GetModPlayer<AllForOnePlayer>();
        

        if (player.SelectedQuirk == QuirkType.HellFlames) 
            return player.CurrentStage >= QuirkStage.Initial;

        if (player.SelectedQuirk == QuirkType.BlueFlames) 
            return player.CurrentStage >= QuirkStage.Initial;

        if (player.SelectedQuirk == QuirkType.AllForOne && (afoPlayer.HasInternalQuirk(QuirkType.BlueFlames) || afoPlayer.HasInternalQuirk(QuirkType.HellFlames)))
        {
            return true;
        }

        return false;
    }

    

public override void OnUse(Player player)
    {
            var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            int BaseDamage = 0;

            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                BaseDamage = 20;
                break;
            
                case QuirkStage.Adequation:
                BaseDamage = 40;
                break;
          
                case QuirkStage.Intermediate:
                BaseDamage =  45;
                break;
            
                case QuirkStage.Advanced:
                BaseDamage = 60;
                break;
          
                case QuirkStage.Final:
                BaseDamage = 80;
                break;
        
                default:
                BaseDamage =20;
                break;
                    
            }
        
        float ModifiedDamage = 1;

        if (hellPlayer.IsFlashFireFistActive){
         
        ModifiedDamage += 1.5f;        
        }
        int FinalDamage = (int)(BaseDamage * ModifiedDamage);



        if (transPlayer.HasActiveQuirk(QuirkType.HellFlames)){
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<IgnitedArrowProj>(),
                FinalDamage, 
                2f, 
                player.whoAmI
            );
            // hellPlayer.CurrentHeat += 15;
        }
        else if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames)){
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<BlueVanishingFistProj>(),
                FinalDamage, 
                2f, 
                player.whoAmI
            );
            }
            else{
                
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<BlueFireBallProj>(),
                FinalDamage, 
                2f, 
                player.whoAmI
            );
            }

            
            // hellPlayer.CurrentHeat += 15;
        }

        foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(15);
                }
            }
            
        }}