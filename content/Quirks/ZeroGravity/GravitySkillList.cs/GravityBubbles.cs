using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Overhaul.Projectiles.DisassembleRange;
using MyHeroMod.content.Quirks.ZeroGravity.Projectiles.GravityTouch;
using MyHeroMod.content.Quirks.ZeroGravity.Projectiles.GravityBubble;

public class GravityBubbleSkill : QuirkBaseSkill
{
    public override string Name => "Zero Gravity Bubble";
    
        

    
    public override string Description => "Negate the gravitational pull of objects at a distance";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/ZeroGravity/GravityBubbleIcon";
    public override string Category => "ZeroGravity";

    public override int BaseCooldown => 300;
    public override QuirkType RequiredQuirk => QuirkType.ZeroGravity;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
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

        Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<GravityBubbleProj>(),
                baseDamage, 
                2f, 
                player.whoAmI);
        
    }
}