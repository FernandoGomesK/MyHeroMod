using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.Armor.AllMight.YoungAge
{
    [AutoloadEquip(EquipType.Legs)]
    public class YoungLeggings : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 10; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
        
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 20)
                .AddRecipeGroup(RecipeSystem.CobaltBarGroup, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}