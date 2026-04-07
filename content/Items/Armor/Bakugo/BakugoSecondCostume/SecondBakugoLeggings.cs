using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Items.Armor.Bakugo.BakugoFirstCostume;

namespace MyHeroMod.content.Items.Armor.Bakugo.BakugoSecondCostume
{
    [AutoloadEquip(EquipType.Legs)]
    public class SecondBakugoLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 12; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FirstBakugoLeggings>(), 1)
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}