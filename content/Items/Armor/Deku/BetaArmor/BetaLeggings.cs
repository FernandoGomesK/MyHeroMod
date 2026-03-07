using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Items.Armor.Deku.BetaArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class BetaLeggings : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 6; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
            // Aumenta a vida máxima em 20 quando equipado
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 15) // Exemplo
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}