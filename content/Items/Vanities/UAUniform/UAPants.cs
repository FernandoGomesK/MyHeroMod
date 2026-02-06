using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Items.Vanities.UAUniform
{
    [AutoloadEquip(EquipType.Legs)]
    public class UAPants : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
             // Defesa do capacete
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Silk, 10)
            .AddTile(TileID.Loom)
            .Register();
        }
    }
}