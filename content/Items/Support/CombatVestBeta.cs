using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass;


namespace MyHeroMod.content.Items.Support
{
    [AutoloadEquip(EquipType.Front)]
    public class CombatVestBeta : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);

            Item.frontSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Front);
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {        
            foreach (var modPlayer in player.ModPlayers)
            {
                
                if (modPlayer is BaseIceAndFirePlayer fireIceUser)
                {
                    fireIceUser.isCombatVestBetaOn = true; 
                }
            }
            
        }
        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddIngredient(ItemID.TissueSample, 10) 
            .AddIngredient(ItemID.ShadowScale, 10)
            .AddIngredient(ItemID.HellstoneBar, 5)
            .AddIngredient(ItemID.IceBlock, 5) 
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}