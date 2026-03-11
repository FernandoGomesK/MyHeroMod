using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Items.Armor.Todoroki.SecondTodorokiCostume
{
    [AutoloadEquip(EquipType.Legs)]
    public class SecondTodorokiLeggings : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 5; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
            // Aumenta a vida máxima em 20 quando equipado
            player.statLifeMax2 += 20;
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