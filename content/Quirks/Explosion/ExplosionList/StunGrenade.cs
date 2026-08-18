

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
using MyHeroMod.content.Projectiles;
using Terraria.Graphics.CameraModifiers;



public class StunGrenadeSkill : QuirkBaseSkill
{
   
    public override string Name => "Stun Grenade";
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Explosion/StunGrenadeIcon";

    public override string Category => "Explosion";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    


            public override void OnUse(Player player)
    {
           var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var explodePlayer = player.GetModPlayer<ExplosionPlayer>();
            var isCluster = explodePlayer.IsClusterActive;

            

                 


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
            
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Vector2 textPosition = player.Center + new Vector2(0, -30f);
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                textPosition,
                Vector2.Zero, 
                ModContent.ProjectileType<BoomOnomatopoeia>(),
                0, 
                0f, 
                player.whoAmI
                );

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
            float shakeIntensity = isCluster ? 10f : 5f;
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), shakeIntensity, 15f, 20, 2000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }