using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Decay.Projectiles.RangeTouch;

public class RangeTouchSkill : QuirkBaseSkill
{
    public override string Name => "Decay Hand";
    
        

    
    public override string Description => "Reach out with decay energy";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Decay";

    public override int BaseCooldown => 200;
    public override QuirkType RequiredQuirk => QuirkType.Decay;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    


    public override void OnUse(Player player)
    {
        Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<RangeTouchProj>(),
                15, 
                2f, 
                player.whoAmI);
        
    }
}