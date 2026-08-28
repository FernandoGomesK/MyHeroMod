
using MyHeroMod.content.Quirks.OFA9th;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class AirGauntlets : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);
            
            Item.handOnSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.HandsOn);
            Item.handOffSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.HandsOff);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
        
            var quirkPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            quirkPlayer.isAirForceOn = true;
            quirkPlayer.isMidGauntletsOn = true;
            
            
            
        }

        public override void AddRecipes()
        {   
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MidGauntlets>(), 1)
                .AddIngredient(ModContent.ItemType<AirForce>(), 1)
                .AddTile(TileID.TinkerersWorkbench) 
                .AddIngredient(ItemID.SoulofFlight, 5) 
                .Register(); 
        }
        
        
        }
        }