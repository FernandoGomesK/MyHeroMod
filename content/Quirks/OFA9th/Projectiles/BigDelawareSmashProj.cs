using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{
    public class BigDelawareSmashProj : ModProjectile
    {
        
        
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 60;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, Color.WhiteSmoke, 2.0f);
            }
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Projectile.scale < 3.0f)
            {
                Projectile.scale += 0.05f;
                Vector2 oldCenter = Projectile.Center;
                Projectile.width = (int)(50 * Projectile.scale);
                Projectile.height = (int)(50 * Projectile.scale);
                Projectile.Center = oldCenter;
            }

            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.5f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

           
            Vector2 origin = texture.Size() / 2f;

            
            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition, 
                null,
                lightColor,
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