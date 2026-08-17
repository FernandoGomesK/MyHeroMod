using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.JetKindlingProjs;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Skills
{
    public class JetKindlingSkill : QuirkBaseSkill
    {
        public override string Name => "Jet Kindling";
        public override string Description => "Shoot a constant stream of fire or ice.";
        // public override string IconPath => 

        public override string IconPath
        {
            get
            {
                Player player = Main.LocalPlayer;
                var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                
                if (hchhPlayer.IsPhosphorActive)
                {
                    
                    return "MyHeroMod/Assets/SkillIcons/HCHH/PaleBladeIcon";
                }
                else if (hchhPlayer.IsFlashFireFistActive)
                {
                    return "MyHeroMod/Assets/SkillIcons/HCHH/JetKindlingIcon";  
                }
                else
                {
                    return "MyHeroMod/Assets/SkillIcons/HCHH/IceThrowerIcon"; 
                }
            }
        }


        public override string Category => "Fire";
        public override int BaseCooldown => 700;
        public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;

        public override string GetDisplayName(Player player)
        {
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
            
            if (hchhPlayer.IsPhosphorActive)
            {
                return "Coldflame's Pale Blade";
            }
            else if (hchhPlayer.IsFlashFireFistActive)
            {
                if (transPlayer.CurrentStage == QuirkStage.Adequation)
                {
                    return "Jet Kindling";
                }
                else
                {
                    return "Fire Blast";
                }
                
            }
            else
            {
                return "Ice Thrower"; 
            }
            
        }

        public override void OnUse(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot)) return;

            int iceDamage = 25;      
            int fireDamage = 40;     
            int phosphorDamage = 120; 

            switch(transPlayer.CurrentStage)
            {
                case QuirkStage.Initial: 
                    iceDamage = 25; fireDamage = 40; phosphorDamage = 120; break;
                case QuirkStage.Adequation: 
                    iceDamage = 45; fireDamage = 65; phosphorDamage = 180; break;
                case QuirkStage.Intermediate: 
                    iceDamage = 60; fireDamage = 80; phosphorDamage = 260; break;
                case QuirkStage.Advanced: 
                    iceDamage = 80; fireDamage = 110; phosphorDamage = 400; break;
                case QuirkStage.Final: 
                    iceDamage = 120; fireDamage = 150; phosphorDamage = 600; break;
            }

            
            float multiplier = 1f;

            if (hchhPlayer.isSurgeArmGauntletsOn)
            {
                multiplier += 0.5f; 
            }

        
            Vector2 velocity = Main.MouseWorld - player.Center;
            velocity.Normalize();
            velocity *= 15f;

            if (hchhPlayer.IsPhosphorActive)
            {
                int finalDamage = (int)(phosphorDamage * multiplier);
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), player.Center, velocity, 
                    ModContent.ProjectileType<JetPaleCharge>(), finalDamage, 2f, player.whoAmI
                );

            }
            else if (hchhPlayer.IsFlashFireFistActive)
            {
                int finalDamage = (int)(fireDamage * multiplier);
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), player.Center, velocity, 
                    ModContent.ProjectileType<JetKindlingCharge>(), finalDamage, 2f, player.whoAmI
                );
                
                hchhPlayer.AddHeat(35); 
            }
            else
            {
                int finalDamage = (int)(iceDamage * multiplier);
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), player.Center, velocity, 
                    ModContent.ProjectileType<JetIceCharge>(), finalDamage, 2f, player.whoAmI
                );      
                hchhPlayer.ReduceHeat(20);
            }
        }
    }
}