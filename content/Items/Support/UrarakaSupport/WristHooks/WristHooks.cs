using MyHeroMod.content.Quirks.Explosion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support.UrarakaSupport.WristHooks
{
    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class WristHooks : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);

            Item.handOnSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.HandsOn);
            Item.handOffSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.HandsOff);
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            
            
            
        
            
        }

        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddIngredient(ItemID.TissueSample, 10) 
            .AddIngredient(ItemID.ShadowScale, 10)
            .AddTile(TileID.Anvils)
            .Register();
            }
        
        }
        }