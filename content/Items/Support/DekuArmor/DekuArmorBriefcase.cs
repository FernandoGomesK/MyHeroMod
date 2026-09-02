
using MyHeroMod.content.Quirks.OFA9th;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support.DekuArmor
{
    
    public class DekuArmorBriefcase : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);
            
            
        
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            
            
        }

        public override void AddRecipes()
        {   
            
        }
        
        
        }
        }