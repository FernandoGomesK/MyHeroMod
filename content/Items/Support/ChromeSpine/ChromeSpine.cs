
using MyHeroMod.content.Buffs.Overclock;
using MyHeroMod.content.Quirks.OFA9th;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support.ChromeSpine
{
    [AutoloadEquip(EquipType.Back)]
    public class ChromeSpine : ModItem
    {

        public override void SetDefaults()  
        {
            Item.width = 28;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow; 
            Item.value = Item.buyPrice(gold: 35); 
            Item.backSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Back);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

      
            player.AddBuff(ModContent.BuffType<ChromeSpineBuff>(),2);


        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Nanites, 15)
                .AddIngredient(ItemID.MartianConduitPlating, 30)
                .AddIngredient(ItemID.SoulofSight, 10)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }


    }
}