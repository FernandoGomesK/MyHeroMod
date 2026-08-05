using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Decay.Projectiles.DashTouch;
using Microsoft.Xna.Framework;

public class DashTouchSkill : QuirkBaseSkill
{
    public override string Name => "Decay Dash";
    
        

    
    public override string Description => "Dash Forward reaching for your foes";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Decay";

    public override int BaseCooldown => 200;
    public override QuirkType RequiredQuirk => QuirkType.Decay;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    


    public override void OnUse(Player player)
    {
        Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<DashTouchProj>(),
                10, 
                10f, 
                player.whoAmI
                
            );
    }
}