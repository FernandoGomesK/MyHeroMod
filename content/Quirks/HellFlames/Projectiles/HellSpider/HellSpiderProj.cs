using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.HellFlames.Projectiles.HellSpider
{
    public class HellSpiderProj : ModProjectile
    {
        public override void SetDefaults()
        {
            // Tamanho da Hitbox (área que dá dano)
            Projectile.width = 14; // É gordinho para acertar fácil
            Projectile.height = 14;
            
            // Comportamento
            Projectile.friendly = true; // Acerta inimigos
            Projectile.hostile = false; 
            Projectile.penetrate = -1; // Atravessa infinitos inimigos
            Projectile.timeLeft = 600; //
            
            // Visual
            Projectile.alpha = 255; // Começa invisível (só veremos as partículas)
            Projectile.ignoreWater = false; // Apaga na água (comportamento clássico)
            Projectile.tileCollide = true; // Bate nas paredes
            
            // IMUNIDADE (O Segredo do Dano)
            // Isso impede que o mesmo foguinho bata 60 vezes por segundo no mesmo bicho.
            // Mas permite que VÁRIOS foguinhos batam no mesmo bicho em sequência.
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10; // Hit a cada 1/6 de segundo por partícula

            Projectile.extraUpdates = 2; // Move mais suave
        }

        public override void AI()
        {
            // 1. Geração de Partículas (O Visual Real)
            // Gera pó de fogo no centro do projétil
            for (int i = 0; i < 2; i++) // Pode aumentar para 3 se quiser mais denso
            {

                Vector2 position = Projectile.position - Projectile.velocity * (float)i / 2;
                int dustIndex = Dust.NewDust(
                    position,
                    Projectile.width, 
                    Projectile.height, 
                    DustID.Torch,
                    0, 0, 
                    
                    100, 
                    default, 
                    1.2f // Tamanho grande
                );
                
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.1f;
            }
        }
    }
}