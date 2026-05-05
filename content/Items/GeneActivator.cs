using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items
{
    public class GeneActivator : ModItem
    {
        public override void SetStaticDefaults()
        {
            

        }
        public override void SetDefaults(){
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 120;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.UseSound = SoundID.Item3;
            Item.value = Item.buyPrice(gold: 10);
        }

         public override bool CanUseItem(Player player)
        {
            if (UISystem.IsUiOpen())
            {
                return false;
            }
            return true;
        }


//         public override void AddRecipes()
// {
//     CreateRecipe()
//         .AddIngredient(ModContent.ItemType<Items.QuirkGene>(), 1) 
//         .AddIngredient(ModContent.ItemType<Items.EmptySyringe>(), 1) 
//         .AddTile(TileID.WorkBenches)         
//         .Register();                         
// }

    
        

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                
                RandomNatureSelection.SelectRandomNature();

            }
            return true;
        }    
        
    }
}
