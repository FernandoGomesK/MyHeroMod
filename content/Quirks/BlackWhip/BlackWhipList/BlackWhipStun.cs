using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhipStun;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackChain;
using MyHeroMod.content.Quirks.FaJin; 

public class BlackWhipStunSkill : QuirkBaseSkill
{
    public override string Name => "Black Whip Stun";
    public override string Description => "Attack with BlackWhip Stunning";
    public override string IconPath
        {
            get
            {   
                Player player = Main.LocalPlayer;
                var transPlayer = player.GetModPlayer<TransformationPlayer>();

                if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
                {
                    return "MyHeroMod/Assets/SkillIcons/Blackwhip/OFABlackwhipStunIcon"; 
                }
                else
                {
                    return "MyHeroMod/Assets/SkillIcons/Blackwhip/BlackwhipStunIcon"; 
                }
            }
        } 
    public override string Category => "BlackWhip";

    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.BlackWhip;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override QuirkStage RequiredOfaStage => QuirkStage.Intermediate;
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

        float damageMultiplier = 1f;
        bool isBlackChain = transPlayer.HasActiveQuirk(QuirkType.FaJin) && transPlayer.CurrentStage >= QuirkStage.Advanced && player.HasBuff(ModContent.BuffType<FaJinBuff>());

        
        if (isBlackChain)
        {
            CombatText.NewText(player.getRect(), Color.Orange, "Blackchain!");
            damageMultiplier = 1.5f; 
            
           
            var faJinPlayer = player.GetModPlayer<FajinPlayer>();
            faJinPlayer.FaJinCharges = 0;
            player.ClearBuff(ModContent.BuffType<FaJinBuff>());
        }
        else
        {
            CombatText.NewText(player.getRect(), Color.Orange, "BlackWhip Stun!");
        }

    
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

        for (int i = 0; i < projectileCount; i++)
        {
            
            Vector2 spreadVelocity = direction.RotatedByRandom(MathHelper.ToRadians(45)) * 8f;

            if (isBlackChain)
            {
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), 
                    player.Center, 
                    spreadVelocity, 
                    ModContent.ProjectileType<BlackChainProjectile>(), 
                    finalDamage, 
                    4f, 
                    player.whoAmI
                );
            }
            else
            {
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), 
                    player.Center, 
                    spreadVelocity, 
                    ModContent.ProjectileType<BlackWhipStunProj>(), 
                    finalDamage, 
                    2f, 
                    player.whoAmI
                );
            }
        }
    }
}