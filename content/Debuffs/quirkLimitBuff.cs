using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace MyHeroMod.content.Debuffs // Ajuste o namespace se necessário
{
    // Herde de ModBuff, não "Debuffs" (a menos que você tenha criado essa classe base)
    public class quirkLimitBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // Diz que é um Debuff (vermelho, não pode cancelar clicando)
            Main.buffNoSave[Type] = true; // Não salva se sair do jogo
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            player.moveSpeed *= 0.2f;
            
            
            player.accRunSpeed *= 0.5f;
            player.maxRunSpeed *= 0.5f;

            // // --- 2. Redução de Dano Geral (50%) ---
            // player.GetDamage(DamageClass.Generic) *= 0.5f;

          

            
        }
    }
}