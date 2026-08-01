using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Rivet.Projectiles;

public class RivetStabSkill : QuirkBaseSkill
{
    public override string Name => "Rivet Stab";
    public override string Description => "Create red like tendrils that stab at your cursor";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Rivet";

    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.Rivet;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        
        
        int projectileCount = transPlayer.CurrentStage switch
        {
            QuirkStage.Initial => 1,
            QuirkStage.Adequation => 2,
            QuirkStage.Intermediate => 4,
            QuirkStage.Advanced => 7,
            QuirkStage.Final => 10,
            _ => 1
        };

        Vector2 direction = Main.MouseWorld - player.Center;
        direction.Normalize();

        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 spreadVelocity = direction.RotatedByRandom(MathHelper.ToRadians(45)) * 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                spreadVelocity, 
                ModContent.ProjectileType<RivetStabPlayerProj>(), 
                10, 
                2f, 
                player.whoAmI
            );
        }
    }
}