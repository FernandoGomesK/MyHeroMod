using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class BigDarkShadowFrontHandProj : ModProjectile
    {
        
        

        public override void SetDefaults()
        {
            Projectile.width = 50; 
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            // 1. Condição de Morte
            if (player.dead || !player.active || !darkPlayer.isCBOArmsOn)
            {
                Projectile.Kill();
                return;
            }

            if (darkPlayer.isFrontHandAttacking)
            {
                Projectile.alpha = 255;
                return;
            }
            else
            {
                Projectile.alpha = 0; 
            }

            Projectile.timeLeft = 2;

            // 2. Lógica de Flutuação (Na FRENTE do jogador)
            // +30f empurra para a frente (dependendo da direção), +10f empurra para baixo (altura da cintura/peito)
            Vector2 hoverPosition = player.Center + new Vector2(+30f * player.direction, -20f);

            Vector2 direction = hoverPosition - Projectile.Center;
            float distance = direction.Length();

            // Movimentação elástica, um pouco mais rápida e responsiva que o corpo
            if (distance > 10f)
            {
                direction.Normalize();
                Projectile.velocity = (Projectile.velocity * 10f + direction * 8f) / 11f; 
            }
            else
            {
                Projectile.velocity *= 0.8f; 
            }

            Projectile.spriteDirection = player.direction;

            // 3. Cordão Umbral Customizado (#180021)
            // Convertendo a sua cor Hex para RGB (R: 24, G: 0, B: 33)
           Vector2 cordStartPos = player.Center; 

            // Procura todos os projéteis ativos para encontrar o Corpo do Dark Shadow
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                
                if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<DarkShadowBodyProj>())
                {
                    
                    cordStartPos = p.Center + new Vector2(0f, 10f); 
                    
                    break; 
                }
            }

            Color shadowColor = new Color(24, 0, 33);
            
            for (int i = 0; i < 3; i++)
            {
                
                Vector2 cordPos = Vector2.Lerp(cordStartPos, Projectile.Center, Main.rand.NextFloat());
                
                cordPos += Main.rand.NextVector2Circular(4f, 4f); 
                
                Dust dust = Dust.NewDustPerfect(cordPos, DustID.WhiteTorch, Vector2.Zero, 0, shadowColor);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(0.8f, 1.5f);
                dust.velocity = Main.rand.NextVector2Circular(0.2f, 0.2f);
            }
        }
    }
}