using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow.Projectiles; 
using MyHeroMod.content.System;
using KhacesCore.Content.System.Interfaces;

namespace MyHeroMod.content.Quirks.DarkShadow
{

    public partial class DarkShadowPlayer
    {
    
        private void HandleAutomaticAttacks()
        {
            if (IsFrontHandAttacking && IsBackHandAttacking) return;

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

                    if (!IsFrontHandAttacking)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, shootVelocity, ModContent.ProjectileType<DarkShadowLongFrontHandProj>(), damage, knockback, Player.whoAmI);
                    }
                    else if (!IsBackHandAttacking)
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