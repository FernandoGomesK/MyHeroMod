using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Items.Armor.Deku.DeltaArmor;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.Vanities.AllMightClothes
{
    [AutoloadEquip(EquipType.Legs)]
    public class AllMightPants : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 0;
        }
        public override void UpdateEquip(Player player)
        {
            //player.moveSpeed += 0.15f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Silk, 20)
            //.AddRecipeGroup(RecipeSystem.IronAndLeadGroup, 15)
            .AddTile(TileID.Loom)
            .Register();
        }
    }
}