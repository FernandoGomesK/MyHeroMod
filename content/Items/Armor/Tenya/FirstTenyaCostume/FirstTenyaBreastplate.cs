using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;


namespace MyHeroMod.content.Items.Armor.Tenya.FirstTenyaCostume
{
    [AutoloadEquip(EquipType.Body)]
    public class FirstTenyaBreastplate : ModItem
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
            Item.width = 18; 
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 7; 
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.GetDamage(DamageClass.Generic) += 0.10f; 
        }

        // public override void AddRecipes()
        // {
        //     CreateRecipe()
        //         .AddIngredient(ModContent.ItemType<AlphaBreastplate>(), 1)
        //         .AddIngredient(ItemID.HellstoneBar, 10) 
        //         .AddIngredient(ItemID.Bone, 5)
        //         .AddTile(TileID.Anvils)
        //         .Register();
        // }

}
}