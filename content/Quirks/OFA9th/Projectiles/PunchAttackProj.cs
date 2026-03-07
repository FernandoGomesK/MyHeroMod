using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Audio;
namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{
    public class PunchAttackProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/PunchAttackProj";
    
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 0; // AI customizada
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee; // Geralmente socos são Melee, mudei de Generic
            
            // Estilo Zenith: Atravessa inimigos
            Projectile.penetrate = 1; // -1 = Infinito (Atravessa tudo)
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10; // Bate a cada 10 frames no mesmo inimigo

            // Alcance Controlado
            Projectile.timeLeft = 20; // Dura apenas 0.3 segundos (Alcance curto/médio)
            // Se quiser mais longe, aumente para 30 ou 40.
            
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false; // Zenith atravessa paredes. Se quiser que bata na parede, mude para true.
            Projectile.alpha = 0;
        }

        public override void OnKill(int timeLeft)
        {
            // Efeito visual ao sumir
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SteampunkSteam, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.0f);
            }
        }

        public override void AI()
        {

            int style = (int)Projectile.ai[0];
            
            // Define o caminho base. 
            // IMPORTANTE: Certifique-se que essas imagens existem ou altere os nomes abaixo!
            string texturePath = "MyHeroMod/Assets/Projectiles/PunchAttackProj";
                                 

            // Se for estilo 1 ou 2, tenta carregar a variante. Se não existir, usa a padrão.
            if (style == 1 && ModContent.HasAsset(texturePath + "2")) 
                texturePath += "2"; 
            else if (style == 2 && ModContent.HasAsset(texturePath + "3")) 
                texturePath += "3";

            // Carrega a textura escolhida
            Texture2D texture = ModContent.Request<Texture2D>(texturePath).Value;
            // Mantemos a rotação apontando para a velocidade
            Projectile.rotation = Projectile.velocity.ToRotation();
            
            
            

            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SteampunkSteam, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 0.2f);
            }

            if (Projectile.timeLeft < 10)
            {
                Projectile.alpha += 25;
            }
        }

    
        public override bool PreDraw(ref Color lightColor)
        {
            // Pega a textura do projétil
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Define o efeito de espelhamento
            SpriteEffects spriteEffects = SpriteEffects.None;

            // Se o projétil estiver indo para a esquerda (Velocidade X negativa)
            // E o sprite estiver girado, precisamos espelhar verticalmente para ele não ficar de ponta cabeça
            if (Projectile.velocity.X < 0)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }

            // Calcula a origem (centro da imagem)
            Vector2 origin = texture.Size() / 2f;

            // Desenha manualmente
            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation,
                origin,
                Projectile.scale,
                spriteEffects,
                0
            );

            return false; // Retorna FALSE para o jogo não desenhar o padrão (que ficaria errado)
        }
    }
}