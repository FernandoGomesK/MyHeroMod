using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhip
{
    // Renomeei para BlackWhipProjectile para evitar conflitos
    public class BlackWhipProjectileUnique : ModProjectile
    {
        public override void SetDefaults()
        {
            // Clona o comportamento do gancho de ametista
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            Projectile.width = 18;
            Projectile.height = 18;
            // Se quiser mudar a cor do mapa, use: Projectile.DrawOffsetX ...
        }

        // --- CONFIGURAÇÕES DO GANCHO ---
        
        // Alcance em pixels (400 é curto, 800 é médio, 1200 é longo)
        // Blackwhip costuma ter bom alcance
        public override float GrappleRange() => 600f; 

        // Quantos chicotes podem sair ao mesmo tempo? (Blackwhip pode ter vários!)
        public override void NumGrappleHooks(Player player, ref int numHooks) => numHooks = 2;

        // Velocidade de recuo (quão rápido ele te puxa)
        public override void GrappleRetreatSpeed(Player player, ref float speed) => speed = 18f;

        // --- DESENHO DA CORRENTE (O RAIO PRETO) ---
        public override bool PreDraw(ref Color lightColor)
        {
            // Caminho atualizado para a pasta onde você colocou o arquivo
            // IMPORTANTE: O arquivo DEVE ser .png, não .aseprite
            string chainTexturePath = "MyHeroMod/content/Quirks/BlackWhip/Projectiles/BlackWhip/BlackWhipChain";

            // Segurança: Se a textura não existir, não desenha a corrente para não crashar
            if (!ModContent.HasAsset(chainTexturePath)) return false;

            Texture2D texture = ModContent.Request<Texture2D>(chainTexturePath).Value;

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float textureHeight = texture.Height;

            Vector2 vectorToPlayer = mountedCenter - position;
            float rotation = vectorToPlayer.ToRotation() - 1.57f;
            bool chainConnected = true;

            // Loop para desenhar os elos
            while (chainConnected)
            {
                float length = vectorToPlayer.Length();
                // Se estiver muito perto do player, para de desenhar
                if (length < textureHeight + 1)
                {
                    chainConnected = false;
                }
                else
                {
                    Vector2 nextLink = vectorToPlayer;
                    nextLink.Normalize();
                    position += nextLink * textureHeight;
                    vectorToPlayer = mountedCenter - position;
                    
                    // Cor da corrente (Pode forçar preto se quiser: Color.Black)
                    Color color = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    
                    Main.EntitySpriteDraw(texture, position - Main.screenPosition, sourceRectangle, color, rotation, origin, 1f, SpriteEffects.None, 0);
                }
            }
            return false; // Retorna false para o jogo não desenhar a corrente padrão, só a nossa
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            Lighting.AddLight(Projectile.Center, 0.2f, 0.8f, 0.6f); 
            
        }
    }
}