using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OpticBlast.Projectiles;
using MyHeroMod.content.Quirks.OpticBlast;

namespace MyHeroMod.content.Quirks.OpticBlast.Skills{
public class ContinuousOpticBlastSkill : QuirkBaseSkill
    {
        public override string Name => "Continuous Optic Blast";
        public override string Description => "Shoot a concentrated penetrating beam as long as you hold the key.";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/OpticBlast/ContinuousBlastIcon";
        public override string Category => "OpticBlast";

        public override int BaseCooldown => 120; 

        public override QuirkType RequiredQuirk => QuirkType.OpticBlast;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;
        

        public override void OnUse(Player player)
        {
            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.Zero || player.HasBuff(BuffID.Blackout))
            {
                return;
            }
            else
        {

            if (Main.myPlayer == player.whoAmI && opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.Full)
            {
                Color impactColor = (transPlayer.CurrentVariant == QuirkVariant.Variant1) ? Color.Pink : Color.Red;
                ImpactFrameSystem.Trigger(impactColor, false, "MyHeroMod/Assets/Effects/BlankImpactImage", "MyHeroMod/Assets/Effects/BlankImpactImage");
            }
            int damage = 35; 

            
            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                Vector2.Zero, 
                ModContent.ProjectileType<SustainedOpticBlastProj>(), 
                damage, 
                4f, 
                player.whoAmI
            );
            
        }
            
        }
    }
}