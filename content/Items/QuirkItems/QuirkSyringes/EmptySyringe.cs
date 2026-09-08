using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content;

namespace MyHeroMod.content.Items.QuirkItems.QuirkSyringes
{
    public class EmptySyringe : ModItem
    {
        public override void SetStaticDefaults()
        {
            

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(gold: 2); 
        }
    }
}