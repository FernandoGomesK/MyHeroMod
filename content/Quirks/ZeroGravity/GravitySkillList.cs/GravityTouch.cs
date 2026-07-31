using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Overhaul.Projectiles.DisassembleRange;
using MyHeroMod.content.Quirks.ZeroGravity.Projectiles.GravityTouch;

public class GravityTouchSkill : QuirkBaseSkill
{
    public override string Name => "Zero Gravity Touch";
    
        

    
    public override string Description => "Negate the gravitational pull of objects at a distance";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "ZeroGravity";

    public override int BaseCooldown => 120;
    public override QuirkType RequiredQuirk => QuirkType.ZeroGravity;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<GravityTouchProj>(),
                8, 
                2f, 
                player.whoAmI);
        
    }
}