using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Projectiles
{
    public class WeakFireProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames; // Usa textura invisível ou padrão, pois faremos com Dust

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = false; // Fogo apaga na água
            Projectile.DamageType = DamageClass.Magic; // Ou Generic/Melee dependendo do seu mod
            Projectile.penetrate = 3; // Atravessa 3 inimigos (comum para lança-chamas)
            Projectile.timeLeft = 45; // Dura pouco tempo = Curto alcance
            Projectile.extraUpdates = 2; // Move-se mais rápido visualmente
            Projectile.alpha = 255; // Invisível (o desenho será feito pelas partículas)
        }

        public override void AI()
        {
            // Efeito visual de Fogo
            // Se for Blueflames, mude DustID.Torch para DustID.BlueTorch
            int dustType = DustID.Torch; 
            
            // Cria a partícula
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f);
            Main.dust[dust].noGravity = true; // Fogo flutua
            Main.dust[dust].velocity *= 0.3f; // Desacelera um pouco
            Main.dust[dust].velocity += Projectile.velocity * 0.5f; // Segue o tiro
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180); // Causa fogo por 3 segundos
        }
    }
}