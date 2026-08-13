using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills
{
    public abstract class BaseHellSpiderSkill : QuirkBaseSkill
    {
        public override string Name => "Hell Spider";
        public override string GetDisplayName(Player player) => "Flashfire Fist: Hell Spider";
        public override string Description => "Shoot burning lines of fire";
        public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
        public override string Category => "Fire";
        public override int BaseCooldown => 120;
        public override bool IsDefaultSkill => false;
        
        public virtual float FlashfireFistModifier => 1.5f; 
        public virtual float SurgeArmGauntletModifier => 0.5f;

        
        protected abstract int HellSpiderProjType { get; } 
        
        
        protected virtual int HeatCost => 25;

        
        protected abstract int CalculateDamage(TransformationPlayer transPlayer);

        public override void OnUse(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
            
            if (player.ownedProjectileCounts[HellSpiderProjType] > 0) return;

           
            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            
            int baseDamage = CalculateDamage(transPlayer);
    
            
            float totalMultiplier = 1f;

            if (player.TryGetModPlayer(out BaseIceAndFirePlayer firePlayer))
        {
            if (firePlayer.IsFlashFireFistActive)
            {
                totalMultiplier += FlashfireFistModifier; 
            }
            
            if (firePlayer.isSurgeArmGauntletsOn)
            {
                totalMultiplier += SurgeArmGauntletModifier; 
            }
        }
          
            int finalDamage = (int)(baseDamage * totalMultiplier);

            
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, HellSpiderProjType, finalDamage, 0f, player.whoAmI);

          
            foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(HeatCost);
                }
            }
        }
    }
}