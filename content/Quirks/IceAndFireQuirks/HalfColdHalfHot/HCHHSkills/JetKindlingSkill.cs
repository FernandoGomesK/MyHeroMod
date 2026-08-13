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
        public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
        public override string Category => "Fire";
        public override int BaseCooldown => 120;
        public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;

        public override string GetDisplayName(Player player)
        {
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            
            // FIX 1: Correctly checks Phosphor for the Pale Blade!
            if (hchhPlayer.IsPhosphorActive)
            {
                return "Coldflame's Pale Blade";
            }
            else if (hchhPlayer.IsFlashFireFistActive)
            {
                return "Jet Kindling";
            }
            return "Ice Thrower"; 
        }

        public override void OnUse(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot)) return;

            int baseDamage = 20;
            switch(transPlayer.CurrentStage)
            {
                case QuirkStage.Initial: baseDamage = 20; break;
                case QuirkStage.Adequation: baseDamage = 40; break;
                case QuirkStage.Intermediate: baseDamage = 45; break;
                case QuirkStage.Advanced: baseDamage = 60; break;
                case QuirkStage.Final: baseDamage = 80; break;
            }

            float modifiedDamage = 1f;

            if (hchhPlayer.IsFlashFireFistActive)
            {
                modifiedDamage += 1.5f;        
            }
            int finalDamage = (int)(baseDamage * modifiedDamage);

            
            Vector2 velocity = Main.MouseWorld - player.Center;
            velocity.Normalize();
            velocity *= 15f;

            if (hchhPlayer.IsPhosphorActive)
            {
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), player.Center, velocity, 
                    ModContent.ProjectileType<JetPaleCharge>(), finalDamage, 2f, player.whoAmI
                );
            }
            else if (hchhPlayer.IsFlashFireFistActive)
            {
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), player.Center, velocity, 
                    ModContent.ProjectileType<JetKindlingCharge>(), finalDamage, 2f, player.whoAmI
                );
                hchhPlayer.AddHeat(15);
            }
            else
            {
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), player.Center, velocity, 
                    ModContent.ProjectileType<JetIceCharge>(), finalDamage, 2f, player.whoAmI
                );      
                hchhPlayer.ReduceHeat(15);
            }
        }
    }
}