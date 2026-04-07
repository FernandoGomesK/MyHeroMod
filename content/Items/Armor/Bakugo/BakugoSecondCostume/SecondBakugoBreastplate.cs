using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Items.Armor.Bakugo.BakugoFirstCostume;

namespace MyHeroMod.content.Items.Armor.Bakugo.BakugoSecondCostume
{
    [AutoloadEquip(EquipType.Body)]
    public class SecondBakugoBreastplate : ModItem
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
            Item.width = 18; // Tamanho do item no chão/inventário
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 18; 
        }
        
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 15;
            player.GetDamage(DamageClass.Generic) += 0.15f; 
            player.moveSpeed += 0.10f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FirstBakugoBreastplate>(), 1)
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    

}
}