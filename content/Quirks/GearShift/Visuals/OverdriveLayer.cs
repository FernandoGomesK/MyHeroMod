using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;

namespace MyHeroMod.content.Quirks.Gearshift.Visuals
{
    public class OverdriveLayer : PlayerDrawLayer
    {
        // Define que a camada é desenhada DEPOIS do braço do jogador
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            var modPlayer = drawInfo.drawPlayer.GetModPlayer<GearshiftPlayer>();

            // Só aparece se estivermos no modo "GearActivation" (Carregando)
            // E se o timer for maior que 0
            return modPlayer.GearActivation && modPlayer.ActivationTimer > 0;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (!ModContent.HasAsset("MyHeroMod/Assets/GearshiftTransmission")) return;

            Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/GearshiftTransmission").Value;
            var modPlayer = drawInfo.drawPlayer.GetModPlayer<GearshiftPlayer>();

            // Configuração da Animação
            int totalframes = 4; // Quantos frames tem a sua imagem (spritesheet)
            int timer = modPlayer.ActivationTimer;
            int maxTime = modPlayer.ActivationMaxTime;

            // Calcula qual frame mostrar baseado no progresso do carregamento
            int frameDuration = maxTime / totalframes;
            if (frameDuration < 1) frameDuration = 1;

            int currentFrame = (timer / frameDuration);
            if (currentFrame >= totalframes) currentFrame = totalframes - 1;

            // Recorte da Textura
            int frameHeight = texture.Height / totalframes;
            Rectangle sourceRectangle = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

            // Posição Ajustada (Centraliza no Player)
            Vector2 position = drawInfo.Center - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y + drawInfo.drawPlayer.gfxOffY);

            // Efeito de Luz
            Lighting.AddLight(drawInfo.Center, Color.Cyan.ToVector3() * 1.5f);

            // Dados de Desenho
            DrawData drawData = new DrawData(
                texture,
                position,   
                sourceRectangle,
                Color.White,
                drawInfo.drawPlayer.fullRotation,
                new Vector2(texture.Width / 2f, frameHeight / 2f), // Origem no centro
                1f,
                drawInfo.playerEffect,
                0
            );

            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}