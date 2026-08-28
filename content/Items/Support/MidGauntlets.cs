
using MyHeroMod.content.Quirks.OFA9th;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class MidGauntlets : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 1);
            
            Item.handOnSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.HandsOn);
            Item.handOffSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.HandsOff);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
        
            var quirkPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            quirkPlayer.isMidGauntletsOn = true;
            
            
            
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        
        
        }
        }