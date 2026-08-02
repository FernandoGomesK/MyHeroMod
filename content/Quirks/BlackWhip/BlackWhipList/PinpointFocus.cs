using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.BlackWhip;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhip;


public class PinpointFocusSkill : QuirkBaseSkill
    {
         public override string Name => "Pinpoint Focus";
    public override string Description => "Shoot a tendril of blackwhip";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";
    public override string Category => "BlackWhip";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.BlackWhip;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {

        if (player.ownedProjectileCounts[ModContent.ProjectileType<BlackWhipProjectile>()] >= 2) 
            {
            return; 
            }
            CombatText.NewText(player.getRect(), Color.Orange, "BlackWhip!");
            Vector2 velocity = Main.MouseWorld - player.Center;
            velocity.Normalize();
            velocity *= 18f;

            
            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                velocity, 
                ModContent.ProjectileType<BlackWhipProjectile>(), 
                0,  
                0f, 
                player.whoAmI);

    }
    }