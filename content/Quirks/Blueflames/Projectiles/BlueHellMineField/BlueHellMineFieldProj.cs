using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Blueflames.Projectiles.BlueHellMineField
{

    public class BlueHellMineFieldProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 100; // Espinho alto
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1; // Atravessa inimigos infinitos
            Projectile.timeLeft = 60;  // Dura 1 segundo
            Projectile.tileCollide = false; // Não colide, pois nasce DENTRO do chão
            Projectile.ignoreWater = true;
            Projectile.alpha = 255; // Começa invisível (opcional se tiver sprite)
        }

        public override void AI()
        {
            // EFEITO DE "NASCER" DO CHÃO
            // Nos primeiros frames, ele sobe rápido para parecer que brotou
            if (Projectile.ai[0] < 10)
            {
                Projectile.position.Y -= 4f; // Sobe 4 pixels por frame
                Projectile.alpha -= 25; // Aparece gradualmente
                if (Projectile.alpha < 0) Projectile.alpha = 0;
                Projectile.ai[0]++;
            }

            // GERAÇÃO DE PARTÍCULAS (GELO)
            if (Main.rand.NextBool(1))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, 0, 0, 100, default, 3f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.5f;
            }
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Flare_Blue, 0, 0, 100, default, 3f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.5f;
            }
            for (int i = 0; i < 2; i++) 
    {
        // 1. Definição da Posição de Spawn (O Segredo)
        // Pega a posição X normal
        // Mas no Y, pegamos a parte de BAIXO (Bottom) e subimos só um pouquinho (-10)
        Vector2 spawnPos = new Vector2(Projectile.position.X, Projectile.Bottom.Y - 10);
        
        // 2. Altura da área de spawn
        // Usamos '10' em vez de 'Projectile.height'. 
        // Isso obriga o fogo a nascer apenas na fatia de baixo.
        int heightArea = 10; 

        if (Main.rand.NextBool(2))
        {
            // FOGO AZUL
            int dust = Dust.NewDust(spawnPos, Projectile.width, heightArea, DustID.BlueTorch, 0, 0, 100, default, 3f);
            
            Main.dust[dust].noGravity = true; // Flutua
            
            // VELOCIDADE PARA CIMA
            // Define uma velocidade Y negativa forte (sobe rápido)
            Main.dust[dust].velocity.Y = -Main.rand.NextFloat(4f, 8f); 
            
            // Reduz o espalhamento lateral para parecer uma coluna
            Main.dust[dust].velocity.X *= 0.5f; 
            
            // Adiciona a velocidade do próprio projétil para acompanhar a subida
            Main.dust[dust].velocity += Projectile.velocity * 0.5f;
        }}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180); // Congela
        }
    }
}
