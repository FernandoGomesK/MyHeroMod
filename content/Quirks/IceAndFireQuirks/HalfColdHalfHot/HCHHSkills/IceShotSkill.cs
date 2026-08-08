using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.IceShot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.HellSpider;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills
{
    public class IceShotSkill : QuirkBaseSkill
    {
        public override string Name => "Ice Spike";

        public override string GetDisplayName(Player player)
        {
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
   
            if (hchhPlayer.IsFlashFireFistActive)
            {
                return "(FF) Hell Spider";
            }
            return "Ice Spike"; 
        }
        public override string Description => "Shoot an Ice Spike. Evolves based on your active stance.";
        public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
        public override string Category => "HalfColdHalfHot";

        public override int BaseCooldown => 120;
        public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            // 1. Calculate Multiplier (Surge Arm Gauntlets)
            float multiplier = 1.0f;
            if (hchhPlayer.IsSurgeArmGauntletsOn) 
            {
                multiplier += 0.5f;
            }

            // 2. STANCE CHECK: Flashfire Fist -> Hell Spider
            if (hchhPlayer.IsFlashFireFistActive)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<HellSpiderController>()] > 0) return;
                
                int fireDamage = transPlayer.CurrentStage switch {
                    QuirkStage.Adequation => 110, QuirkStage.Intermediate => 180,
                    QuirkStage.Advanced => 360, QuirkStage.Final => 760, _ => 110
                };

                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<HellSpiderController>(), (int)(fireDamage * multiplier), 0f, player.whoAmI);
                
                // Add Heat (Fire Side)
                foreach (var modPlayer in player.ModPlayers)
                {
                    if (modPlayer is IHeroTemperature heatUser) heatUser.AddHeat(25);
                }
            }
            // 3. BASE STANCE: Ice Spike
            else
            {
                int iceDamage = transPlayer.CurrentStage switch {
                    QuirkStage.Initial => 25, QuirkStage.Adequation => 55, QuirkStage.Intermediate => 90,
                    QuirkStage.Advanced => 180, QuirkStage.Final => 380, _ => 25
                };

                Vector2 velocity = direction * 15f;
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, velocity, ModContent.ProjectileType<IceShotProj>(), (int)(iceDamage * multiplier), 2f, player.whoAmI);

                // Add Cold / Reduce Heat (Ice Side)
                foreach (var modPlayer in player.ModPlayers)
                {
                    if (modPlayer is IHeroTemperature heatUser) heatUser.ReduceHeat(25);
                }
            }
        }
    }
}