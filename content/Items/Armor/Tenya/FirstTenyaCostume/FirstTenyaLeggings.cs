using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Items.Armor.Tenya.FirstTenyaCostume
{
    [AutoloadEquip(EquipType.Legs)]
    public class FirstTenyaLeggings : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 3; 
        }
        public override void UpdateEquip(Player player)
        {
            
            
        }
        // public override void AddRecipes()
        // {
        //     CreateRecipe()
        //     .AddIngredient(ItemID.Silk, 20)
        //     .AddIngredient(RecipeGroupID.IronBar, 15)
        //     .AddTile(TileID.Loom)
        //     .Register();
        // }
    }
}