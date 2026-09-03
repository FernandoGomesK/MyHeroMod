using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Items.Support.DekuArmor;

namespace MyHeroMod.content.Items.Support.UrarakaSupport.ZeroBetaBoots
{
    [AutoloadEquip(EquipType.Shoes)]
    public class ZeroBetaBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override void UpdateEquip(Player player)
        { 
            var zeroDashPlayer = player.GetModPlayer<UrarakaDashPlayer>();
            zeroDashPlayer.isUrarakaDashBootsOn = true;
        }

        public override void AddRecipes()
        {   
            
        }
    }
}