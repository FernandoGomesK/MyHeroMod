using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace MyHeroMod.content.Debuffs // Ajuste o namespace se necessário
{
    // Herde de ModBuff, não "Debuffs" (a menos que você tenha criado essa classe base)
    public class QuirkErased : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // Diz que é um Debuff (vermelho, não pode cancelar clicando)
            Main.buffNoSave[Type] = true; // Não salva se sair do jogo
            Main.pvpBuff[Type] = true;
        }
    }}