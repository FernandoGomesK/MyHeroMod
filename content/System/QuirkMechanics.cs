using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Debuffs; // Certifique-se que o namespace do seu Debuff está aqui
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.System
{
    public static class QuirkMechanics
    {
        /// <summary>
        /// verifies the current Heat
        /// </summary>
        /// <param name="player">Currente Player</param>
        /// <param name="currentTemp">Current Temperature</param>
        /// <param name="maxTemp">Max Temperature allowed</param>
        public static void CheckHeatstroke(Player player, int currentTemp, int maxTemp)
        {
            // if current Temp > mas temp
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