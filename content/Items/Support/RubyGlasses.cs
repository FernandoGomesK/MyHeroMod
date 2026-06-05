// using MyHeroMod.content.Quirks.HalfColdHalfHot;

using MyHeroMod.content.Quirks.OpticBlast;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    [AutoloadEquip(EquipType.Head)]
    
    public class RubyGlasses : ModItem
    
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
        
            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();
            opticPlayer.isRubyGlassesEquipped = true;
            
            
            
        }

        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddIngredient(ItemID.Sunglasses, 10) 
            .AddIngredient(ItemID.Ruby, 2)         
            .AddTile(TileID.Anvils)
            .Register();
            }
        
        
        }
        }