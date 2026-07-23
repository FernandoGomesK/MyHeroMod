using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.HalfColdHalfHot;


using MyHeroMod.content.Quirks.HellFlames;

using MyHeroMod.content.Quirks.Blueflames;
using MyHeroMod.content.Quirks.AllForOne;

using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.JetBurn;
using MyHeroMod.content.System.Interfaces;

using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.ColdflamePaleblade;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.IceThrower; 

public class JetBurnSkill : QuirkBaseSkill
{
    public override string Name 
    {
        get 
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            // PRIORIDADE 1: HCHH
            if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                return "Ice Thrower / Jet Kindling";
            }
            // PRIORIDADE 2: BlueFlames
            else if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames))
            {
                if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
                    return "Flashfire Fist: Jet Burn";
                else
                    return "Flamethrower";
            }
            
            // PRIORIDADE 3 / Padrão: HellFlames
            return "Flashfire Fist: Jet Burn"; 
        }
    }

    public override string Description => "Fire a wave of flames or ice";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "Fire";
    public override int BaseCooldown => 120;
    
    
    public override QuirkType RequiredQuirk => QuirkType.HellFlames; 
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    
    public override bool CheckUnlock(TransformationPlayer player)
    {
    
        if (player.HasActiveQuirk(QuirkType.HalfColdHalfHot) || 
            player.HasActiveQuirk(QuirkType.HellFlames) || 
            player.HasActiveQuirk(QuirkType.BlueFlames))
        {
            return player.CurrentStage >= QuirkStage.Initial; 
        }
        return false;
    }

    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
        var bluePlayer = player.GetModPlayer<BlueFlamesPlayer>();
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

        Vector2 direction = Main.MouseWorld - player.Center;
        direction.Normalize();

        float multiplier = 1.0f;
        
        if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot) && hchhPlayer.IsSurgeArmGauntletsOn) 
        {
            multiplier += 0.5f;
        }

        
        int fireDamage = 12; 
        
       
        if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames))
        {
            fireDamage = transPlayer.CurrentStage switch {
                QuirkStage.Initial => 12, QuirkStage.Adequation => 55, QuirkStage.Intermediate => 120,
                QuirkStage.Advanced => 200, QuirkStage.Final => 400, _ => 12
            };
        }
        
        else 
        {
            fireDamage = transPlayer.CurrentStage switch {
                QuirkStage.Initial => 12, QuirkStage.Adequation => 22, QuirkStage.Intermediate => 55,
                QuirkStage.Advanced => 130, QuirkStage.Final => 350, _ => 12
            };
        }

      

        if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
        {
            if (hchhPlayer.IsPhosphorActive)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<PaleflameController>()] > 0) return;
                int phosDamage = transPlayer.CurrentStage == QuirkStage.Final ? 550 : 180;
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<PaleflameController>(), (int)(phosDamage * multiplier), 0f, player.whoAmI);
            }
            else if (hchhPlayer.IsFlashFireFistActive)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0) return;
                
                
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<JetKindlingController>(), (int)(fireDamage * multiplier), 0f, player.whoAmI);
                
                foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(25);
                }
            }

            return;
            }
            else
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<IceThrowerController>()] > 0) return;
                int iceDamage = transPlayer.CurrentStage switch {
                    QuirkStage.Initial => 8, QuirkStage.Adequation => 15, QuirkStage.Intermediate => 35,
                    QuirkStage.Advanced => 90, QuirkStage.Final => 220, _ => 8
                };
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<IceThrowerController>(), (int)(iceDamage * multiplier), 0f, player.whoAmI);
                foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.ReduceHeat(25);
                }

                
            }

            return;
            }
        }
        
        else if (transPlayer.HasActiveQuirk(QuirkType.HellFlames))
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0) return;
            
            
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<JetKindlingController>(), (int)(fireDamage * multiplier), 0f, player.whoAmI);
            
            foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(25);
                }

                
            }

            return;
        }
        
        else if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames))
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0) return;
            
            
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<JetKindlingController>(), (int)(fireDamage * multiplier), 0f, player.whoAmI);
            
            foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(25);
                }

                
            }

            return;
        }

        
    }}