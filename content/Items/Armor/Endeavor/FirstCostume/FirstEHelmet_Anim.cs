using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.Items.Armor;
using MyHeroMod.content.Items.Armor.Endeavor.FirstCostume;

namespace MyHeroMod.content.System
{
    public class EndeavorMaskGlow : PlayerDrawLayer
    {
        // Define que vai desenhar DEPOIS da cabeça (por cima)
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            
            return drawInfo.drawPlayer.head == ModContent.GetInstance<FirstEHelmet>().Item.headSlot 
                   && !drawInfo.drawPlayer.dead;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;

            // CARREGUE A TEXTURA
            // Certifique-se que o caminho está correto!
            Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/content/Items/Armor/Endeavor/FirstCostume/EndeavorMask_Anim").Value;

            // LÓGICA DE ANIMAÇÃO
            int frameCount = 4; // Total de quadros na imagem
            int frameSpeed = 5; // Velocidade (quanto menor, mais rápido). 5 é rápido, 10 é médio.
            
            // Calcula qual quadro mostrar baseado no tempo do jogo
            int currentFrame = (int)((Main.GameUpdateCount / frameSpeed) % frameCount);

            // TAMANHO DO QUADRO
            // Se você seguiu minha recomendação, frameHeight será 56.
            int frameHeight = texture.Height / frameCount; 

            // Recorta o pedaço certo da imagem
            Rectangle sourceRect = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

            // POSIÇÃO
            // Essa fórmula alinha o centro do seu sprite 40x56 com o centro do sprite do jogador
            Vector2 drawPos = (drawInfo.Position - Main.screenPosition) + new Vector2(player.width / 2, player.height - player.bodyFrame.Height + 4) + drawInfo.headVect;
            Vector2 ajuste = new Vector2(-20f, 0f); 
            
            drawPos += ajuste;
            // Cria o dado de desenho
            DrawData drawData = new DrawData(
                texture,
                drawPos.Floor(), // .Floor() evita tremedeira
                sourceRect,
                Color.White, // COR BRANCA = BRILHO MÁXIMO (Fogo)
                player.headRotation,
                drawInfo.headVect, // Usa o mesmo pivô da cabeça original
                1f,
                drawInfo.playerEffect, // Garante que vire para esquerda/direita junto com o player
                0
            );

            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}