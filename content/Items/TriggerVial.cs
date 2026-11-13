using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MyHeroMod.content.Items
{
    public class TriggerVial : ModItem
    {
        public override void SetStaticDefaults()
        {
          
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 26;
            Item.value = Item.buyPrice(silver: 50); 
            Item.maxStack = 30; 
            Item.rare = ItemRarityID.Green; 
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.DrinkLiquid; 
            Item.consumable = true; 
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.Deathweed, 1)
            .AddIngredient(ModContent.ItemType<Items.QuirkGene>(), 3) 
            .AddIngredient(ItemID.GlowingMushroom, 3)
            .AddTile(TileID.Bottles) 
            .Register();
        }

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                
                player.AddBuff(ModContent.BuffType<content.Buffs.Trigger.TriggerBuff>(), 3600);

              
                SoundEngine.PlaySound(SoundID.Item3, player.position);
            }
            return true;
        }
    }
}