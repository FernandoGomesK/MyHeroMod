using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content
{
    public class QuirkSelectorItem : ModItem
    {
        public override void SetDefaults(){
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Blue;
        }
        
        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                UISystem.ShowUI();
            }
            return true;
        }    
    }
}
