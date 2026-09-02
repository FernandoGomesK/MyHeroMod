using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Items.Support.DekuArmor;

namespace MyHeroMod.content.Items.Support.DekuArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class DekuArmorBoots : ModItem
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
            var armorPlayer = player.GetModPlayer<DekuArmorPlayer>();
            armorPlayer.isArmorBootsOn = true;
        }

        public override void AddRecipes()
        {   
            
        }
    }
}