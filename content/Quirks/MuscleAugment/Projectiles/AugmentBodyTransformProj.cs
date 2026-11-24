using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyHeroMod.content.Buffs.MuscleAugment;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.MuscleAugment.Projectiles
{
    public class AugmentBodyTransformProj : BaseChannelingProj
    {
        protected override int ChannelTime => 40;

        private int animTimer;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = 30;
            Projectile.height = 24;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = false;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs,
            List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }

        public override void AI()
        {
            base.AI();
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead)
                return;

            player.velocity *= 0.6f;
            animTimer++;

            if (animTimer == 1)
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound") with { Volume = 0.2f}, player.position);
                

            // Even 5-tick pacing per frame across the 40-tick channel
            Projectile.frame = Math.Clamp((animTimer - 1) * 8 / ChannelTime, 0, 7);

            // Lock onto the player's body position
            float offsetY = -Projectile.height + 6;
            float offsetX = player.direction == -1 ? 1f : 0f;
            Projectile.spriteDirection = player.direction;
            Projectile.Center = new Vector2(
                (float)Math.Floor(player.Center.X - 1 + offsetX),
                (float)Math.Floor(player.Bottom.Y + player.gfxOffY + offsetY));
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
            int frameHeight = texture.Height / Main.projFrames[Type];
            Rectangle sourceRect = new Rectangle(0, Projectile.frame * frameHeight, texture.Width, frameHeight);
            Vector2 origin = sourceRect.Size() / 2f;

            SpriteEffects effects = Projectile.spriteDirection == -1
                ? SpriteEffects.FlipHorizontally
                : SpriteEffects.None;

            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition,
                sourceRect,
                lightColor,
                Projectile.rotation,
                origin,
                Projectile.scale,
                effects,
                0);

            return false;
        }

        public override void SpawnChargingDust(Player player) { }
        public override void OnChargeCancelled(Player player) { }

        public override void OnChargeComplete(Player player)
        {
            player.AddBuff(ModContent.BuffType<MuscleAugmentBuff>(), 360000);
            player.ClearBuff(ModContent.BuffType<PartialAugmentBuff>());

            SoundEngine.PlaySound(SoundID.Item74, player.Center);

            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Cloud, 0f, 0f, 100, Color.White, 1.2f);
                d.velocity = Main.rand.NextVector2Circular(4f, 4f);
                d.noGravity = true;
            }

            Main.instance.CameraModifiers.Add(new PunchCameraModifier(
                player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 6f, 10f, 15, 1000f, "MuscleAugmentShake"));
        }
    }
}