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



public class HellProminenceBurnSkill: QuirkBaseSkill
{
    
    public override string Name => "Hell Prominence Burn ";

    public override string GetDisplayName(Player player) => "Prominence Burn ";
        
   
    public override string Description => "Shoot a fireball";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "Fire";

    public override int BaseCooldown => 3600;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        int BaseDamage = 0;
        
            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                BaseDamage = 100;
                break;
            
                case QuirkStage.Adequation:
                BaseDamage = 150;
                break;
          
                case QuirkStage.Intermediate:
                BaseDamage =  220;
                break;
            
                case QuirkStage.Advanced:
                BaseDamage = 350;
                break;
          
                case QuirkStage.Final:
                BaseDamage = 500;
                break;
        
                default:
                BaseDamage =20;
                break;
                    
            }
        
            float modifiedDamage = 1f;

            
            if (hellPlayer.IsFlashFireFistActive)
            {
                modifiedDamage += 1.5f; 
            }
        
            if (hellPlayer.isSurgeArmGauntletsOn)
            {
                modifiedDamage += 0.5f; 
            }

            int finalDamage = (int)(BaseDamage * modifiedDamage);



        if (transPlayer.HasActiveQuirk(QuirkType.HellFlames)){
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<ChargeHellProminenceBurnProj>(),
                finalDamage, 
                2f, 
                player.whoAmI
            );
           
        }

        
            
        }}