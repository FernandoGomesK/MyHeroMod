using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.DangerSense; // Importante para achar o Player

namespace MyHeroMod.content.Quirks.OFA9th.Visuals
{
    public class DangerSenseEffect : PlayerDrawLayer
    {
        // Define que desenha depois da cabeça/capacete
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            var modPlayer = drawInfo.drawPlayer.GetModPlayer<DangerSensePlayer>();
            
            
            return modPlayer.VisualTimer > 0 && !drawInfo.drawPlayer.dead;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (!ModContent.HasAsset("MyHeroMod/Assets/DangerSenseEffect")) return;

            Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/DangerSenseEffect").Value;
            var modPlayer = drawInfo.drawPlayer.GetModPlayer<DangerSensePlayer>();

            // CONFIGURAÇÃO DA ANIMAÇÃO
            int totalFrames = 8; // Quantos frames tem sua imagem
            int timer = modPlayer.VisualMaxTimer - modPlayer.VisualTimer; // Inverte para contar de 0 pra cima
            int frameDuration = modPlayer.VisualMaxTimer / totalFrames;
            
            if (frameDuration < 1) frameDuration = 1;

            int currentFrame = timer / frameDuration;
            if (currentFrame >= totalFrames) currentFrame = totalFrames - 1;

            // Recorte do Sprite Sheet
            int frameHeight = texture.Height / totalFrames;
            Rectangle sourceRectangle = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

            // POSIÇÃO (Acima da Cabeça)
            // drawInfo.Center é o umbigo do player.
            // Subtraímos Y para subir (Terraria Y cresce pra baixo).
            Vector2 drawPos = drawInfo.Center - Main.screenPosition;
            drawPos.Y -= 25f; // Sobe 40 pixels (ajuste conforme necessário)
            drawPos.Y += drawInfo.drawPlayer.gfxOffY; // Compensa movimento de montaria/pulo

            // Cor e Luz
            Lighting.AddLight(drawInfo.Center, Color.Cyan.ToVector3() * 0.8f);

            DrawData drawData = new DrawData(
                texture,
                drawPos,
                sourceRectangle,
                Color.White, // Use drawInfo.colorArmorBody se quiser que escureça à noite
                0f, // Rotação
                new Vector2(texture.Width / 2f, frameHeight / 2f), // Origem (Centro da imagem)
                1f, // Escala
                drawInfo.playerEffect, // Espelhar se o player virar
                0
            );

            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}