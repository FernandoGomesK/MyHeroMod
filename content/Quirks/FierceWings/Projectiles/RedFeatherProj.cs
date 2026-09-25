using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System;

namespace MyHeroMod.content.Quirks.FierceWings.Projectiles
{
    public class RedFeatherProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
         
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;

        }

        public override void AI()
        {
            
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        
            if (Main.rand.NextBool(5))
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch, 0, 0, 100, default, 1f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.5f; 
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
      
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);


                Color color = Color.Red * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                color.A = 150;

             
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true; 
        }
    }
}