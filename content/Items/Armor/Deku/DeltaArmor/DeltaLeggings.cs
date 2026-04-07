using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Items.Armor.Deku.GammaArmor;

namespace MyHeroMod.content.Items.Armor.Deku.DeltaArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class DeltaLeggings : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 20; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<GammaLeggings>(), 1)
                .AddIngredient(ItemID.ChlorophyteBar, 15) 
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}