using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System; 

namespace MyHeroMod.content.Items.QuirkItems.QuirkSyringes
{
    public abstract class SpecificQuirkGene : ModItem
    {
        
        public abstract QuirkType TargetQuirk { get; }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 50);
        }

       

        
    }
}