using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content;

namespace MyHeroMod.content.Items
{
    public class QuirkSelectorItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(12, 8));

        }
        public override void SetDefaults(){
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Blue;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        
        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                if (player.altFunctionUse == 2)
                {
                    CycleStage(player);
                }
                UISystem.ShowUI();
            }
            return true;
        }    
        private void CycleStage(Player player)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();

            modPlayer.CurrentStage++;

            if (modPlayer.CurrentStage > QuirkStage.Final)
            {
                modPlayer.CurrentStage = QuirkStage.Initial;
            }

            Main.NewText($"Current Stage: {modPlayer.CurrentStage}", Color.Green);
        }
    }
}
