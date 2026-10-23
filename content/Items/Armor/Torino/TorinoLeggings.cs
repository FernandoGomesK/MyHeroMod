using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Items.Armor.Deku.DeltaArmor;

namespace MyHeroMod.content.Items.Armor.Torino
{
    [AutoloadEquip(EquipType.Legs)]
    public class TorinoLeggings : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 25; 
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                
                .Register();
        }
    }
}