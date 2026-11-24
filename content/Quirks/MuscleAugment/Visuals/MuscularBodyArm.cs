using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.MuscleAugment.Visuals
{
   
    [AutoloadEquip(EquipType.Body)]
    public class MuscularBodyArm : ModItem
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