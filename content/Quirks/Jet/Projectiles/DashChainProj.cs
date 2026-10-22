using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Jet.Projectiles
{
    public class DashChainProj : ModProjectile
    {
        // Pointing directly to your new animation sprite sheet
        public override string Texture => "MyHeroMod/content/Quirks/Jet/Projectiles/TorinoChainProj";

        public override void SetStaticDefaults()
        {
            // Tell Terraria this sprite sheet has 6 vertical frames
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 40; // 5 seconds
            Projectile.alpha = 150;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            // --- ANIMATION LOGIC ---
            // Advance the frame every 4 ticks (15 FPS)
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                {
                    Projectile.frame = 0; // Loop back to the first frame
                }
            }

            // --- PLAYER LOCK & INVINCIBILITY ---
            player.Center = Projectile.Center;
            player.velocity = Vector2.Zero;
            player.immune = true;
            player.immuneTime = 5;
            player.controlUseItem = false;
            player.controlUseTile = false;
            player.AddBuff(BuffID.Invisibility, 2);

            // --- PARTICLES & LIGHTING ---
            if (Main.rand.NextBool(2))
            {
                Vector2 randomDustPos = Projectile.position + new Vector2(Main.rand.NextFloat(Projectile.width), Main.rand.NextFloat(Projectile.height));
                Dust dust = Dust.NewDustDirect(randomDustPos, 1, 1, DustID.Cloud, 0f, 0f, 100, Color.White, 1.2f);
                dust.velocity *= 0.5f;
                dust.noGravity = true;

                Lighting.AddLight(randomDustPos, 1f, 1f, 1f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // 1. DRAW THE DULL CYAN BACKGROUND
            // Grab Terraria's built-in 1x1 white pixel to stretch into the background square
            Texture2D magicPixel = TextureAssets.MagicPixel.Value;
            Rectangle bgRect = new Rectangle(
                (int)(Projectile.position.X - Main.screenPosition.X),
                (int)(Projectile.position.Y - Main.screenPosition.Y),
                Projectile.width,
                Projectile.height
            );

            Color bgColor = Color.Cyan * 0f;
            Vector2 bgScale = new Vector2(bgRect.Width, bgRect.Height);

            Main.EntitySpriteDraw(
                magicPixel,
                new Vector2(bgRect.X, bgRect.Y),
                null,
                bgColor,
                0f,
                Vector2.Zero,
                bgScale,
                SpriteEffects.None
            );

            // 2. DRAW THE 6-FRAME ANIMATION (TorinoChainProj)
            Texture2D animTexture = TextureAssets.Projectile[Type].Value;

            // Calculate the height of a single frame
            int frameHeight = animTexture.Height / Main.projFrames[Projectile.type];

            // Select the exact slice of the sprite sheet based on Projectile.frame
            Rectangle sourceRect = new Rectangle(0, Projectile.frame * frameHeight, animTexture.Width, frameHeight);

            // Set the origin to the center of the frame
            Vector2 drawOrigin = new Vector2(animTexture.Width / 2f, frameHeight / 2f);

            // Draw the animation directly in the center of the hitbox
            Main.EntitySpriteDraw(
                animTexture,
                Projectile.Center - Main.screenPosition,
                sourceRect,
                Color.White, // Ignore level lighting so the attack always looks bright
                Projectile.rotation,
                drawOrigin,
                Projectile.scale,
                SpriteEffects.None
            );

            return false; // Tells Terraria we already handled the drawing
        }
    }
}