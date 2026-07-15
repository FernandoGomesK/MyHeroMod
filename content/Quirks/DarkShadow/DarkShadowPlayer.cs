using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow.Projectiles; 
using MyHeroMod.content.System; // Required to use the TargetFinder

namespace MyHeroMod.content.Quirks.DarkShadow
{
    public partial class DarkShadowPlayer : ModPlayer
    {
        public bool isDarkShadowOn = false;
        public bool isBlackAbyssOn = false;
        public bool isMediumDarkShadowOn = false;
        public bool isCBOArmsOn = false;

        
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

        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!Main.dayTime)
            {
                isMediumDarkShadowOn = true;
                
                if (transPlayer.CurrentStage == QuirkStage.Initial || transPlayer.CurrentStage == QuirkStage.Intermediate)
                {
                    isDarkShadowOn = true;
                }
                {
                    isUncontrolledMode = true;
                }
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

        }

    
private void HandleAutomaticAttacks()
{
    if (isFrontHandAttacking && isBackHandAttacking) return;

    
    var targetFinder = new TargetFinder();
    NPC closestNPC = targetFinder.FindClosestEnemy(Player, DarkShadowRange, isUncontrolledMode);

    if (closestNPC != null)
    {
        AutomaticAttackTimer++;

        if (AutomaticAttackTimer >= AutomaticAttackCooldown)
        {
            Vector2 attackDirection = (closestNPC.Center - Player.Center).SafeNormalize(Vector2.Zero);
            float shootSpeed = 15f; 
            Vector2 shootVelocity = attackDirection * shootSpeed;

            int damage = isMediumDarkShadowOn ? 80 : 40; 
            float knockback = 5f;

            if (!isFrontHandAttacking)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, shootVelocity, ModContent.ProjectileType<DarkShadowLongFrontHandProj>(), damage, knockback, Player.whoAmI);
            }
            else if (!isBackHandAttacking)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, shootVelocity, ModContent.ProjectileType<DarkShadowLongBackHandProj>(), damage, knockback, Player.whoAmI);
            }

            AutomaticAttackTimer = 0;
        }
    }
    else
    {
        if (AutomaticAttackTimer > 0) AutomaticAttackTimer--;
    }
}
    }
}