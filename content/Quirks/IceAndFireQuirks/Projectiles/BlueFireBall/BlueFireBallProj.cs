using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.BlueFireBall
{
    
    public class BlueFireBallProj : ModProjectile
    {
        
        public string dustcolor = "Torch";
        public override void SetDefaults()
        {
            Projectile.width = 60; 
            Projectile.height = 60;
            
            // Comportamento
            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 120; 
            
            
            Projectile.alpha = 255; 
            Projectile.ignoreWater = false; 
            Projectile.tileCollide = false; 
            
            
            
            
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10; // Hit a cada 1/6 de segundo por partícula
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 4.0f);
                Main.dust[idx].noGravity = true;
                Main.dust[idx].velocity *= 3f;

                Main.dust[idx].velocity += Projectile.velocity * 0.5f;
            
            } 
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 4.5f);
            }
            for (int i = 0; i < 5; i++)
            {

                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ash, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f );
            }
        }   
       public override void AI()
        {
            // 1. Geração de Partículas (O Visual Real)
            // Gera pó de fogo no centro do projétil
            for (int i = 0; i < 4; i++) // Pode aumentar para 3 se quiser mais denso
            {
                int dustIndex = Dust.NewDust(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height, 
                    DustID.Torch, // ID do fogo padrão (6)
                    Projectile.velocity.X * 0.2f, 
                    Projectile.velocity.Y * 0.2f, 
                    100, 
                    default, 
                    5f // Tamanho grande
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
            Projectile.velocity *= 0.99f; 
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Aplica o Debuff clássico de fogo
            target.AddBuff(BuffID.OnFire, 180); // 3 segundos de fogo
        }

        
    }

    
}