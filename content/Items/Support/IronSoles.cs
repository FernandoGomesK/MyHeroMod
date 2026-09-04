
using MyHeroMod.content.Quirks.OFA9th;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    [AutoloadEquip(EquipType.Shoes)]
    public class IronSoles : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 22;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        { 
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            ofaPlayer.isIronSolesOn = true;
        }

        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddIngredient(ItemID.TissueSample, 10)  
            .AddIngredient(ItemID.ShadowScale, 10)
            .AddIngredient(ItemID.IronGreaves, 1)       
            .AddTile(TileID.Anvils)
            .Register();
            }
        
        }
        }