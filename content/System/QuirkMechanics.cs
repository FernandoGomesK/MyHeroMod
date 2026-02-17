using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Debuffs; // Certifique-se que o namespace do seu Debuff está aqui
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.System
{
    public static class QuirkMechanics
    {
        /// <summary>
        /// Verifica o superaquecimento e aplica a insolação se necessário.
        /// </summary>
        /// <param name="player">O Jogador atual</param>
        /// <param name="currentTemp">Temperatura/Calor atual</param>
        /// <param name="maxTemp">Temperatura Máxima permitida</param>
        public static void CheckHeatstroke(Player player, int currentTemp, int maxTemp)
        {
            // Se a temperatura passou do limite
            if (currentTemp > maxTemp)
            {
                
                player.AddBuff(ModContent.BuffType<Heatstroke>(), 2);

                
                if (Main.rand.NextBool(5))
                {
                    Dust.NewDust(player.position, player.width, player.height, Terraria.ID.DustID.Smoke, 0, 0, 100, default, 1.5f);
                }
            }
        }
    }
}