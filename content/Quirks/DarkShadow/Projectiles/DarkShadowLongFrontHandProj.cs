using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.DarkShadow;

namespace MyHeroMod.content.Quirks.DarkShadow.Projectiles
{
    public class DarkShadowLongFrontHandProj : ModProjectile
    {
        // COLOQUE AQUI O NOME DO SEU SPRITE MAIOR
        // public override string Texture => "MyHeroMod/Assets/Projectiles/DarkShadowLongFrontHand"; 

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false; // Como é um grapple sombrio, atravessa paredes
            Projectile.penetrate = -1; // Atravessa inimigos
            Projectile.timeLeft = 300; // Tempo de segurança limite (5 segundos)
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            
            
            

            // 2. Encontra o Corpo do Dark Shadow para usar como âncora
            Vector2 bodyCenter = player.Center; 
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<DarkShadowBodyProj>())
                {
                    bodyCenter = p.Center + new Vector2(0f, 10f); // A mesma âncora que criámos antes!
                    break;
                }
            }

            // 3. ESTADO 0: INDO PARA A FRENTE
            if (Projectile.ai[1] == 0)
            {
                // Se estiver muito longe do corpo (Ex: 400 pixels), muda para o estado de voltar
                if (Vector2.Distance(Projectile.Center, bodyCenter) > 400f)
                {
                    Projectile.ai[1] = 1; // Muda o estado para Voltar
                }
            }
            // 4. ESTADO 1: VOLTANDO (RETRACTING)
            else if (Projectile.ai[1] == 1)
            {
                Vector2 returnDirection = bodyCenter - Projectile.Center;
                float distanceToBody = returnDirection.Length();

                // Se chegou perto o suficiente do corpo, o ataque terminou!
                if (distanceToBody < 20f)
                {
                    Projectile.Kill();
                    return;
                }

                // Puxa a mão de volta com muita velocidade
                returnDirection.Normalize();
                Projectile.velocity = returnDirection * 20f; 
            }

            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;

            // 5. CORDÃO UMBRAL DO ATAQUE (Com a sua cor #180021)
            Color shadowColor = new Color(24, 0, 33);
            for (int i = 0; i < 4; i++) // Um pouco mais denso durante o ataque
            {
                Vector2 cordPos = Vector2.Lerp(bodyCenter, Projectile.Center, Main.rand.NextFloat());
                cordPos += Main.rand.NextVector2Circular(5f, 5f); 
                
                Dust dust = Dust.NewDustPerfect(cordPos, DustID.WhiteTorch, Vector2.Zero, 0, shadowColor);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(1.0f, 1.8f);
                if (Projectile.ai[0] == 1) dust.customData = 0; // Se for a mão de trás, desenha a poeira atrás!
            }
        }
    }
}