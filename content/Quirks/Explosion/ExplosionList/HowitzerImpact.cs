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
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.Explosion.Projectiles;
using System.Diagnostics;


public class HowitzerImpactSkill : QuirkBaseSkill
{
   
    public override string Name => "Howitzer Impact";
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";
    public override string Category => "Explosion";

    public override int BaseCooldown => 520;

    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


            public override void OnUse(Player player)
    {
        var explodePlayer = player.GetModPlayer<ExplosionPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

    if (player.ownedProjectileCounts[ModContent.ProjectileType<HowitzerImpactProj>()] > 0)
                return;


            int BaseDamage = 150;

            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                BaseDamage = 1500;
                break;
            
                case QuirkStage.Adequation:
                BaseDamage = 150;
                break;
          
                case QuirkStage.Intermediate:
                BaseDamage = 300;
                break;
            
                case QuirkStage.Advanced:
                BaseDamage = 650;
                break;
          
                case QuirkStage.Final:
                BaseDamage = 1500;
                break;
        
                default:
                BaseDamage =150;
                break;
                    
            }
        
            

             
           
        float ModifiedDamage = 1;

        if (explodePlayer.IsClusterActive){
         
        ModifiedDamage += 2.5f;        
        }
        int FinalDamage = (int)(BaseDamage * ModifiedDamage);



            

            if (explodePlayer.IsClusterActive){
                CombatText.NewText(player.getRect(), Color.Orange, "HOWITZER IMPACT: CLUSTER!");
            }
            else
            {
                CombatText.NewText(player.getRect(), Color.Orange, "HOWITZER IMPACT!");
            }

            // Spawna o projétil que vai controlar o player
            // A velocidade inicial não importa aqui, pois a AI[0] controla a subida
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<HowitzerImpactProj>(),
                FinalDamage, // Dano alto (Impacto)
                10f, // Knockback alto
                player.whoAmI
            );
            explodePlayer.CurrentSweat += 15;
        }

        
}