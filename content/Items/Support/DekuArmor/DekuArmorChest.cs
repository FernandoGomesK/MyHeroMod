using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using MyHeroMod.content.Items.Armor.Deku.DeltaArmor;

namespace MyHeroMod.content.Items.Support.DekuArmor
{
    [AutoloadEquip(EquipType.Body)]
    public class DekuArmorChest : ModItem
    {
        

        public static int CapeSlotID { get; private set; }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server) return;

            CapeSlotID = EquipLoader.AddEquipTexture(Mod, "MyHeroMod/content/Items/Support/DekuArmor/DekuArmorCape", EquipType.Back, this);
            
        }

                


    
    public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ArmorIDs.Body.Sets.IncludedCapeBack[Item.bodySlot] = CapeSlotID;
        }
    
        public override void SetDefaults()
        {
            Item.width = 18; 
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 30; 
        }
        public override void UpdateEquip(Player player)
        {
            var dekuPlayer = player.GetModPlayer<DekuArmorPlayer>();
            dekuPlayer.isChestArmorOn = true;
            player.moveSpeed += 0.15f;
            player.GetDamage(DamageClass.Generic) += 0.25f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.20f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DeltaBreastplate>(), 1)
                .AddIngredient(ItemID.BeetleHusk, 10)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

}
}