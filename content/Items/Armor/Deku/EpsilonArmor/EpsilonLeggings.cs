using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Items.Armor.Deku.EpsilonArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class EpsilonLeggings : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 22; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
            // Aumenta a vida máxima em 20 quando equipado
           
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ChlorophyteBar, 1) 
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}