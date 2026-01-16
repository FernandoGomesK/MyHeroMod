using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures; // Necessário para acessar as Camadas (Layers)
using MyHeroMod.content.Items.Weapons; // Para reconhecer o PunchAttack

namespace MyHeroMod.content.System
{
    public class HideArmsPlayer : ModPlayer
    {
        // Este método roda todo frame de desenho do personagem
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            // Verifica se:
            // 1. O item que o jogador está segurando é o PunchAttack
            // 2. O jogador está no meio da animação de uso (itemAnimation > 0)
            if (Player.HeldItem.type == ModContent.ItemType<PunchAttack>() && Player.itemAnimation > 0)
            {
                // Esconde o braço da frente (que normalmente segura a arma)
                PlayerDrawLayers.ArmOverItem.Hide();
                
                // Esconde a mão (caso ela seja desenhada separadamente)
                PlayerDrawLayers.HandOnAcc.Hide();
            }
        }
    }
}