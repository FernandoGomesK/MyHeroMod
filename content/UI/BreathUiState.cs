using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using MyHeroMod.content.Quirks.Overclock; // Para ler o seu OverclockPlayer
using MyHeroMod.content.System; // Para ler o TransformationPlayer

namespace MyHeroMod.content.UI
{
    public class BreathUIState : UIState
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Pega o jogador local (a sua tela)
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var overPlayer = player.GetModPlayer<OverclockPlayer>();

            // Só desenha a barra se ele tiver a Quirk do Overclock equipada
            if (!transPlayer.HasActiveQuirk(QuirkType.Overclock))
                return;

           
            Texture2D barFrame = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/BreathBarFrame").Value;
            Texture2D barFill = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/BreathBarFill").Value;

            // Calcula a posição na tela (Vamos colocar no meio, embaixo do personagem)
            Vector2 screenCenter = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 2f);
            Vector2 drawPos = screenCenter + new Vector2(-barFrame.Width / 2f, 60f); // 60 pixels abaixo do centro

            // A MÁGICA: Calcula a porcentagem do fôlego (de 0.0 a 1.0)
            float quotient = 1f;
            if (overPlayer.maxBreath > 0)
            {
                quotient = (float)overPlayer.currentBreath / overPlayer.maxBreath;
            }
            
            // Trava o valor para não bugar o desenho
            quotient = MathHelper.Clamp(quotient, 0f, 1f);

            // Desenha a Borda da Barra
            spriteBatch.Draw(barFrame, drawPos, Color.White);

            // Desenha o Preenchimento (cortando a imagem dependendo do Fôlego)
            // O Rectangle corta a largura da imagem de acordo com o 'quotient'
            Rectangle fillRect = new Rectangle(0, 0, (int)(barFill.Width * quotient), barFill.Height);
            spriteBatch.Draw(barFill, drawPos, fillRect, Color.White);

            // Opcional: Desenha o número por cima da barra (ex: "80 / 100")
            int displayBreath = (int)overPlayer.currentBreath; 
            string text = $"{displayBreath} / {overPlayer.maxBreath}";

            Color textColor = quotient <= 0.25f ? Color.Red : Color.Cyan;

            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - 20f, barFrame.Height);
            Utils.DrawBorderString(spriteBatch, text, textPos, textColor, 0.8f);

            base.Draw(spriteBatch);
        }
    }
}