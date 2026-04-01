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
using MyHeroMod.content.Quirks.SlideAndGlide.Projectiles.ShootyGo;

public class ShootyGoBBBSkill : QuirkSkill
{
    public override string Name => "Ap Machine Gun";
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";

    public override int BaseCooldown => 300;

    public override QuirkType RequiredQuirk => QuirkType.SlideAndGlide;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


                    public override void OnUse(Player player)
            { 
                if (player.ownedProjectileCounts[ModContent.ProjectileType<ShootyGoBBBController>()] > 0)
            return;

            if (player.GetModPlayer<TransformationPlayer>().CurrentStage >= QuirkStage.Final)
            {
                CombatText.NewText(player.getRect(), Color.Orange, "Shooty Go Kablam!");
            }
            else
            {
                CombatText.NewText(player.getRect(), Color.Orange, "Shooty Go Blam Blam Blam!");
            }

            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            // Lança o Controlador
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                direction,
                ModContent.ProjectileType<ShootyGoBBBController>(),
                0, 
                0f, 
                player.whoAmI

             );

            }}