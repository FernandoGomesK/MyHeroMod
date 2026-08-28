using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.BlackWhip.Projectiles{
    public class AutomaticBlackWhipProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 20; 
            Projectile.height = 12; 
            Projectile.hostile = false; 
            Projectile.friendly = true;
            Projectile.penetrate = 1; 
            Projectile.tileCollide = true; 
            
            
            Projectile.timeLeft = 100; 
            Projectile.extraUpdates = 1; 
        }

        public override void AI()
        {
            Vector2 targetCenter = Vector2.Zero;
            bool hasTarget = false;
            float closestDistance = 400f;

          
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && !npc.friendly && npc.CanBeChasedBy())
                {
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetCenter = npc.Center;
                        hasTarget = true;
                    }
                }
            }

            if (hasTarget)
            {
                Vector2 targetDirection = (targetCenter - Projectile.Center).SafeNormalize(Vector2.Zero);
                float speed = Projectile.velocity.Length();
                if (speed < 8f) speed = 8f;

                
                Projectile.velocity = Vector2.Normalize(Vector2.Lerp(Projectile.velocity, targetDirection * speed, 0.15f)) * speed;
            }

            
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frozen, 120);  
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Microsoft.Xna.Framework.Graphics.Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);

                Main.EntitySpriteDraw(
                    texture, 
                    drawPos, 
                    null, 
                    color, 
                    Projectile.oldRot[k], 
                    drawOrigin, 
                    Projectile.scale, 
                    Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 
                    0
                );
            }
            return true; 
        }
    }
}
