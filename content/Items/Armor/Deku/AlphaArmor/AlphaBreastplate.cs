using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.Armor.Deku.AlphaArmor
{
    [AutoloadEquip(EquipType.Body)]
    public class AlphaBreastplate : ModItem
    {
        public static int FemaleBodySlot;

        public static int CapeSlotID { get; private set; }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server) return;

            
            
        }

                


    
    public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            
        }
    public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
            if (!male && FemaleBodySlot != -1)
            {
                equipSlot = FemaleBodySlot;
            }
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.defense = 4;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.04f;
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