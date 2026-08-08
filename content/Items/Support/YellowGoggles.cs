
using MyHeroMod.content.Quirks.Erasure;
using MyHeroMod.content.Quirks.OFA9th;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    [AutoloadEquip(EquipType.Head)]
    
    public class YellowGoggles : ModItem
    
    {
        
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);
            
            Item.headSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
        
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
        
            var quirkPlayer = player.GetModPlayer<ErasurePlayer>();
            quirkPlayer.isYellowGogglesOn = true;
            
            
            
        }

        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddIngredient(ItemID.Sunglasses, 10) 
            .AddIngredient(ItemID.YellowDye, 5)         
            .AddTile(TileID.Anvils)
            .Register();
            }
        
        
        }
        }