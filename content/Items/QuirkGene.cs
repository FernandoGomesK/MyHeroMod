using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content;

namespace MyHeroMod.content.Items
{
    public class QuirkGene : ModItem
    {
        public override void SetStaticDefaults()
        {
            

        }

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