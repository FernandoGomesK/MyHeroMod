using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.IceShot;
using MyHeroMod.content.Projectiles.HellSpider;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.JetKindling;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.IceThrower;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.ColdflamesPaleblade;


public class iceThrowerJetKindling : QuirkSkill
{
    
    public override string Name => "Ice Thrower/Jet Kindling";

   
    public override string Description => "Fire a wave of ice or fire at the direction of your looking.";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

        // Multiplicador da Luva
        float multiplier = 1.0f;
        if (hchhPlayer.IsSurgeArmGauntletsOn) multiplier += 0.5f;

        Vector2 direction = Main.MouseWorld - player.Center;
        direction.Normalize();

        
        if (hchhPlayer.IsPhosphorActive)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<PaleflameController>()] > 0) return;

            
            int phosDamage = transPlayer.CurrentStage == QuirkStage.Final ? 550 : 180;

            int finalDamage = (int)(phosDamage * multiplier);

            Projectile.NewProjectile(
                player.GetSource_FromThis(), player.Center, direction,
                ModContent.ProjectileType<PaleflameController>(), finalDamage, 0f, player.whoAmI
            );
            
            
        }
        
        
        else if(hchhPlayer.IsFlashFireFistActive)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0) return;

            int fireDamage = 20;
            switch(transPlayer.CurrentStage) {
                case QuirkStage.Initial: fireDamage = 12; break;
                case QuirkStage.Adequation: fireDamage = 22; break;
                case QuirkStage.Intermediate: fireDamage = 55; break;
                case QuirkStage.Advanced: fireDamage = 130; break;
                case QuirkStage.Final: fireDamage = 350; break;
            }
            int finalDamage = (int)(fireDamage * multiplier);

            Projectile.NewProjectile(
                player.GetSource_FromThis(), player.Center, direction,
                ModContent.ProjectileType<JetKindlingController>(), finalDamage, 0f, player.whoAmI
            );
            
            hchhPlayer.temperature += 25; 
        }
        
        
        else
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<IceThrowerController>()] > 0) return;

            int iceDamage = 15;
            switch(transPlayer.CurrentStage) {
                case QuirkStage.Initial: iceDamage = 8; break;
                case QuirkStage.Adequation: iceDamage = 15; break;
                case QuirkStage.Intermediate: iceDamage = 35; break;
                case QuirkStage.Advanced: iceDamage = 90; break;
                case QuirkStage.Final: iceDamage = 220; break;
            }
            int finalDamage = (int)(iceDamage * multiplier);

            Projectile.NewProjectile(
                player.GetSource_FromThis(), player.Center, direction,
                ModContent.ProjectileType<IceThrowerController>(), finalDamage, 0f, player.whoAmI
            );
            
            hchhPlayer.temperature -= 25; 
        }
    }
}