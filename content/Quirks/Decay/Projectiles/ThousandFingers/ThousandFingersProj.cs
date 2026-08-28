using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Debuffs;

namespace MyHeroMod.content.Quirks.Decay.Projectiles.ThousandFingers
{
    public class ThousandFingersProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 60;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 20; 
            Projectile.height = 20; 
            Projectile.hostile = false; 
            Projectile.friendly = true;
            Projectile.penetrate = 1; 
            Projectile.tileCollide = true; 
            
            
            Projectile.timeLeft = 150; 
            Projectile.extraUpdates = 1; 
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ai[0]++; 
            var transPlayer = Main.player[Projectile.owner].GetModPlayer<TransformationPlayer>();
        
            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 targetPos = Main.MouseWorld;

                
                if (Projectile.ai[0] % 10 == 0) 
                {
                    Vector2 directionToTarget = targetPos - Projectile.Center;
                    float distanceToTarget = directionToTarget.Length();

                
                    if (distanceToTarget > 30f) 
                    {
                        directionToTarget.Normalize();
                        
                        float currentAngle = Projectile.velocity.ToRotation();
                        float targetAngle = directionToTarget.ToRotation();

                        float difference = MathHelper.WrapAngle(targetAngle - currentAngle);

                       
                        float maxTurn = MathHelper.PiOver2; 
                        
                        if (difference > maxTurn) difference = maxTurn;
                        else if (difference < -maxTurn) difference = -maxTurn;

                        
                        Projectile.velocity = Projectile.velocity.RotatedBy(difference);
                    }
                }
            }
            if (transPlayer.HasActiveQuirk(QuirkType.Decay))
            {
                if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Wraith);
            }
            }
        }


        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            
            target.AddBuff(ModContent.BuffType<DecayBuff>(), 300);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            target.AddBuff(ModContent.BuffType<DecayBuff>(), 300);
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
