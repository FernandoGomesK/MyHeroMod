using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.Armor.Deku.AlphaArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class AlphaHelmet : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 2; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
            
           
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Silk, 20)
            .AddRecipeGroup(RecipeSystem.IronAndLeadGroup, 15)
            .AddTile(TileID.Loom)
            .Register();
        }
    }
}