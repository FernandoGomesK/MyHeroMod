using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.HeavenPiercingWall
{
    public class IceSpikeProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 60; // Espinho alto
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
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, 0, 0, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.5f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 180); // Congela
        }
    }
}