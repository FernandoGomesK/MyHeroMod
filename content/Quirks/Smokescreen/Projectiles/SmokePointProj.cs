using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.Smokescreen.Projectiles
{
    /// <summary>
    /// Invisible, stationary projectile that keeps spawning smoke dust while it exists.
    /// The duration is passed through <c>ai[0]</c> (in ticks) so it is synced to every client.
    /// </summary>
    public class SmokePointProj : ModProjectile
    {
        #region Tuning

        public const int DefaultTimeLeft = 120;
        public const int FadeOutTicks = 60;

        public const float SmokeRadius = 40f;
        public const int DustPerTick = 3;

        public const float SmokeScaleMin = 2.0f;
        public const float SmokeScaleMax = 3.5f;

        private static readonly Color SmokeColor = Color.MediumPurple;

        public static int GetDuration(QuirkStage stage) => stage switch
        {
            QuirkStage.Initial => 300,
            QuirkStage.Adequation => 420,
            QuirkStage.Intermediate => 600,
            QuirkStage.Advanced => 900,
            _ => 1500
        };

        #endregion

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.WoodenArrowFriendly;

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = -1;        
            Projectile.friendly = false;      
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = DefaultTimeLeft;
            Projectile.DamageType = DamageClass.Generic;
        }

        /// <summary>
        /// Applies the duration received through ai[0]. ai values are part of the spawn packet,
        /// while timeLeft is not, so this runs on every client instead of setting timeLeft at the call site.
        /// </summary>
        public override void OnSpawn(IEntitySource source)
        {
            if (Projectile.ai[0] > 0f)
                Projectile.timeLeft = (int)Projectile.ai[0];
        }

        public override void AI()
        {
            Projectile.velocity = Vector2.Zero;
            if (Main.netMode == NetmodeID.Server)
                return;
            float fade = MathHelper.Clamp(Projectile.timeLeft / (float)FadeOutTicks, 0f, 1f);

            for (int i = 0; i < DustPerTick; i++)
            {
                if (Main.rand.NextFloat() > fade)
                    continue;

                Vector2 position = Projectile.Center + Main.rand.NextVector2Circular(SmokeRadius, SmokeRadius);
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-0.4f, 0.4f), Main.rand.NextFloat(-1.2f, -0.3f));
                float scale = Main.rand.NextFloat(SmokeScaleMin, SmokeScaleMax);

                Dust d = Dust.NewDustPerfect(position, DustID.Smoke, velocity, 100, SmokeColor, scale);
                d.noGravity = true;
            }

            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, Color.MediumPurple, 6.0f);
        }
        public override bool PreDraw(ref Color lightColor) => false;
    }
}