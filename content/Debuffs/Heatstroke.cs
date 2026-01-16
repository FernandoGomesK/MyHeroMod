using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Debuffs // Ajuste o namespace se necessário
{
    // Herde de ModBuff, não "Debuffs" (a menos que você tenha criado essa classe base)
    public class Heatstroke : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // Diz que é um Debuff (vermelho, não pode cancelar clicando)
            Main.buffNoSave[Type] = true; // Não salva se sair do jogo
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // --- 1. Redução de Velocidade (50%) ---
            player.moveSpeed *= 0.5f;
            
            // Se quiser afetar a velocidade máxima de corrida também:
            player.accRunSpeed *= 0.5f;
            player.maxRunSpeed *= 0.5f;

            // --- 2. Redução de Dano Geral (50%) ---
            player.GetDamage(DamageClass.Generic) *= 0.5f;

            // --- 3. Bloquear Skills ---
            // A gente não bloqueia aqui dentro do Update. 
            // A gente verifica se o player TEM esse buff lá no código das skills.
        }
    }
}