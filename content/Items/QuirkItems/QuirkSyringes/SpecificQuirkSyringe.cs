using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Items;
using MyHeroMod.content.Tiles.CraftingStations;

namespace MyHeroMod.content.Items.QuirkItems.QuirkSyringes
{
    public abstract class SpecificQuirkSyringe : ModItem
    {
        public abstract QuirkType TargetQuirk { get; }
        
        
        public abstract int RequiredGeneType { get; } 

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item3; 
            Item.rare = ItemRarityID.LightRed;
        }

        public override bool CanUseItem(Player player)
        {
            if (UISystem.IsUiOpen()) return false;
            var quirkPlayer = player.GetModPlayer<TransformationPlayer>();
            return !quirkPlayer.HasActiveQuirk(TargetQuirk); 
        }

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                
                transPlayer.ActiveQuirks.Add(TargetQuirk);
                player.QuickSpawnItem(player.GetSource_ItemUse(Item), ModContent.ItemType<EmptySyringe>());          
                Main.NewText($"You injected the {TargetQuirk.ToString()} quirk!", Microsoft.Xna.Framework.Color.LightGreen);
            }
            return true;
        }

        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(RequiredGeneType, 1)
                .AddIngredient(ModContent.ItemType<EmptySyringe>(), 1)
                .AddTile(ModContent.TileType<NomuVat>()) 
                .Register();
        }
    }
}