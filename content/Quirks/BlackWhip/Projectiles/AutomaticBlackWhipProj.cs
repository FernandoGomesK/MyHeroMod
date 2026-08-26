using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.BlackWhip.Projectiles{
    public class AutomaticBlackWhipProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 150; 
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
         
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            
            if (Projectile.ai[0] == 0)
            {
               
                Projectile.timeLeft -= 1; 
            }
            else
            {
            
            }
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
