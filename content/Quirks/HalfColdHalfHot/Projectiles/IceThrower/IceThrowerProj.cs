using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.IceThrower
{
    public class IceThrowerProj : ModProjectile
    {
        public override void SetDefaults()
        {
            // Tamanho da Hitbox (área que dá dano)
            Projectile.width = 60; // É gordinho para acertar fácil
            Projectile.height = 60;
            
            // Comportamento
            Projectile.friendly = true; // Acerta inimigos
            Projectile.hostile = false; 
            Projectile.penetrate = -1; // Atravessa infinitos inimigos
            Projectile.timeLeft = 60; // Dura 1 segundo (alcance médio)
            
            // Visual
            Projectile.alpha = 255; // Começa invisível (só veremos as partículas)
            Projectile.ignoreWater = false; // Apaga na água (comportamento clássico)
            Projectile.tileCollide = true; // Bate nas paredes
            
            // IMUNIDADE (O Segredo do Dano)
            // Isso impede que o mesmo foguinho bata 60 vezes por segundo no mesmo bicho.
            // Mas permite que VÁRIOS foguinhos batam no mesmo bicho em sequência.
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 200;
        }

        public override void AI()
        {
            // 1. Geração de Partículas (O Visual Real)
            // Gera pó de fogo no centro do projétil
            for (int i = 0; i < 2; i++) // Pode aumentar para 3 se quiser mais denso
            {
                int dustIndex = Dust.NewDust(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height, 
                    DustID.SnowflakeIce, // ID do fogo padrão (6)
                    Projectile.velocity.X * 0.2f, 
                    Projectile.velocity.Y * 0.2f, 
                    100, 
                    default, 
                    2f // Tamanho grande
                );
                
                Main.dust[dustIndex].noGravity = true; // Fogo flutua
                Main.dust[dustIndex].velocity *= 1.5f; // Fogo se expande um pouco
                Main.dust[dustIndex].velocity += Projectile.velocity * 0.5f; // Segue o tiro
            }

            // 2. Crescimento da Hitbox (Opcional, estilo Overhaul)
            // Faz o fogo começar pequeno e ficar grande no final
            /*
            if (Projectile.width < 100) 
            {
                Projectile.width += 2;
                Projectile.height += 2;
            }
            */

            // 3. Físicas do Fogo
            // Desacelera um pouco conforme viaja (resistência do ar)
            Projectile.velocity *= 0.95f; 
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Aplica o Debuff clássico de fogo
            target.AddBuff(BuffID.OnFire, 180); // 3 segundos de fogo
        }
    }
}