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
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.Blueflames;


public class HellSpiderSkill: QuirkSkill
{
    
    public override string Name => "Hell Spider";

   
    public override string Description => "Shoot a Huge Ice Spike at your Cursor or Lines of fire";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

public override void OnUse(Player player)
    {
    
 if (player.ownedProjectileCounts[ModContent.ProjectileType<HellSpiderController>()] > 0) return;

         var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
         var bluePlayer = player.GetModPlayer<BlueFlamesPlayer>();
         var transPlayer = player.GetModPlayer<TransformationPlayer>();

            
            float multiplier = 1.0f;
            

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
            
            hellPlayer.CurrentHeat += 15; 
        }
}