using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhipStun;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackChain;
using MyHeroMod.content.Quirks.FaJin;
using MyHeroMod.content.Quirks.FierceWings.Projectiles;
using MyHeroMod.content.Quirks.FierceWings;

public class ShootTrackedFeatherSkill : QuirkBaseSkill
{
    public override string Name => "Tracked Feather";
    public override string Description => "Shoot a feather that follows your cursor";
    public override string IconPath => "";
       
    public override string Category => "Fierce Wings";

    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.FierceWings;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    
    public override bool IsDefaultSkill => false;
    public override bool CanUse(Player player)
    {
        var featherPlayer = player.GetModPlayer<FierceWingsPlayer>();
    
        return featherPlayer.currentFeathers >= 20 && base.CanUse(player);
    }
    
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

        float damageMultiplier = 1f;
        
        
    
        int finalDamage = (int)(baseDamage * damageMultiplier);
        
    
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
        var featherPlayer = player.GetModPlayer<FierceWingsPlayer>();
        featherPlayer.currentFeathers -= 20;

        for (int i = 0; i < projectileCount; i++)
        {
            
            Vector2 spreadVelocity = direction.RotatedByRandom(MathHelper.ToRadians(45)) * 8f;

           
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), 
                    player.Center, 
                    spreadVelocity, 
                    ModContent.ProjectileType<TrackedFeatherProj>(), 
                    finalDamage, 
                    2f, 
                    player.whoAmI
                );
            
        }
    }
}