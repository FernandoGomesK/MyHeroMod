using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content;

namespace MyHeroMod.content.Items
{
    public class EmptySyringe : ModItem
    {
        public override void SetStaticDefaults()
        {
            

        }

        public override void SetDefaults(){
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 120;
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
{
    CreateRecipe()
        .AddIngredient(ItemID.Glass, 2) 
        .AddIngredient(RecipeGroupID.IronBar, 1) 
        .AddTile(TileID.WorkBenches)         
        .Register();                         
}
        }
        }