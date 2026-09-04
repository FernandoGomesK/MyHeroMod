using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Items;
using MyHeroMod.content.Tiles.CraftingStations;

namespace MyHeroMod.content.Items.QuirkItems
{
    public abstract class NomuFragment : ModItem
    {
        

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 16;
            Item.maxStack = 99;
             
            Item.rare = ItemRarityID.LightRed;
        }

        
        public override void AddRecipes()
        {
            
        }
    }
}