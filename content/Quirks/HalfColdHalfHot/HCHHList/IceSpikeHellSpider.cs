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

        if (hchhPlayer.IsFlashFireFistActive)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<HellSpiderController>()] > 0)
                return;

            
            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                direction,
                ModContent.ProjectileType<HellSpiderController>(),
                0, 
                0f,
                player.whoAmI
            );
            hchhPlayer.temperature += 35;
        }
        else
        {
            int IceDamage = 40;
            float multiplier = 1f;
            
            if (hchhPlayer.IsSurgeArmGauntletsOn)
            {
                multiplier += 1f;
            }
            int FinalDamage = (int)(IceDamage * multiplier);


            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<IceShotProj>(),
                FinalDamage, 
                2f, 
                player.whoAmI);

                hchhPlayer.temperature -= 25;
        }
        
        }
        }