using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.DarkShadow;
using MyHeroMod.content.System; 

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class DarkShadowBodyProj : ModProjectile
    {
        
        public int mediumFrame = 0;
        public int mediumFrameCounter = 0;

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            if (player.dead || !player.active || !darkPlayer.isDarkShadowOn || darkPlayer.isBlackAbyssOn)
            {
                Projectile.Kill();
                return;
            }

            Projectile.timeLeft = 2; 

            float offsetX = -50f;
            float offsetY = -30f;

            if (darkPlayer.isMediumDarkShadowOn)
            {
                Projectile.width = 40; 
                Projectile.height = 40;
                offsetX = -80f; 
            }
            else
            {
                Projectile.width = 28; 
                Projectile.height = 28;
            }

            if (darkPlayer.isFlying)
            {
                offsetX = -5f; 
                offsetY = darkPlayer.isMediumDarkShadowOn ? -70f : -50f;
            }

            Vector2 hoverPosition = player.Center + new Vector2(offsetX * player.direction, offsetY);
            int targetSpriteDirection = player.direction; 

            if (!darkPlayer.isFlying && (darkPlayer.isDarkShadowAutomatic || darkPlayer.isUncontrolledMode))
            {
                IClosestEnemyFinder targetFinder = new TargetFinder();
                NPC target = targetFinder.FindClosestEnemy(player, darkPlayer.DarkShadowRange, darkPlayer.isUncontrolledMode);

                if (target != null)
                {
                    Vector2 directionToTarget = (target.Center - player.Center).SafeNormalize(Vector2.Zero);
                    hoverPosition = player.Center + (directionToTarget * darkPlayer.darkShadowBodyRange);
                    targetSpriteDirection = target.Center.X < player.Center.X ? -1 : 1;
                }
            }
        
            Vector2 direction = hoverPosition - Projectile.Center;
            float distance = direction.Length();

            if (distance > 10f)
            {
                direction.Normalize();
                Projectile.velocity = (Projectile.velocity * 10f + direction * 6f) / 11f; 
            }
            else
            {
                Projectile.velocity *= 0.8f; 
            }

            Projectile.spriteDirection = targetSpriteDirection; 

            Vector2 playerPoint = player.Center;
            float tailX = Projectile.spriteDirection == 1 ? 0f : Projectile.width;
            Vector2 darkShadowTail = Projectile.position + new Vector2(tailX, Projectile.height);
            Color shadowColor = new Color(24, 0, 33);

            int dustAmount = darkPlayer.isMediumDarkShadowOn ? 8 : 5; 

            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 cordPos = Vector2.Lerp(playerPoint, darkShadowTail, Main.rand.NextFloat());
                cordPos += Main.rand.NextVector2Circular(8f, 8f); 
                
                Dust dust = Dust.NewDustPerfect(cordPos, DustID.Shadowflame, Vector2.Zero, 0, shadowColor);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(1.2f, 2.0f);
                dust.velocity = Main.rand.NextVector2Circular(0.5f, 0.5f);
            }

            // 2. LÓGICA DE ANIMAÇÃO INDEPENDENTE
            if (darkPlayer.isMediumDarkShadowOn)
            {
                mediumFrameCounter++;
                // Troca de frame a cada 5 ticks (diminua para animar mais rápido, aumente para mais devagar)
                if (mediumFrameCounter >= 5) 
                {
                    mediumFrame++;
                    mediumFrameCounter = 0;
                    

                    if (mediumFrame >= 12) 
                    {
                        mediumFrame = 0;
                    }
                }
            }
            else
            {
                mediumFrame = 0; 
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            if (darkPlayer.isMediumDarkShadowOn)
            {
                var Path = "MyHeroMod/content/Quirks/DarkShadow/Projectiles/MediumDarkShadowBodyProj"; 
                Texture2D mediumTexture = ModContent.Request<Texture2D>(Path).Value;

                
                int frameHeight = mediumTexture.Height / 12; 
                
                
                Rectangle sourceRect = new Rectangle(0, mediumFrame * frameHeight, mediumTexture.Width, frameHeight);

                
                Vector2 drawOrigin = new Vector2(mediumTexture.Width * 0.5f, frameHeight * 0.5f);
                Vector2 drawPos = Projectile.Center - Main.screenPosition;
                SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                Main.EntitySpriteDraw(
                    mediumTexture,
                    drawPos,
                    sourceRect,
                    Projectile.GetAlpha(lightColor), 
                    Projectile.rotation,
                    drawOrigin,
                    Projectile.scale,
                    effects,
                    0
                );

                return false;
            }

            return true; 
        }
    }
}