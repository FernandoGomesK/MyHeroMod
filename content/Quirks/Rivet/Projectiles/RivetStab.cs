using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.Rivet.Projectiles
{
    public class RivetStabPlayerProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 60; 
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 12; 
            Projectile.height = 8; 
            
            // CORREÇÃO: Agora ele ataca inimigos e não fere o jogador
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
            Projectile.ai[0]++; 

            // Só quem atirou pode ditar a posição do mouse (prevenção de bugs no multiplayer)
            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 targetPos = Main.MouseWorld;

                // Vira de forma brusca a cada 10 frames
                if (Projectile.ai[0] % 10 == 0) 
                {
                    Vector2 directionToTarget = targetPos - Projectile.Center;
                    float distanceToTarget = directionToTarget.Length();

                    // Evita que o projétil fique "tremendo" se já estiver muito perto do mouse
                    if (distanceToTarget > 30f) 
                    {
                        directionToTarget.Normalize();
                        
                        float currentAngle = Projectile.velocity.ToRotation();
                        float targetAngle = directionToTarget.ToRotation();

                        float difference = MathHelper.WrapAngle(targetAngle - currentAngle);

                        // EFEITO ÔMEGA: Curvas muito bruscas (até 90 graus de uma vez)
                        float maxTurn = MathHelper.PiOver2; 
                        
                        if (difference > maxTurn) difference = maxTurn;
                        else if (difference < -maxTurn) difference = -maxTurn;

                        // Aplica a nova rotação sem perder a velocidade
                        Projectile.velocity = Projectile.velocity.RotatedBy(difference);
                    }
                }
            }
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