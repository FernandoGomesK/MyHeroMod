using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow.Projectiles; 

namespace MyHeroMod.content.Quirks.DarkShadow
{
    public partial class DarkShadowPlayer : ModPlayer
    {
        public bool isDarkShadowOn = false;
        public bool isBlackAbyssOn = false;
        public bool isMediumDarkShadowOn = false;
        public bool isCBOArmsOn = false;

        public bool isFlying = false;
        public bool isUncontrolledMode = false; 

        public int DarkShadowRange => isMediumDarkShadowOn ? 800 : 600; 
        public bool isDarkShadowAutomatic = false;
        public int AutomaticAttackTimer = 0;
        public int AutomaticAttackCooldown = 60; 

        public int darkShadowBodyRange => isMediumDarkShadowOn ? 120 : 50;
        
        public bool isFrontHandAttacking => Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowLongFrontHandProj>()] > 0;
        public bool isBackHandAttacking => Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowLongBackHandProj>()] > 0;
        
        public override void ResetEffects()
        {
            isDarkShadowOn = false;      
            isBlackAbyssOn = false;
            isDarkShadowAutomatic = false;
            isCBOArmsOn = false;
            isMediumDarkShadowOn = false;
            isUncontrolledMode = false; 
            isFlying = false;
        }

        public override void FrameEffects()
        {
            if (Player.HasBuff(ModContent.BuffType<Buffs.BlackAbyssBuff>()))
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, "AbyssHead", EquipType.Head);
                Player.handon = EquipLoader.GetEquipSlot(Mod, "AbyssArms", EquipType.HandsOn);
                Player.handoff = EquipLoader.GetEquipSlot(Mod, "AbyssArms", EquipType.HandsOff);
            }
        }

        public override void PostUpdateEquips()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.DarkShadow))  
                return;

            bool hasFormBuff = Player.HasBuff(ModContent.BuffType<Buffs.BlackAbyssBuff>()) || 
                               Player.HasBuff(ModContent.BuffType<Buffs.DarkShadowBuff>());

            if (transPlayer.CurrentStage >= QuirkStage.Intermediate && hasFormBuff)
            {
                Player.wingTimeMax = 360000;
                Player.noFallDmg = true;

                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29; 
                    Player.wings = -1; 
                }
            }
        }

        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.DarkShadow))
                return;

            bool isNight = !Main.dayTime;
            bool isMastered = transPlayer.CurrentStage >= QuirkStage.Advanced; 

            
            isMediumDarkShadowOn = transPlayer.CurrentStage >= QuirkStage.Intermediate || isNight;

            
            if (isNight && !isMastered)
            {
                isDarkShadowOn = true;
                isUncontrolledMode = true;
            }

            if (isDarkShadowOn && !isBlackAbyssOn)
            {
            
                SpawnDarkShadowPart<DarkShadowBodyProj>(0);
                SpawnDarkShadowPart<DarkShadowFrontHandProj>(10);
                SpawnDarkShadowPart<DarkShadowBackHandProj>(10);
    
                if (isUncontrolledMode || isDarkShadowAutomatic) 
                {
                    HandleAutomaticAttacks();
                }
            }

            
            isFlying = Player.velocity.Y != 0 && !Player.mount.Active && (Player.wingTime > 0 || Player.rocketDelay > 0);
        }



        /// <summary>
        /// Summons Dark Shadow parts if they aren't already
        /// </summary>
        private void SpawnDarkShadowPart<T>(int damage) where T : ModProjectile
        {
            int projType = ModContent.ProjectileType<T>();
            if (Player.ownedProjectileCounts[projType] < 1)
            {
                Projectile.NewProjectile(
                    Player.GetSource_FromThis(), 
                    Player.Center, 
                    Vector2.Zero, 
                    projType, 
                    damage, 
                    0f, 
                    Player.whoAmI
                );
            }
        }
    }
}