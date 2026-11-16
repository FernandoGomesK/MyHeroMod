using MyHeroMod.content.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Armor.AntiAFO
{
    [AutoloadEquip(EquipType.Legs)]
    public class AntiAFOPants : ModItem
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
            player.moveSpeed += 0.05f;
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
