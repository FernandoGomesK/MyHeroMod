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
using MyHeroMod.content.Projectiles;
using Terraria.Graphics.CameraModifiers;


public class ApShotSkill : QuirkBaseSkill
{
    public override string Name => "Ap Shot";
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";
    public override string Category => "Explosion";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
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

            if (player.HasBuff(ModContent.BuffType<ClusterBuff>())) {
                damageMultiplier = 2.5f; 
            }

            var finalDamage = (int)(damageMultiplier * MaxDamage);

CombatText.NewText(player.getRect(), Color.Orange, "AP-SHOT!");
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
                ModContent.ProjectileType<ApShotProj>(),
                finalDamage, 
                2f, 
                player.whoAmI
            );
            explodePlayer.CurrentSweat += 15;
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }}
        