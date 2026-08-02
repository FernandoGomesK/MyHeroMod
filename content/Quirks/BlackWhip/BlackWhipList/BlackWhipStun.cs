using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhip;
using MyHeroMod.content.Buffs;


public class BlackWhipStunSkill : QuirkBaseSkill
{
    public override string Name => "Black Whip Stun";
    public override string Description => "Attack with BlackWhip Stunning";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "BlackWhip";

    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.BlackWhip;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

        if (transPlayer.HasActiveQuirk(QuirkType.FaJin) && transPlayer.CurrentStage >= QuirkStage.Advanced && player.HasBuff(ModContent.BuffType<FaJinBuff>()))
        {
            CombatText.NewText(player.getRect(), Color.Orange, "Blackchain!");
        }
        else
        {
            CombatText.NewText(player.getRect(), Color.Orange, "BlackWhip Stun!");
        }
        
        
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
            Vector2 spreadVelocity = direction.RotatedByRandom(MathHelper.ToRadians(45)) * 8f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                spreadVelocity, 
                ModContent.ProjectileType<BlackWhipStunProj>(), 
                10, 
                2f, 
                player.whoAmI
            );
        }
    }
}