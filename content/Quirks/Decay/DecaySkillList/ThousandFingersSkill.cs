using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Decay.Projectiles.RangeTouch;
using MyHeroMod.content.Quirks.Decay.Projectiles.ThousandFingers;

public class ThousandFingersSkill : QuirkBaseSkill
{
    public override string Name => "Thousand Hands";
    
        

    
    public override string Description => "Reach out with a thousand hands";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Decay";

    public override int BaseCooldown => 200;
    public override QuirkType RequiredQuirk => QuirkType.Decay;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;

    public override bool CheckUnlock(TransformationPlayer player)
    {
        
        if (player.Nature == NatureType.PerfectVessel && (player.HasActiveQuirk(QuirkType.Decay)||player.HasActiveQuirk(QuirkType.AllForOne) ) && player.CurrentStage >= QuirkStage.Advanced)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    


    public override void OnUse(Player player)
    {
        Vector2 velocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX) * 5f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                velocity,   
                ModContent.ProjectileType<ThousandFingersProj>(),
                15, 
                2f, 
                player.whoAmI);
        
    }
}