using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.Hardening.Visuals
{

    [AutoloadEquip(EquipType.Front)]
    public class UHardeningBody : ModItem
    {

        public override void SetStaticDefaults()
        {
        }

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