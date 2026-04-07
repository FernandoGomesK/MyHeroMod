using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Items.Armor.AllMight.GoldenAge
{
    // Define que é um item de Cabeça
    [AutoloadEquip(EquipType.Head)]
    public class GoldenHelmet : ModItem
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
            
            
            Item.defense = 10;
            
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ChlorophyteBar, 15)
                .AddIngredient(ItemID.BeetleHusk, 5) 
                .AddTile(TileID.Anvils)
                .Register();
        }

                public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            
            color = drawPlayer.hairColor;
            
            
        }
    }
}