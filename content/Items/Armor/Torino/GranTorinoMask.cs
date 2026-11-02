
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;


namespace MyHeroMod.content.Items.Armor.Torino
{
    [AutoloadEquip(EquipType.Head)]
    public class GranTorinoMask: ModItem

    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);

            Item.headSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);

        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;

        }

        public override void UpdateEquip(Player player)
        {
            

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            



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
