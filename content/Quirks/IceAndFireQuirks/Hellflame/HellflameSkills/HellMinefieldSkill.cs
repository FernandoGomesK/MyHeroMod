using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills
{
    public class HellMinefieldSkill : QuirkBaseSkill
    {
        public override string Name => "Hell Minefield";
        public override string GetDisplayName(Player player) => "Hell Minefield";
        
        public override string Description => "Release a wave of fire that leaves explosive mines along the ground.";
        public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
  
        public override string Category => "HellFlames"; 

        public override int BaseCooldown => 120;
        public override QuirkType RequiredQuirk => QuirkType.HellFlames;
        
       
        public override QuirkStage RequiredStage => QuirkStage.Initial; 
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
        
            int baseDamage = transPlayer.CurrentStage switch {
                QuirkStage.Initial => 20,
                QuirkStage.Adequation => 40,
                QuirkStage.Intermediate => 45,
                QuirkStage.Advanced => 60,
                QuirkStage.Final => 80,
                _ => 20
            };  

            float modifiedDamage = 1f;

            if (hellPlayer.IsFlashFireFistActive)
            {
                modifiedDamage += 1.5f;        
            }
            
            int finalDamage = (int)(baseDamage * modifiedDamage);

            if (transPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                Vector2 velocity = Main.MouseWorld - player.Center;
                velocity.Normalize();
                
            
                velocity *= 15f; 

                Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    player.Center,
                    velocity,
                    ModContent.ProjectileType<HellMinefieldController>(),
                    finalDamage, 
                    2f, 
                    player.whoAmI
                );
            }

            foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(15);
                }
            }
        }
    }
}