using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace MyHeroMod.content.Items.Armor.Deku.EpsilonArmor
{
    [AutoloadEquip(EquipType.Body)]
    public class EpsilonBreastplate : ModItem
    {
        

        public static int CapeSlotID { get; private set; }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server) return;

            CapeSlotID = EquipLoader.AddEquipTexture(Mod, "MyHeroMod/content/Items/Armor/Deku/EpsilonArmor/Epsilon_Cape", EquipType.Back, this);
            
        }

                


    
    public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ArmorIDs.Body.Sets.IncludedCapeBack[Item.bodySlot] = CapeSlotID;
        }
    
        public override void SetDefaults()
        {
            Item.width = 18; // Tamanho do item no chão/inventário
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 24; 
        }
        public override void UpdateEquip(Player player)
        {
            // Seus buffs aqui (ex: +Dano, +Velocidade)
            // player.GetDamage(DamageClass.Generic) += 0.10f; 
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ChlorophyteBar, 1) 
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

}
}