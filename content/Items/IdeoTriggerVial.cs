using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MyHeroMod.content.Items
{
    public class IdeoTriggerVial : ModItem
    {
        public override void SetStaticDefaults()
        {
   
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 26;
            Item.value = Item.buyPrice(gold: 2);
            Item.maxStack = 30;
            Item.rare = ItemRarityID.LightRed; 
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.consumable = true;
        }

        public override void AddRecipes()
        {
  
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<TriggerVial>(), 1)
            .AddIngredient(ItemID.SoulofNight, 2)

          
            .AddIngredient(ItemID.Ichor, 3)

            .AddTile(TileID.AlchemyTable) 
            .Register();

            CreateRecipe()
            .AddIngredient(ModContent.ItemType<TriggerVial>(), 1)
            .AddIngredient(ItemID.SoulofNight, 2)
            .AddIngredient(ItemID.CursedFlame, 3)
            .AddTile(TileID.AlchemyTable)
            .Register();
        }

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            { 
                player.AddBuff(ModContent.BuffType<content.Buffs.Trigger.IdeoTriggerBuff>(), 7200);

                SoundEngine.PlaySound(SoundID.Item3, player.position);
            }
            return true;
        }
    }
}