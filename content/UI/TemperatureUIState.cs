using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.HellFlames;

namespace MyHeroMod.content.UI
{
    public class TemperatureUIState : UIState
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            
            if (!transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot) &&
                !transPlayer.HasActiveQuirk(QuirkType.HellFlames) &&
                !transPlayer.HasActiveQuirk(QuirkType.Blueflame))
                return;

            int currentTemp = 0;
            int maxTemp = 100;
            int minTemp = 100; 

        
            if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                var hchh = player.GetModPlayer<HalfColdHalfHotPlayer>();
                currentTemp = hchh.Temperature;
                maxTemp = hchh.MaxTemperature;
                minTemp = Math.Abs(hchh.MinTemperature);
            }
            else if (transPlayer.HasActiveQuirk(QuirkType.Blueflame))
            {
                var blue = player.GetModPlayer<BlueflamePlayer>();
                currentTemp = blue.Temperature;
                maxTemp = blue.MaxTemperature;
                
            }
            else if (transPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                var hell = player.GetModPlayer<HellFlamesPlayer>();
                currentTemp = hell.Temperature;
                maxTemp = hell.MaxTemperature;
                
            }

            Texture2D barFrame = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/TemperatureBarFrame").Value;
            Texture2D barFill = ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/TemperatureBarFill").Value;

            // Posição: Coloquei um pouco abaixo da barra de fôlego (ajuste o Y como preferir)
            Vector2 screenCenter = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 2f);
            Vector2 drawPos = screenCenter + new Vector2(-barFrame.Width / 2f, 80f); 

            // 1. Desenha a Borda primeiro
            spriteBatch.Draw(barFrame, drawPos, Color.White);

            // 2. Checa se o Phosphor está ativo
            bool hasPhosphor = player.HasBuff(ModContent.BuffType<PhosphorBuff>());
            Color textColor = Color.White;

            if (hasPhosphor)
            {
                
                float pulse = (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 5f) * 0.5f + 0.5f);
                Color phosphorColor = Color.Lerp(Color.Cyan, Color.OrangeRed, pulse);
                textColor = phosphorColor;

                
                spriteBatch.Draw(barFill, drawPos, phosphorColor);
            }
            else
            {
                // LÓGICA NORMAL (Divisão Central)
                float centerOffsetX = barFill.Width / 2f;
                Vector2 centerDrawPos = drawPos + new Vector2(centerOffsetX, 0);

                if (currentTemp > 0) // QUENTE (Barra enche para a Direita)
                {
                    float quotient = MathHelper.Clamp((float)currentTemp / maxTemp, 0f, 1f);
                    int fillWidth = (int)(centerOffsetX * quotient); // Metade da barra * porcentagem
                    
                    // Corta a imagem: Começa do meio (X), pega Y(0), Largura(fillWidth), Altura total
                    Rectangle sourceRect = new Rectangle((int)centerOffsetX, 0, fillWidth, barFill.Height);
                    
                    textColor = Color.OrangeRed;
                    spriteBatch.Draw(barFill, centerDrawPos, sourceRect, textColor);
                }
                else if (currentTemp < 0) // FRIO (Barra enche para a Esquerda)
                {
                    float quotient = MathHelper.Clamp((float)Math.Abs(currentTemp) / minTemp, 0f, 1f);
                    int fillWidth = (int)(centerOffsetX * quotient);
                    
                    // A posição na tela precisa ir para trás (esquerda) para a imagem crescer pra trás
                    Vector2 leftDrawPos = centerDrawPos - new Vector2(fillWidth, 0);
                    
                    // Corta a imagem: (Meio menos o tamanho atual)
                    Rectangle sourceRect = new Rectangle((int)centerOffsetX - fillWidth, 0, fillWidth, barFill.Height);

                    textColor = Color.Cyan;
                    spriteBatch.Draw(barFill, leftDrawPos, sourceRect, textColor);
                }
            }

        
            string text = hasPhosphor ? "PHOSPHOR" : $"{currentTemp}°C";
            Vector2 textPos = drawPos + new Vector2(barFrame.Width / 2f - (hasPhosphor ? 40f : 15f), barFrame.Height + 2f);
            Utils.DrawBorderString(spriteBatch, text, textPos, textColor, 0.8f);

            base.Draw(spriteBatch);
        }
    }
}