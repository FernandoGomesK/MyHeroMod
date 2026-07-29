using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow.Projectiles; 
using MyHeroMod.content.System;
using rail;

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

            if (transPlayer.CurrentStage == QuirkStage.Intermediate &&
             (Player.HasBuff(ModContent.BuffType<Buffs.BlackAbyssBuff>()) || Player.HasBuff(ModContent.BuffType<Buffs.DarkShadowBuff>())))
            {
                Player.wingTimeMax =360000;

                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29; 
                    Player.wings = -1; 
                }

                Player.noFallDmg = true;
            }
        }

        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.DarkShadow))
                return;

                
            if (!Main.dayTime)
            {
                isMediumDarkShadowOn = true;
                
                if (transPlayer.CurrentStage == QuirkStage.Initial || transPlayer.CurrentStage == QuirkStage.Intermediate)
                {
                    isDarkShadowOn = true;
                }
                
                isUncontrolledMode = true;
            }

            if (isDarkShadowOn && !isBlackAbyssOn)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowBodyProj>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<DarkShadowBodyProj>(), 0, 0f, Player.whoAmI);
                }  
                
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowFrontHandProj>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<DarkShadowFrontHandProj>(), 10, 0f, Player.whoAmI);
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowBackHandProj>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<DarkShadowBackHandProj>(), 10, 0f, Player.whoAmI);
                }
    
                if (isUncontrolledMode) 
                {

                    HandleAutomaticAttacks();
                }
            }

            if (Player.velocity.Y != 0 && (Player.wingTime > 0 || Player.rocketDelay > 0) && !Player.mount.Active)
            {
                isFlying = true;
            }
        }
    }
}