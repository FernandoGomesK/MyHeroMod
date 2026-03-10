

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


public class StunGrenadeSkill : QuirkSkill
{
   
    public override string Name => "Stun Grenade";
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


            public override void OnUse(Player player)
    {
            var explodePlayer = player.GetModPlayer<ExplosionPlayer>();


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
                40, 
                2f, 
                player.whoAmI
            );
            explodePlayer.CurrentSweat += 15;
        }
    }