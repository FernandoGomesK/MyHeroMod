using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Gearshift; // Certifique-se que o namespace está certo

namespace MyHeroMod.content.System
{
    public class GearshiftGlobalProjectile : GlobalProjectile
    {
        // Permite que cada projétil tenha dados individuais
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile projectile)
        {
            // Para ter "sombra", o projétil precisa lembrar onde esteve (Cache)
            // Se o projétil não tiver rastro configurado, forçamos um rastro de 10 frames
            if (projectile.friendly && projectile.owner == Main.myPlayer)
            {
                if (ProjectileID.Sets.TrailCacheLength[projectile.type] < 10)
                {
                    ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
                    ProjectileID.Sets.TrailingMode[projectile.type] = 0;
                }
            }
        }

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            // 1. Verificações de Segurança
            Player player = Main.player[projectile.owner];
            
            // Só desenha se o dono for o jogador E se o Gearshift estiver ativo
            if (projectile.friendly && player.active && !player.dead && player.GetModPlayer<GearshiftPlayer>().isGearshiftBuffActive)
            {
                // Pega a textura do projétil
                Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
                Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
                Vector2 origin = frame.Size() / 2f;

                // 2. Loop para desenhar as "Sombras" (Afterimages)
                // Vamos desenhar as posições antigas (oldPos)
                for (int i = 0; i < projectile.oldPos.Length; i++)
                {
                    // Posição ajustada para a tela
                    Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + origin + new Vector2(0f, projectile.gfxOffY);
                    
                    // Cor: Ciano (Gearshift) com transparência gradual
                    // Quanto maior o 'i' (mais antigo), mais transparente fica
                    Color color = Color.Cyan * ((projectile.oldPos.Length - i) / (float)projectile.oldPos.Length);
                    
                    // Deixa um pouco mais transparente geral (0.6f)
                    color *= 0.6f; 

                    Main.EntitySpriteDraw(
                        texture, 
                        drawPos, 
                        frame, 
                        color, 
                        projectile.rotation, 
                        origin, 
                        projectile.scale, 
                        SpriteEffects.None, 
                        0
                    );
                }
            }

            // Retorna true para deixar o jogo desenhar o projétil original normal por cima das sombras
            return true;
        }

        public override void PostAI(Projectile projectile)
        {
            // Opcional: Adicionar partículas elétricas enquanto viaja
            Player player = Main.player[projectile.owner];
            if (projectile.friendly && player.GetModPlayer<GearshiftPlayer>().isGearshiftBuffActive)
            {
                if (Main.rand.NextBool(5)) // 20% de chance por frame
                {
                    Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Electric, 0, 0, 100, Color.Cyan, 0.5f);
                    d.velocity *= 0.2f;
                    d.noGravity = true;
                }
            }
        }
    }
}         