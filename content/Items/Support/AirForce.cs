// using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.OFA9th;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class AirForce : ModItem
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
            
            
            
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.IronBar, 15)
            .AddTile(TileID.Anvils)
            .Register();
        }
        
        
        }
        }