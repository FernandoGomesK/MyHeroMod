using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyHeroMod.content.Buffs.MuscleAugment;
using MyHeroMod.content.Quirks.Hardening.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.DetroitSmash;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.MuscleAugment.Projectiles
{
    public class MusclePunchThroughProj : BaseChannelingProj
    {
       
        public override string Texture => "MyHeroMod/content/Quirks/MuscleAugment/Projectiles/AugmentArmTransformProj";

        protected override int ChannelTime => 60;
        protected override int TapThresholdTicks => 20;

        private int animTimer;
        private bool IsPartialForm(Player player) => !player.GetModPlayer<MuscleAugmentPlayer>().isMuscleAugmentActive;

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


            if (Main.myPlayer == Projectile.owner)
            {
                int aimDir = Main.MouseWorld.X >= player.Center.X ? 1 : -1;
                player.ChangeDir(aimDir);
            }

            if (animTimer == 1)
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound") with { Volume = 0.2f }, player.position);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/MuscleStressSound") with { Volume = 0.6f }, player.position);
            }

            
            int animDuration = 40;
            Projectile.frame = Math.Clamp((animTimer - 1) * 8 / animDuration, 0, 7);

            
            float offsetY = -Projectile.height + 6;
            float offsetX = player.direction == -1 ? 1f : 0f;
            Projectile.spriteDirection = player.direction;
            Projectile.Center = new Vector2(
                (float)Math.Floor(player.Center.X - 1 + offsetX),
                (float)Math.Floor(player.Bottom.Y + player.gfxOffY + offsetY));
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

           
            if (!IsPartialForm(player))
                return false;

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

        public override void OnTapRelease(Player player)
        {
            var musclePlayer = player.GetModPlayer<MuscleAugmentPlayer>();
            Vector2 direction = player.DirectionTo(Main.MouseWorld);
            Vector2 velocity = direction * 15f;

            int tapBase = musclePlayer.CalculateStageDamage(35, 90, 140, 220, 400);
            float formMult = musclePlayer.isMuscleAugmentActive ? 1f : 0.65f;
            int finalDamage = (int)player.GetTotalDamage(DamageClass.Melee).ApplyTo(tapBase * formMult);

            if (!musclePlayer.isMuscleAugmentActive)
            {
                player.AddBuff(ModContent.BuffType<PartialAugmentBuff>(), 120);
            }

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                velocity,
                ModContent.ProjectileType<RedGauntletProj>(),
                finalDamage,
                2f,
                player.whoAmI
            );
        }

        public override void OnChargeComplete(Player player)
        {
            var musclePlayer = player.GetModPlayer<MuscleAugmentPlayer>();

            int maxDamage = musclePlayer.CalculateStageDamage(100, 350, 500, 800, 1600);

            
            float formMultiplier = musclePlayer.isMuscleAugmentActive ? 1f : 0.65f;
            int finalDamage = (int)player.GetTotalDamage(DamageClass.Melee).ApplyTo(maxDamage * formMultiplier);

            Vector2 direction = player.DirectionTo(Main.MouseWorld);
            Vector2 velocity = direction * 15f;
            Vector2 baseSpawnLocation = player.Center + (direction * 90f);
            Vector2 currentSpawn = baseSpawnLocation - (direction * 25f);

            Projectile.NewProjectile(player.GetSource_FromThis(), currentSpawn, velocity, ModContent.ProjectileType<DetroitSmashAnimProj>(), finalDamage, 2f, player.whoAmI);
            Projectile.NewProjectile(player.GetSource_FromThis(), baseSpawnLocation, velocity, ModContent.ProjectileType<KhacesCore.Content.System.BaseProjectiles.PunchAttackProj>(), 0, 0f, player.whoAmI);

            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.1f }, player.position);
            SoundEngine.PlaySound(SoundID.Item74, player.Center);

            float shakeStrength = musclePlayer.isMuscleAugmentActive ? 10f : 6f;
            Main.instance.CameraModifiers.Add(new PunchCameraModifier(
                player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), shakeStrength, 15f, 20, 1000f, "MusclePunchShake"));

            if (!musclePlayer.isMuscleAugmentActive)
            {
                player.AddBuff(ModContent.BuffType<PartialAugmentBuff>(), 550);
            }
            
        }
    }
}