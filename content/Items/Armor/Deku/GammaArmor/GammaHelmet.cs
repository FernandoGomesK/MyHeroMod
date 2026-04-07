using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Items.Armor.Deku.BetaArmor;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.Armor.Deku.GammaArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class GammaHelmet : ModItem
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
            Item.defense = 8; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
            
            
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BetaHelmet>(), 1)
                
                .AddRecipeGroup(RecipeSystem.AdamantineGroup, 12)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}