using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Items.Armor.AllMight.YoungAge 
{
    // Define que é um item de Cabeça
    [AutoloadEquip(EquipType.Head)]
    public class YoungHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // --- 1. ESCONDER O CABELO ORIGINAL ---
            
            // DrawHead = true: Mostra o rosto do personagem (nariz, olhos)
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = true; 
            
            // DrawHatHair = false: Esconde o cabelo que aparece "embaixo" de chapéus
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = false; 
            
            // DrawFullHair = false: Esconde o cabelo completo
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = false;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            
            // Se for uma armadura de verdade:
            Item.defense = 10;
            
            // Se for apenas visual (Vanity), descomente a linha abaixo:
            // Item.vanity = true;
        }

        // --- 2. A MÁGICA DA COR ---
        
        // Este método roda antes de desenhar o capacete. 
        // Ele permite trocar a cor do sprite dinamicamente.
        public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            // OPÇÃO A: Cabelo Dinâmico (Respeita a cor que o jogador escolheu na criação)
            // O sprite cinza será tingido pela cor do cabelo do player.
            color = drawPlayer.hairColor;
            
            // OPÇÃO B: Cabelo Canônico (Sempre Loiro)
            // Se você quer que seja sempre amarelo igual ao anime, independente da cor do player:
            // color = new Color(255, 220, 0); // Amarelo All Might
        }
    }
}