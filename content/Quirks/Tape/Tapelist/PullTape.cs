using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Projectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Tape;
using MyHeroMod.content.Quirks.Tape.Projectiles;





public class PullTapeSkill : QuirkBaseSkill
    {
         public override string Name => "Pulling Tape";
    public override string Description => "Shoot a hook made from blackwhip at you cursor and pull yourself towards it";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";
    public override string Category => "Tape";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.Tape;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

        int baseDamage = transPlayer.CurrentStage switch
        {
            QuirkStage.Initial => 20,
            QuirkStage.Adequation => 50,
            QuirkStage.Intermediate => 90,
            QuirkStage.Advanced => 150,
            QuirkStage.Final => 250,
            _ => 20
        };

        if (player.ownedProjectileCounts[ModContent.ProjectileType<PullTapeProjectile>()] >= 2) 
            {
            return; 
            }
            CombatText.NewText(player.getRect(), Color.Orange, "Pull");
            Vector2 velocity = Main.MouseWorld - player.Center;
            velocity.Normalize();
            velocity *= 18f;

            
            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                velocity, 
                ModContent.ProjectileType<PullTapeProjectile>(), 
                baseDamage,  
                0f, 
                player.whoAmI);

    }
    }