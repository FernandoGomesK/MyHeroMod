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
using MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueFireball;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles;



public class BlueProminenceSkill: QuirkBaseSkill
{
    
    public override string Name => "Blue Prominence Burn ";

    public override string GetDisplayName(Player player) => "Prominence Burn ";
        
   
    public override string Description => "Shoot a fireball";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "Fire";

    public override int BaseCooldown => 1200;

    public override QuirkType RequiredQuirk => QuirkType.Blueflame;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var bluePlayer = player.GetModPlayer<BlueflamePlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        int BaseDamage = 0;
        
            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                BaseDamage = 200;
                break;
            
                case QuirkStage.Adequation:
                BaseDamage = 300;
                break;
          
                case QuirkStage.Intermediate:
                BaseDamage =  450;
                break;
            
                case QuirkStage.Advanced:
                BaseDamage = 650;
                break;
          
                case QuirkStage.Final:
                BaseDamage = 900;
                break;
        
                default:
                BaseDamage =20;
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
                ModContent.ProjectileType<ChargeBlueProminenceBurnProj>(),
                finalDamage, 
                2f, 
                player.whoAmI
            );
           
        }

        
            
        }}