using MyHeroMod.content.Quirks.HalfColdHalfHot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    public class SurgeArmGauntlets : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Pega o Player do Todoroki e liga o colete
            var quirkPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            quirkPlayer.IsSurgeArmGauntletsOn = true;
            
            
            
        }
        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddIngredient(ItemID.HellstoneBar, 10) 
            .AddIngredient(ItemID.Bone, 5)         
            .AddTile(TileID.Anvils)
            .Register();
            }
        
        }
        }