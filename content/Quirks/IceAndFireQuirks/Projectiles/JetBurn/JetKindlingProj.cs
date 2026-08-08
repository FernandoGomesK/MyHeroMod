using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.JetBurn
{
    public class JetKindlingProj : ModProjectile
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
            Projectile.tileCollide = true; 
            
            
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            int fireColor = DustID.Torch; 
            
            
            if (transPlayer.HasActiveQuirk(QuirkType.Blueflame) && transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                fireColor = DustID.BlueTorch; 
            }




            for (int i = 0; i < 4; i++) // Pode aumentar para 3 se quiser mais denso
            {
                int dustIndex = Dust.NewDust(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height, 
                    fireColor, // ID do fogo padrão (6)
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

            
            Projectile.velocity *= 0.98f; 
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Aplica o Debuff clássico de fogo
            target.AddBuff(BuffID.OnFire, 180); // 3 segundos de fogo
        }
    }
}