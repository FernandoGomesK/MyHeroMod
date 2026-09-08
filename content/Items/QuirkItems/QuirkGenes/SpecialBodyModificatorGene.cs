using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.System;
using MyHeroMod.content.Items.QuirkItems;
using MyHeroMod.content.Tiles.CraftingStations;

namespace MyHeroMod.content.Items.QuirkItems.QuirkGenes
{
    public class SpecialBodyModificatorGene : ModItem
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
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                
                RandomNatureSelection.SelectRandomNature();
                if (transPlayer.Nature == NatureType.ResistantBody)
                {
                    transPlayer.Nature = NatureType.PerfectVessel;
                }

            }
            return true;
        }    

        public override void AddRecipes()
        {
            CreateRecipe()
               
                .AddIngredient(ModContent.ItemType<NomuFragment>(), 10)
                .AddTile(ModContent.TileType<NomuVat>()) 
                .Register();
        }
        
    }
}
