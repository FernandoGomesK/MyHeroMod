using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Items.Armor.Deku.BetaArmor;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.Armor.Deku.GammaArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class GammaLeggings : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 14; // Defesa do capacete
        }

        public override void UpdateEquip(Player player)
        {
            // Aumenta a vida máxima em 20 quando equipado
        
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BetaLeggings>(), 1)
                
                .AddRecipeGroup(RecipeSystem.AdamantineGroup, 12)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}