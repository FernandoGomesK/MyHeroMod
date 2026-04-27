using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.HellSpider
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
            
            
            Projectile.alpha = 255; // Começa invisível (só veremos as partículas)
            Projectile.ignoreWater = false; // Apaga na água (comportamento clássico)
            Projectile.tileCollide = true; 
            
            
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10; // Hit a cada 1/6 de segundo por partícula
            Projectile.hide = true;

            Projectile.extraUpdates = 2; // Move mais suave
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            int fireColor = DustID.Torch; 
            
            
            if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames) && transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                fireColor = DustID.BlueTorch; 
            }

             

            // 1. Geração de Partículas (O Visual Real)
            // Gera pó de fogo no centro do projétil
            for (int i = 0; i < 2; i++) // Pode aumentar para 3 se quiser mais denso
            {

                Vector2 position = Projectile.position - Projectile.velocity * (float)i / 2;
                int dustIndex = Dust.NewDust(
                    position,
                    Projectile.width, 
                    Projectile.height, 
                    fireColor,
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