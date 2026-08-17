using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.IceShot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles.ContinuousBlast.HellSpider;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills
{
    public class IceShotSkill : QuirkBaseSkill
    {
        public override string Name => "Ice Spike";

         public override string IconPath
        {
            get
            {
                Player player = Main.LocalPlayer;
                var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                
                
                if (hchhPlayer.IsFlashFireFistActive)
                {
                    return "MyHeroMod/Assets/SkillIcons/HCHH/HellSpiderIcon";  
                }
                else
                {
                    return "MyHeroMod/Assets/SkillIcons/HCHH/IceShotIcon"; 
                }
            }
        }

        public override string GetMorphedSkillName(Player player)
    {
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
        if (hchhPlayer.IsFlashFireFistActive)
        {
        
            return "Hell Spider"; 
        }
        return Name;
    }

        public override string GetDisplayName(Player player)
        {
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
   
            if (hchhPlayer.IsFlashFireFistActive)
            {
                return "Flashfire Fist: Hell Spider";
            }
            else
            {
                return "Ice Spike"; 
            }
            
        }
        public override string Description => "Shoot an Ice Spike. Evolves based on your active stance.";
        
        public override string Category => "HalfColdHalfHot";

        public override int BaseCooldown 
        {
            get
            {
               
                Player player = Main.LocalPlayer; 
                var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

                
                if (hchhPlayer.IsFlashFireFistActive)
                {
                    return 1500;
                }
                
                else
                {
                    return 300; 
                }  
            }
        }

        public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            
            float multiplier = 1.0f;
            if (hchhPlayer.isSurgeArmGauntletsOn) 
            {
                multiplier += 0.5f;
            }

            
            
            
            
            int iceDamage = transPlayer.CurrentStage switch {
                QuirkStage.Initial => 20, QuirkStage.Adequation => 40, QuirkStage.Intermediate => 45,
                QuirkStage.Advanced => 60, QuirkStage.Final => 80, _ => 25
            };

            Vector2 velocity = direction * 15f;
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, velocity, ModContent.ProjectileType<IceShotProj>(), (int)(iceDamage * multiplier), 2f, player.whoAmI);

            
            foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) heatUser.ReduceHeat(15);
            }
            
        }
    }



    public class HellSpiderSkill : QuirkBaseSkill
{
    
    public override string Name => "Hell Spider"; 
    public override string Description => "Release a concentrated net of fire.";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/HCHH/HellSpiderIcon"; 
    public override string Category => "HalfColdHalfHot";
    public override int BaseCooldown => 1500;
    public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;

    public override void OnUse(Player player)
    {

        Vector2 direction = Main.MouseWorld - player.Center;
        direction.Normalize();

             var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
            float multiplier = 1.0f;
            if (hchhPlayer.isSurgeArmGauntletsOn) 
            {
                multiplier += 0.5f;
            }

            if (hchhPlayer.IsFlashFireFistActive)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<HellSpiderController>()] > 0) return;
                
                int fireDamage = transPlayer.CurrentStage switch {
                    QuirkStage.Adequation => 50, QuirkStage.Intermediate => 75,
                    QuirkStage.Advanced => 90, QuirkStage.Final => 120, _ => 110
                };

                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<HellSpiderController>(), (int)(fireDamage * multiplier), 0f, player.whoAmI);
                
                
                foreach (var modPlayer in player.ModPlayers)
                {
                    if (modPlayer is IHeroTemperature heatUser) heatUser.AddHeat(25);
                }
            }
       
    }
}
}

