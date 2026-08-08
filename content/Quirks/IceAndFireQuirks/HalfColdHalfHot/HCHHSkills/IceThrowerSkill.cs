using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.IceThrower;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.ColdflamePaleblade;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.JetBurn;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills
{
    public class IceThrowerSkill : QuirkBaseSkill
    {
        public override string Name => "Ice Thrower";
        public override string Description => "Release a stream of elements. Evolves based on your active stance.";
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

            
            float multiplier = 1.0f;
            if (hchhPlayer.IsSurgeArmGauntletsOn) 
            {
                multiplier += 0.5f;
            }

            
            if (hchhPlayer.IsPhosphorActive)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<PaleflameController>()] > 0) return;
                
                int phosDamage = transPlayer.CurrentStage == QuirkStage.Final ? 550 : 180;
                
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<PaleflameController>(), (int)(phosDamage * multiplier), 0f, player.whoAmI);
            }
            else if (hchhPlayer.IsFlashFireFistActive)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0) return;
                
                int fireDamage = transPlayer.CurrentStage switch {
                    QuirkStage.Initial => 12, QuirkStage.Adequation => 22, QuirkStage.Intermediate => 55,
                    QuirkStage.Advanced => 130, QuirkStage.Final => 350, _ => 12
                };

                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ModContent.ProjectileType<JetKindlingController>(), (int)(fireDamage * multiplier), 0f, player.whoAmI);
                
                foreach (var modPlayer in player.ModPlayers)
                {
                    if (modPlayer is IHeroTemperature heatUser) heatUser.AddHeat(25);
                }
            }
            // Stance C: Base (Ice state)
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
                    if (modPlayer is IHeroTemperature heatUser) heatUser.ReduceHeat(25);
                }
            }
        }
    }
}
