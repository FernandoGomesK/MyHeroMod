using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs; // Necessário para ler o Buff do Overclock

namespace MyHeroMod.content.System
{
    public class TimeStopSystem : ModSystem
    {
        public static bool IsTimeStopped = false;

        // Isto corre ANTES dos Monstros, Tiros e Ecrã atualizarem
        public override void PreUpdateEntities()
        {
            IsTimeStopped = false;

            // Vasculha todos os jogadores do servidor (Perfeito para Multiplayer!)
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player p = Main.player[i];
                
                // Se encontrar algum jogador vivo com o fôlego do Overclock ativo...
                if (p.active && !p.dead && p.HasBuff(ModContent.BuffType<OverclockBuff>()))
                {
                    IsTimeStopped = true;
                    break; // Pára de procurar, o tempo congelou!
                }
            }
        }
    }
}