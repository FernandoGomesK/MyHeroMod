using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using MyHeroMod.content.System;
using MyHeroMod.content.Items.Armor.Deku.BetaArmor;

namespace MyHeroMod.content.Items.Armor.Deku.GammaArmor
{
    [AutoloadEquip(EquipType.Body)]
    public class GammaBreastplate : ModItem
    {
    

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server) return;

            
            
        }

                


    
    public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            
        }

        public override void SetDefaults()
        {
            Item.width = 18; // Tamanho do item no chão/inventário
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 22; 
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.GetDamage(DamageClass.Generic) += 0.10f; 
            player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BetaBreastplate>(), 1)
                
                .AddRecipeGroup(RecipeSystem.AdamantineGroup, 12)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

}
}