
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{
    public class DetroitSmashProj : ModProjectile
    {
        
        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 60;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            Projectile.scale = 2.0f;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f);
            }
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.5f);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            // Pega a textura do projétil
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            // Calcula o CENTRO da imagem (Largura / 2, Altura / 2)
            Vector2 origin = texture.Size() / 2f;

            // Desenha a imagem centralizada na posição do projétil
            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition, // Posição na tela
                null,
                lightColor,
                Projectile.rotation, // Rotação correta
                origin,              // Pivô no centro!
                Projectile.scale,
                SpriteEffects.None,
                0
            );

            return false;
        }
    }
}