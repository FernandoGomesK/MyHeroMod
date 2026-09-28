using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.Hardening.Visuals
{

    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class HardeningArms : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.vanity = true;
            Item.value = 0;
            Item.rare = ItemRarityID.Blue;
        }
    }
}