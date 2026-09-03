// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using MyHeroMod.content.Tiles.CraftingStations;

// namespace MyHeroMod.content.Items.Placeable
// {
//     public class NomuVatItem : ModItem
//     {
//         public override void SetDefaults()
//         {
//             Item.width = 28;
//             Item.height = 14;
//             Item.maxStack = 99; 
            
            
//             Item.useTurn = true;
//             Item.autoReuse = true;
//             Item.useAnimation = 15;
//             Item.useTime = 10;
//             Item.useStyle = ItemUseStyleID.Swing;
//             Item.consumable = true;
            
            
//             Item.createTile = ModContent.TileType<NomuVat>();
//         }

      
//         public override void AddRecipes()
//         {
//             CreateRecipe()
//                 .AddIngredient(ItemID.IronBar, 10)
//                 .AddIngredient(ItemID.Wood, 10)
//                 .AddTile(TileID.WorkBenches)
//                 .Register();
//         }
//     }
// }