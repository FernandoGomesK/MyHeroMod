

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
using MyHeroMod.content.Quirks.Explosion.Projectiles.StunGrenade;



public class StunGrenadeSkill : QuirkBaseSkill
{
   
    public override string Name => "Stun Grenade";
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";
    public override string Category => "Explosion";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


            public override void OnUse(Player player)
    {
           var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var explodePlayer = player.GetModPlayer<ExplosionPlayer>();

                 


        float damageMultiplier = 1.0f;
        int MaxDamage = 45;
         

            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                MaxDamage = 25;
                break;
            
                case QuirkStage.Adequation:
                MaxDamage = 55;
                break;
          
                case QuirkStage.Intermediate:
                MaxDamage = 90;
                break;
            
                case QuirkStage.Advanced:
                MaxDamage = 160;
                break;
          
                case QuirkStage.Final:
                MaxDamage = 320;
                break;
        
                default:
                MaxDamage =45;
                break;
                    
            }

            if (player.HasBuff(ModContent.BuffType<ClusterBuff>())) {
                damageMultiplier = 2.5f; 
            

            
            }
            var finalDamage = (int)(damageMultiplier * MaxDamage);




            CombatText.NewText(player.getRect(), Color.Orange, "STUN GRENADE!");
            // Evita usar se já estiver usando
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<StunGrenadeProj>(),
                finalDamage, 
                2f, 
                player.whoAmI
            );
            explodePlayer.CurrentSweat += 15;
        }
    }