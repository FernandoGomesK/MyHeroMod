using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OpticBlast.Projectiles;
using MyHeroMod.content.Quirks.OpticBlast;


public class ContinuousOpticBlastSkill : QuirkBaseSkill
    {
        public override string Name => "Continuous Optic Blast";
        public override string Description => "Shoot a concentrated penetrating beam as long as you hold the key.";
        public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
        public override string Category => "OpticBlast";

        public override int BaseCooldown => 120; 

        public override QuirkType RequiredQuirk => QuirkType.OpticBlast;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;
        

        public override void OnUse(Player player)
        {
            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();

            if (opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.Zero || player.HasBuff(BuffID.Darkness))
            {
                return;
            }
            else
        {
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
