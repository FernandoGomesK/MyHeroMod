using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.Armor.Bakugo.BakugoFirstCostume
{
    [AutoloadEquip(EquipType.Legs)]
    public class FirstBakugoLeggings : ModItem
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
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 20) 
                .AddRecipeGroup(RecipeSystem.EvilIronGroup, 15)
                .AddIngredient(ItemID.Grenade, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}