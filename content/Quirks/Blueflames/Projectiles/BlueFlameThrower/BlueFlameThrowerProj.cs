using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Blueflames.Projectiles.BlueFlameThrower
{
    public class BlueFlameThrowerProj : ModProjectile
    {
        public override void SetDefaults()
        {
            
            Projectile.width = 60; 
            Projectile.height = 60;
            
            // Comportamento
            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 60;
            
            
            Projectile.alpha = 255; 
            Projectile.ignoreWater = false; 
            Projectile.tileCollide = true; 
            
            
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10; 
        }

        public override void AI()
        {
            
            for (int i = 0; i < 2; i++) 
            {
                int dustIndex = Dust.NewDust(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height, 
                    DustID.BlueTorch, 
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
            // Aplica o Debuff clássico de fogo
            target.AddBuff(BuffID.OnFire, 180); // 3 segundos de fogo
        }
    }
}