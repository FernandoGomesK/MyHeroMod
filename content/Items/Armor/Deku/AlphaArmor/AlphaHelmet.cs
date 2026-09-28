using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.Armor.Deku.AlphaArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class AlphaHelmet : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
                
            player.GetDamage(DamageClass.Melee) += 0.03f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AlphaBreastplate>() && legs.type == ModContent.ItemType<AlphaLeggings>();
        }

        
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases defense by 2 and melee speed by 5%";
            player.statDefense += 2;
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
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