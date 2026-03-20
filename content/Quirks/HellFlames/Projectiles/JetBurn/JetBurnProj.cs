using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.HellFlames.Projectiles.JetBurn
{
    public class JetBurnProj : ModProjectile
    {
        public override void SetDefaults()
        {
            
            Projectile.width = 60; 
            Projectile.height = 60;
            
            
            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 60; 
            
            
            Projectile.alpha = 255; 
            Projectile.ignoreWater = false; 
            Projectile.tileCollide = true; 
            
            
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10; // Hit a cada 1/6 de segundo por partícula
        }

        public override void AI()
        {
            
            
            for (int i = 0; i < 2; i++) // Pode aumentar para 3 se quiser mais denso
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
                    3f // Tamanho grande
                );
                
                Main.dust[dustIndex].noGravity = true; // Fogo flutua
                Main.dust[dustIndex].velocity *= 1.5f; // Fogo se expande um pouco
                Main.dust[dustIndex].velocity += Projectile.velocity * 0.5f; // Segue o tiro
            }

            
            Projectile.velocity *= 0.95f; 
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        
            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}