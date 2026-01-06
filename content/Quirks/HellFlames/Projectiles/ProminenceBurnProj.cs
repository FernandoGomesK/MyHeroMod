using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.HellFlames.Projectiles
{
    public class ProminenceBurnProj : ModProjectile
    {
        private const float MaxDistance = 2000f;

        public override void SetDefaults()
        {
            Projectile.width = 30; // Hitbox mais fina para representar o feixe
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300; 
            Projectile.hide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }
    // 1. Atualizar Mira
            if (Projectile.owner == Main.myPlayer)
        {
            Vector2 diff = Main.MouseWorld - player.MountedCenter;
            diff.Normalize();
            Projectile.velocity = diff;
        
        // Vira o jogador para o lado do mouse
            player.ChangeDir(Main.MouseWorld.X > player.MountedCenter.X ? 1 : -1);
            Projectile.direction = player.direction;
            Projectile.netUpdate = true;
        }

    // 2. POSICIONAMENTO FINAL (Sem empurrar para frente)
    // Usamos apenas (0, -6f) para subir a origem do umbigo para o peito.
    // Como removemos o "velocity * 40f", ele não vai mais flutuar separado.
    Projectile.Center = player.MountedCenter + new Vector2(0, -4f);

    // 3. Rotação
    Projectile.rotation = Projectile.velocity.ToRotation();

    // 4. Animação do Braço
    player.heldProj = Projectile.whoAmI;
    player.itemTime = 2;
    player.itemAnimation = 2;
    
    // Calcula a rotação do braço para apontar junto com o laser
    player.itemRotation = (Projectile.velocity * player.direction).ToRotation();

    // Reduz velocidade
    player.velocity *= 0.5f; 

    // 5. Partículas
    if (Main.rand.NextBool(3))
    {
        // Gera partículas um pouco à frente para não tapar o rosto
        Vector2 offset = Projectile.velocity * Main.rand.NextFloat(10f, 50f); 
        Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Torch, Projectile.velocity * 8f);
        d.noGravity = true;
        d.scale = 4.5f;
    }
}

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            return Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(), 
                targetHitbox.Size(), 
                Projectile.Center, 
                Projectile.Center + Projectile.velocity * MaxDistance, 
                Projectile.width, 
                ref point
            );
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/content/Quirks/HellFlames/Projectiles/ProminenceBurnProj").Value;

            Vector2 unit = Projectile.velocity;
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            float rotation = Projectile.rotation;

    // IMPORTANTE: Se a textura é horizontal, usamos texture.Width para pular os pedaços
    // Se usar Height aqui, vai desenhar tudo esmagado.
            for (float i = 0; i < MaxDistance; i += texture.Width) 
            {
                spriteBatch.Draw(
                 texture, 
                drawPos + unit * i, 
                null, 
                Color.Orange * 0.8f, 
                rotation, // Sem +1.57f, pois já está deitado
                new Vector2(0, texture.Height / 2f), // <--- O SEGREDO: Origem no Meio-Esquerda (0, Metade da Altura)
                new Vector2(1f, 1f), 
                SpriteEffects.None,                     0f
        );
    }
    return false; 
}
    }
}