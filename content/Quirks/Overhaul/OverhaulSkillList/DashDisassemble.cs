using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Overhaul.Projectiles.DashDisassemble;
using Microsoft.Xna.Framework;

public class DashDisassembleSkill : QuirkBaseSkill
{
    public override string Name => "Decay Dash";
    
        

    
    public override string Description => "Dash Forward reaching for your foes";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Overhaul";

    public override int BaseCooldown => 200;
    public override QuirkType RequiredQuirk => QuirkType.Overhaul;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    


    public override void OnUse(Player player)
    {
        Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<DashDisassembleProj>(),
                10, 
                10f, 
                player.whoAmI
                
            );
    }
}