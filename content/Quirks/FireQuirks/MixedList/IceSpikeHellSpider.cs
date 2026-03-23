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
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.HCHellSpider;


public class iceSpikeHellSpider: QuirkSkill
{
    
    public override string Name => "Ice Spike/Hell Spider";

   
    public override string Description => "Shoot a Huge Ice Spike at your Cursor or Lines of fire";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    
        public override void OnUse(Player player)
    {
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

        
        float multiplier = 1.0f;
        if (hchhPlayer.IsSurgeArmGauntletsOn) multiplier += 0.5f;

      
        
        
        
        if (hchhPlayer.IsFlashFireFistActive)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<HCHellSpiderController>()] > 0) return;

          
            int fireDamage = 110;
            switch(transPlayer.CurrentStage){
                case QuirkStage.Adequation: fireDamage = 110; break;
                case QuirkStage.Intermediate: fireDamage = 180; break;
                case QuirkStage.Advanced: fireDamage = 360; break;
                case QuirkStage.Final: fireDamage = 760; break;
            }
            int finalDamage = (int)(fireDamage * multiplier);

            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

        
            Projectile.NewProjectile(
                player.GetSource_FromThis(), player.Center, direction,
                ModContent.ProjectileType<HCHellSpiderController>(), finalDamage, 2f, player.whoAmI
            );
            
            hchhPlayer.temperature += 35; 
        }
        
        
        else
        {
        
            int IceDamage = 25;
            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial: IceDamage = 25; break;
                case QuirkStage.Adequation: IceDamage = 55; break;
                case QuirkStage.Intermediate: IceDamage = 90; break;
                case QuirkStage.Advanced: IceDamage = 180; break;
                case QuirkStage.Final: IceDamage = 380; break;
            }
            int finalDamage = (int)(IceDamage * multiplier);

            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(), player.Center, Velocity,
                ModContent.ProjectileType<IceShotProj>(), finalDamage, 2f, player.whoAmI
            );

            hchhPlayer.temperature -= 25; 
        }
    }
}