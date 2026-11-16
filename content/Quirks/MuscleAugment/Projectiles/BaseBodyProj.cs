using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs.MuscleAugment;

namespace MyHeroMod.content.Quirks.MuscleAugment.Projectiles
{
    /// <summary>
    /// Muscular body overlay that follows the player while Muscle Augmentation is active.
    /// Drawn over the player.
    /// </summary>
    public class BaseBodyProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.hide = true;
        }

        /// <summary>Draws the projectile over the player.</summary>
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active || !player.HasBuff<MuscleAugmentBuff>())
            {
                Projectile.Kill();
                return;
            }

            Projectile.timeLeft = 2;
            Projectile.spriteDirection = player.direction;

            float offsetY = -Projectile.height + 6;

            
            float offsetX = (player.direction == -1) ? 1f : 0f; 

            Projectile.Center = new Vector2(
                (float)Math.Floor(player.Center.X - 1 + offsetX),
                (float)Math.Floor(player.Bottom.Y + player.gfxOffY + offsetY));

            Projectile.velocity = Vector2.Zero;
        }
    }
}