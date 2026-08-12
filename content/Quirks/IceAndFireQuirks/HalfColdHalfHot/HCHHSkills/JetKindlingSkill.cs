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
using MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.JetKindlingProjs;



public class JetKindlingSkill: QuirkBaseSkill
{
    
    public override string Name => "Jet Kindling";

     public override string GetDisplayName(Player player)
        {
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
   
            if (hchhPlayer.IsFlashFireFistActive)
            {
                return "Coldflame's Pale Blade";
            }
            else if (hchhPlayer.IsFlashFireFistActive)
            {
                return "Jet Kindling";
            }
            return "Ice Thrower"; 
        }
            
        
   
    public override string Description => "Shoot a constant stream of fire";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "Fire";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

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

        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

        if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot)){
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;
            if (hchhPlayer.IsPhosphorActive)
            {
                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<JetPaleCharge>(),
                FinalDamage, 
                2f, 
                player.whoAmI
            );
            }
            else if (hchhPlayer.IsFlashFireFistActive)
            {
                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<JetKindlingCharge>(),
                FinalDamage, 
                2f, 
                player.whoAmI
            );
            }
            else
            {
                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<JetIceCharge>(),
                FinalDamage, 
                2f, 
                player.whoAmI
            );
            }

            
            
        }

        foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(15);
                }
            }
            
        }}