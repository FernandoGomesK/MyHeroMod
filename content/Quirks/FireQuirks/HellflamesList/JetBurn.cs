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
using MyHeroMod.content.Quirks.HellFlames.Projectiles.IgnitedArrow;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.JetKindling;
using MyHeroMod.content.Quirks.Blueflames;
using MyHeroMod.content.Quirks.AllForOne;


public class JetBurnSkill: QuirkSkill
{
    
    
    public override string Name 
    {
        get 
        {
            
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.SelectedQuirk == QuirkType.BlueFlames)
            {
                if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
                {
                    return "Flashfire Fist: Jet Burn";
                }
                
                else
                {
                    return "Flamethrower";
                }
            }

            return "Flashfire Fist: Jet Burn"; 
        }
    }

    public override string Description => "Fire a wave of flames";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public override bool CheckUnlock(TransformationPlayer player)
    {
        var bluePlayer = player.Player.GetModPlayer<BlueFlamesPlayer>();
        var afoPlayer = player.Player.GetModPlayer<AllForOnePlayer>();
        

        if (player.SelectedQuirk == QuirkType.BlueFlames || player.SelectedQuirk == QuirkType.HellFlames) 
            return player.CurrentStage >= QuirkStage.Initial;

        if (player.SelectedQuirk == QuirkType.AllForOne && (afoPlayer.HasInternalQuirk(QuirkType.BlueFlames) || afoPlayer.HasInternalQuirk(QuirkType.HellFlames)))
        {
            return true;
        }

        return false;
    }

public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
        var bluePlayer = player.GetModPlayer<BlueFlamesPlayer>();

        if (transPlayer.SelectedQuirk == QuirkType.HellFlames)
        {

            if (player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0) return;

            int fireDamage = 20;
            float multiplier = 1.0f;
            switch(transPlayer.CurrentStage) {
                case QuirkStage.Initial: fireDamage = 12; break;
                case QuirkStage.Adequation: fireDamage = 22; break;
                case QuirkStage.Intermediate: fireDamage = 55; break;
                case QuirkStage.Advanced: fireDamage = 130; break;
                case QuirkStage.Final: fireDamage = 350; break;
            }
            int finalDamage = (int)(fireDamage * multiplier);
            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                player.GetSource_FromThis(), player.Center, direction,
                ModContent.ProjectileType<JetKindlingController>(), finalDamage, 0f, player.whoAmI
            );
            
            hellPlayer.CurrentHeat += 25; 
        }
        else if (transPlayer.SelectedQuirk == QuirkType.BlueFlames)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0) return;

            int fireDamage = 20;
            float multiplier = 1.0f;
            switch(transPlayer.CurrentStage) {
                case QuirkStage.Initial: fireDamage = 12; break;
                case QuirkStage.Adequation: fireDamage = 22; break;
                case QuirkStage.Intermediate: fireDamage = 55; break;
                case QuirkStage.Advanced: fireDamage = 130; break;
                case QuirkStage.Final: fireDamage = 350; break;
            }
            int finalDamage = (int)(fireDamage * multiplier);
            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                player.GetSource_FromThis(), player.Center, direction,
                ModContent.ProjectileType<JetKindlingController>(), finalDamage, 0f, player.whoAmI
            );
            
            bluePlayer.CurrentHeat += 25; 
        }
        }

        
        
        
        }

        