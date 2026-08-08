using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.ProminenceBurn
{
    public class ProminenceBurnFire : ModProjectile
    {
        public override void SetDefaults()
        {
            // HITBOX GIGANTE
            Projectile.width = 120; // 2x maior que o JetBurn
            Projectile.height = 120;
            
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1; // Infinito
            
            // Duração média (o fogo viaja longe antes de sumir)
            Projectile.timeLeft = 80; 
            
            Projectile.alpha = 255; // Invisível (só partículas)
            Projectile.tileCollide = false; // O Prominence Burn atravessa paredes (OPCIONAL)
            Projectile.ignoreWater = true;  // Fogo tão forte que queima na água
            
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10; // Hit muito rápido
        }

        public override void AI()
        {
            // GERADOR DE PARTÍCULAS MASSIVO
            // Gera 5 a 8 partículas por frame para preencher o espaço gigante
            Player player = Main.player[Projectile.owner];

            var transPlayer = player.GetModPlayer<TransformationPlayer>();

             int fireColor = DustID.SolarFlare; 
             int fireColor2 = DustID.Torch;
            
            
            if (transPlayer.HasActiveQuirk(QuirkType.Blueflame) && transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                fireColor = DustID.BlueTorch; 
                fireColor2 = DustID.IceTorch;
            }

            for (int i = 0; i < 6; i++) 
            {
                
                // Espalha as partículas aleatoriamente dentro da hitbox gigante
                Vector2 dustPos = Projectile.position + new Vector2(Main.rand.Next(Projectile.width), Main.rand.Next(Projectile.height));
                
                int dustID = fireColor2;
                // Chance de gerar partículas de fumaça ou fogo mais escuro para textura
                if (Main.rand.NextBool(3)) dustID = fireColor; 

                int idx = Dust.NewDust(dustPos, 0, 0, dustID, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 100, default, 1f);
                
                Main.dust[idx].noGravity = true;
                Main.dust[idx].scale = Main.rand.NextFloat(3f, 6f); // PARTÍCULAS GIGANTES
                Main.dust[idx].velocity *= 2f; // Partículas se movem rápido
                Main.dust[idx].velocity += Projectile.velocity * 0.8f;
            }

            // O projétil cresce um pouco enquanto viaja
            Projectile.scale += 0.05f;
            Projectile.velocity *= 0.98f; // Desacelera levemente
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 300); // Hellfire (Fogo mais forte do jogo)
        }
    }
}