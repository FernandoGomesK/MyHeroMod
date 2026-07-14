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
        public bool isCBOArmsOn = false;
        public bool isFrontHandAttacking => Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowLongFrontHandProj>()] > 0;
        public bool isBackHandAttacking => Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowLongBackHandProj>()] > 0;
        
        public override void ResetEffects()
        {
           
            isDarkShadowOn = false;      
            isBlackAbyssOn = false;
            isCBOArmsOn = false;
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
            if (isDarkShadowOn && !isBlackAbyssOn)
            {
                
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowBodyProj>()] < 1)
                {
                    
                    Projectile.NewProjectile(
                        Player.GetSource_FromThis(),
                        Player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<DarkShadowBodyProj>(),
                        0, 
                        0f,
                        Player.whoAmI
                    );
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowFrontHandProj>()] < 1)
                {
                    
                    Projectile.NewProjectile(
                        Player.GetSource_FromThis(),
                        Player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<DarkShadowFrontHandProj>(),
                        10, 
                        0f,
                        Player.whoAmI
                    );
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowBackHandProj>()] < 1)
                {
                    
                    Projectile.NewProjectile(
                        Player.GetSource_FromThis(),
                        Player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<DarkShadowBackHandProj>(),
                        10, 
                        0f,
                        Player.whoAmI
                    );
                }
            }
        }
    }
}