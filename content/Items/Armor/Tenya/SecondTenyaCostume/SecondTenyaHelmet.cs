using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Items.Armor.Deku.AlphaArmor;

namespace MyHeroMod.content.Items.Armor.Tenya.SecondTenyaCostume
{
    [AutoloadEquip(EquipType.Head)]
    public class SecondTenyaHelmet : ModItem
    {

        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            
        }
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 5; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
            // Aumenta a vida máxima em 20 quando equipado
            
        }
        // public override void AddRecipes()
        // {
        //     CreateRecipe()
        //         .AddIngredient(ModContent.ItemType<AlphaHelmet>(), 1)
        //         .AddIngredient(ItemID.HellstoneBar, 10) 
        //         .AddIngredient(ItemID.Bone, 5)
        //         .AddTile(TileID.Anvils)
        //         .Register();
        // }
    }
}