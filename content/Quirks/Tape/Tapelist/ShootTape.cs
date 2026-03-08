using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Projectiles;
using Microsoft.Xna.Framework;



    

    public class ShootTapeSkill : QuirkSkill
    {
         public override string Name => "Swinging Tape";
    public override string Description => "Shoot a hook made from blackwhip at you cursor and pull yourself towards it";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.Tape;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => true;
    public override bool IsBaseQuirk => false;

    public override void OnUse(Player player)
    {

        if (player.ownedProjectileCounts[ModContent.ProjectileType<SwingTapeProjectile>()] >= 2) 
            {
            return; 
            }
            CombatText.NewText(player.getRect(), Color.Orange, "One For All 5th: BlackWhip");
            Vector2 velocity = Main.MouseWorld - player.Center;
            velocity.Normalize();
            velocity *= 18f;

            
            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                velocity, 
                ModContent.ProjectileType<SwingTapeProjectile>(), 
                0,  
                0f, 
                player.whoAmI);

    }
    }