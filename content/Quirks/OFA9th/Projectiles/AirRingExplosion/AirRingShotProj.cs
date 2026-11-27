using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.OFA9th.Projectiles.AirRingExplosion
{
    public class AirRingShotProj : ModProjectile
    {
        private const int Lifetime = 30; 

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 80;
            Projectile.aiStyle = 0;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = Lifetime;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            Projectile.scale = 0.8f;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            // 0.0 -> 1.0 progress across the projectile's lifetime
            float progress = 1f - (Projectile.timeLeft / (float)Lifetime);

            // Evenly distribute the 5 frames across the full lifetime
            Projectile.frame = Math.Clamp((int)(progress * Main.projFrames[Projectile.type]), 0, Main.projFrames[Projectile.type] - 1);

            // Smoothly expand from 0.8x to targetScale over the lifetime
            float targetScale = Projectile.ai[0] > 0f ? Projectile.ai[0] : 3.5f;
            Projectile.scale = MathHelper.Lerp(0.8f, targetScale, progress);

            // Hold full opacity for the first 40% of the animation, then fade out cleanly to 255
            float fadeProgress = Math.Clamp((progress - 0.4f) / 0.6f, 0f, 1f);
            Projectile.alpha = (int)(fadeProgress * 255f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            int frameHeight = texture.Height / Main.projFrames[Type];
            Rectangle sourceRect = new Rectangle(0, Projectile.frame * frameHeight, texture.Width, frameHeight);
            Vector2 origin = sourceRect.Size() / 2f;

            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition,
                sourceRect,
                lightColor * Projectile.Opacity,
                Projectile.rotation,
                origin,
                Projectile.scale,
                SpriteEffects.None,
                0
            );

            return false;
        }
    }
}